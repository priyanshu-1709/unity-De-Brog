using UnityEngine;

public class XRDevSimulatorMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform xrRig;

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float v = Input.GetAxis("Vertical");   // W/S or Up/Down

        Vector3 input = new Vector3(h, 0, v);

        if (input.magnitude > 0.01f)
        {
            Vector3 move = Camera.main.transform.TransformDirection(input);
            move.y = 0; // Keep horizontal
            xrRig.position += move.normalized * moveSpeed * Time.deltaTime;
        }
    }
}
