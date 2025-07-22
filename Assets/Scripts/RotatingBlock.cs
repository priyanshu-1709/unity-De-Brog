using UnityEngine;

public class RotatingBlock : MonoBehaviour
{
    // These are your letters in clockwise rotation
    public string[] letters = new string[4] { "H", "M", "V", "X" };

    // Degrees per rotation
    public float rotationStep = 90f;

    // Smooth rotation optional
    private bool isRotating = false;

    // Current absolute Y rotation
    private float targetAngle = 0;

    void Start()
    {
        targetAngle = transform.localEulerAngles.y;
    }

    void Update()
    {
        if (isRotating)
        {
            float currentY = transform.localEulerAngles.y;
            float newY = Mathf.MoveTowardsAngle(currentY, targetAngle, 300 * Time.deltaTime);
            transform.localEulerAngles = new Vector3(0, newY, 0);

            if (Mathf.Abs(Mathf.DeltaAngle(newY, targetAngle)) < 0.1f)
            {
                transform.localEulerAngles = new Vector3(0, targetAngle, 0);
                isRotating = false;
            }
        }
    }

    public void Rotate()
    {
        if (isRotating) return;
        targetAngle += rotationStep;
        targetAngle %= 360;
        isRotating = true;
    }

    public string GetCurrentLetter()
    {
        // Snap rotation to 0-360
        float y = transform.localEulerAngles.y % 360;

        // Determine which of 4 faces is in front
        int faceIndex = Mathf.RoundToInt(y / 90f) % 4;
        return letters[faceIndex];
    }
}
