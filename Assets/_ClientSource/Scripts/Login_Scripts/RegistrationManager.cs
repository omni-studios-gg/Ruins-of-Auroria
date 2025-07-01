using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using UnityEngine.SceneManagement;
using System.Net;
using System.Net.Sockets;

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

    // Make sure this URL matches your backend route for registration
    private string apiUrl = "http://localhost:5062/api/users/register";

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

        // Local validation
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

        StartCoroutine(RegisterUser(username, password, email));
    }

    IEnumerator RegisterUser(string username, string password, string email)
    {
        var payload = new RegisterPayload
        {
            username = username,
            password = password,
            email = email,
            ip = GetLocalIPAddress(),
            os = SystemInfo.operatingSystem
        };

        string jsonData = JsonUtility.ToJson(payload);

        using var request = new UnityEngine.Networking.UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
        {
            messageText.text = "✅ Registered! Redirecting...";
            ClearFields();
            Invoke(nameof(SwitchToLoginPanel), 1.5f);
        }
        else
        {
            string response = request.downloadHandler.text;
            Debug.LogWarning("❌ Register failed: " + response);
            messageText.text = "❌ " + (string.IsNullOrEmpty(response) ? request.error : response);
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
            loginUIController.ShowLoginPanel();
        else
            Debug.LogWarning("LoginUIController reference not set.");
    }

    private string GetLocalIPAddress()
    {
        try
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
        }
        catch { }
        return "Unknown";
    }

    [System.Serializable]
    private class RegisterPayload
    {
        public string username;
        public string password;
        public string email;
        public string ip;
        public string os;
    }
}
