using UnityEngine;
using FishNet.Managing;

public class NetworkBootstrap : MonoBehaviour
{
    public NetworkManager networkManager;

    private void Start()
    {
        // Start host mode: server + client
        networkManager.ServerManager.StartConnection();
        networkManager.ClientManager.StartConnection();
    }
}
