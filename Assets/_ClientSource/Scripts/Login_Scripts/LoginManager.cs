using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button loginButton;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] public string main_worldscene;
    [SerializeField] private string Authserver_Url; // Update to your actual API URL

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

        StartCoroutine(LoginCoroutine(username, password));
    }

    IEnumerator LoginCoroutine(string username, string password)
    {
        string ip = GetLocalIPAddress();

        var loginData = new LoginPayload
        {
            username = username,
            password = password,
            ip = ip
        };

        string jsonData = JsonUtility.ToJson(loginData);

        using var request = new UnityEngine.Networking.UnityWebRequest(Authserver_Url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
        {
            Debug.Log("✅ Login success: " + request.downloadHandler.text);
            errorText.text = "";
            SceneManager.LoadScene(main_worldscene);
        }
        else
        {
            string errorResponse = request.downloadHandler.text;
            errorText.text = "❌ Login failed: " + (string.IsNullOrEmpty(errorResponse) ? request.error : errorResponse);
            Debug.LogWarning("Login failed: " + errorResponse);
        }
    }

    // Get the local IP address of the user
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
        catch { }
        return "Unknown";
    }

    // Payload class for login
    [System.Serializable]
    private class LoginPayload
    {
        public string username;
        public string password;
        public string ip;
    }
}
