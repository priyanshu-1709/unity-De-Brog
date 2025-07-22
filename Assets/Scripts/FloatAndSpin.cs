using UnityEngine;

public class FloatAndSpin : MonoBehaviour
{
    public float floatSpeed = 0.5f;
    public float rotationSpeed = 30f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * floatSpeed) * 0.1f;
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
