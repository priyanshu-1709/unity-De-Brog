using UnityEngine;

public class SciFiDoubleDoor : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    // Increased outward distance on Z axis
    public Vector3 leftOpenOffset = new Vector3(0f, 0f, -10f);   // Left door moves back
    public Vector3 rightOpenOffset = new Vector3(0f, 0f, 10f);   // Right door moves forward

    public float openSpeed = 2f;

    private Vector3 leftClosedPos, rightClosedPos;
    private Vector3 leftTargetPos, rightTargetPos;
    private bool isOpen = false;

    void Start()
    {
        leftClosedPos = leftDoor.position;
        rightClosedPos = rightDoor.position;
        leftTargetPos = leftClosedPos;
        rightTargetPos = rightClosedPos;
    }

    void Update()
    {
        leftDoor.position = Vector3.Lerp(leftDoor.position, leftTargetPos, Time.deltaTime * openSpeed);
        rightDoor.position = Vector3.Lerp(rightDoor.position, rightTargetPos, Time.deltaTime * openSpeed);
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            leftTargetPos = leftClosedPos + leftOpenOffset;
            rightTargetPos = rightClosedPos + rightOpenOffset;
        }
        else
        {
            leftTargetPos = leftClosedPos;
            rightTargetPos = rightClosedPos;
        }
    }
}
