using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

public class RegistrationManager : MonoBehaviour
{
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_InputField confirmPasswordInput;
    [SerializeField] private TMP_InputField emailInput;

    [Header("UI Elements")]
    [SerializeField] private Button registerButton;
    [SerializeField] private TMP_Text messageText;

    [Header("UI Controller")]
    [SerializeField] private LoginUIController loginUIController;

    private string connectionString = "Server=localhost;Database=authdb;User ID=root;Password=root;";

    void Start()
    {
        registerButton.onClick.AddListener(OnRegisterClicked);
    }

    void OnRegisterClicked()
    {
        string username = usernameInput.text.Trim();
        string password = passwordInput.text;
        string confirm = confirmPasswordInput.text;
        string email = emailInput.text.Trim();

        // Validate fields
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirm) || string.IsNullOrEmpty(email))
        {
            messageText.text = "Please fill in all fields.";
            return;
        }

        if (username.Length < 3 || username.Length > 16)
        {
            messageText.text = "Username must be 3–16 characters.";
            return;
        }

        if (!email.Contains("@") || !email.Contains("."))
        {
            messageText.text = "Invalid email address.";
            return;
        }

        if (password != confirm)
        {
            messageText.text = "Passwords do not match.";
            return;
        }

        // Register user
        if (RegisterUser(username, password, email))
        {
            messageText.text = "Registration successful. Redirecting to login...";
            ClearFields();
            Invoke(nameof(SwitchToLoginPanel), 1.5f);
        }
        else
        {
            messageText.text = "Username already exists or database error.";
        }
    }

    private void ClearFields()
    {
        usernameInput.text = "";
        passwordInput.text = "";
        confirmPasswordInput.text = "";
        emailInput.text = "";
    }

    private void SwitchToLoginPanel()
    {
        if (loginUIController != null)
        {
            loginUIController.ShowLoginPanel();
        }
        else
        {
            Debug.LogWarning("LoginUIController reference not set.");
        }
    }

    private bool RegisterUser(string username, string password, string email)
    {
        try
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();

            // Check for existing username
            using var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM users WHERE username = @u;", conn);
            checkCmd.Parameters.AddWithValue("@u", username);
            if ((long)checkCmd.ExecuteScalar() > 0)
                return false;

            // Insert new user with email, OS, and timestamp
            string os = SystemInfo.operatingSystem;
            using var insertCmd = new MySqlCommand(
                "INSERT INTO users (username, password_hash, email, created_at, os) VALUES (@u, @p, @e, NOW(), @os);",
                conn
            );

            insertCmd.Parameters.AddWithValue("@u", username);
            insertCmd.Parameters.AddWithValue("@p", Hash(password));
            insertCmd.Parameters.AddWithValue("@e", email);
            insertCmd.Parameters.AddWithValue("@os", os);

            return insertCmd.ExecuteNonQuery() > 0;
        }
        catch (MySqlException e)
        {
            Debug.LogError("MySQL Error: " + e.Message);
            return false;
        }
    }

    private string Hash(string password)
    {
        using var sha = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(password);
        byte[] hash = sha.ComputeHash(bytes);
        return System.Convert.ToBase64String(hash);
    }
}
