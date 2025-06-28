// Scripts/PlayerController.cs
using UnityEngine;
using FishNet.Object;

public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 5f;

    private void Update()
    {
        if (!IsOwner) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(h, 0, v);
        transform.Translate(move * moveSpeed * Time.deltaTime);
    }
}
