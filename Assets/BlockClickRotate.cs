using UnityEngine;

public class BlockClickRotate : MonoBehaviour
{
    public float rotationAmount = 90f;

    void OnMouseDown()
    {
        transform.Rotate(0, rotationAmount, 0);
    }
}
