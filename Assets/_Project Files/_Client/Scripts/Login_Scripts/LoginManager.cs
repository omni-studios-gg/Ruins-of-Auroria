using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button loginButton;
    [SerializeField] private TMP_Text errorText;

    private string connectionString = "Server=localhost;Database=authdb;User ID=root;Password=root;";

    void Start()
    {
        loginButton.onClick.AddListener(OnLoginClicked);
    }

    void OnLoginClicked()
    {
        string username = usernameInput.text.Trim();
        string password = passwordInput.text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            errorText.text = "Please fill all fields.";
            return;
        }

        if (ValidateUser(username, password))
        {
            errorText.text = "";
            SceneManager.LoadScene("Demo");
        }
        else
        {
            errorText.text = "Invalid username or password.";
        }
    }

    private string Hash(string password)
    {
        using var sha = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(password);
        byte[] hash = sha.ComputeHash(bytes);
        return System.Convert.ToBase64String(hash);
    }

    bool ValidateUser(string username, string password)
    {
        try
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();

            // Check user with hashed password in password_hash column
            string query = "SELECT * FROM users WHERE username=@u AND password_hash=@p LIMIT 1;";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@p", Hash(password));

            using var reader = cmd.ExecuteReader();
            bool userExists = reader.HasRows;
            reader.Close();

            if (userExists)
            {
                // Update last login and IP
                string updateQuery = "UPDATE users SET last_login = NOW(), last_ip = @ip WHERE username = @u;";
                using var updateCmd = new MySqlCommand(updateQuery, conn);
                updateCmd.Parameters.AddWithValue("@u", username);
                updateCmd.Parameters.AddWithValue("@ip", GetLocalIPAddress());
                updateCmd.ExecuteNonQuery();

                return true;
            }

            return false;
        }
        catch (MySqlException e)
        {
            errorText.text = "Database error.";
            Debug.LogError("MySQL Error: " + e.Message);
            return false;
        }
    }

    // Helper function to get local IP address
    string GetLocalIPAddress()
    {
        try
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
        }
        catch
        {
            // fallback or ignore
        }
        return "Unknown";
    }
}
