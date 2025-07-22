using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class AtomPanelAndDoorActivator : MonoBehaviour
{
    [Header("UI + Audio")]
    public GameObject atomInfoPanel;
    public AudioSource voiceover;
    public AudioSource warningAudio;

    [Header("Door Setup")]
    public Transform leftDoor;
    public Transform rightDoor;
    public Vector3 leftOpenOffset = new Vector3(-2f, 0f, 0f);
    public Vector3 rightOpenOffset = new Vector3(2f, 0f, 0f);
    public float openSpeed = 2f;

    private Vector3 leftClosedPos;
    private Vector3 rightClosedPos;
    private Vector3 leftTargetPos;
    private Vector3 rightTargetPos;

    private bool hasActivated = false;
    private bool warningStarted = false;

    void Start()
    {
        leftClosedPos = leftDoor.position;
        rightClosedPos = rightDoor.position;
        leftTargetPos = leftClosedPos + leftOpenOffset;
        rightTargetPos = rightClosedPos + rightOpenOffset;

        // Ensure the warning sound is not playing at start
        if (warningAudio != null && warningAudio.isPlaying)
        {
            warningAudio.Stop();
        }
    }

    void Update()
    {
        // On Q press, activate sequence once
        if (!hasActivated && Keyboard.current.qKey.wasPressedThisFrame)
        {
            hasActivated = true;

            if (atomInfoPanel != null)
                atomInfoPanel.SetActive(true);

            if (voiceover != null)
                voiceover.Play();

            // Start delayed warning sound (5 seconds after door starts opening)
            if (!warningStarted)
            {
                warningStarted = true;
                StartCoroutine(PlayWarningAfterDelay(5f));
            }
        }

        // Door opening animation
        if (hasActivated)
        {
            if (leftDoor != null)
                leftDoor.position = Vector3.MoveTowards(leftDoor.position, leftTargetPos, openSpeed * Time.deltaTime);

            if (rightDoor != null)
                rightDoor.position = Vector3.MoveTowards(rightDoor.position, rightTargetPos, openSpeed * Time.deltaTime);
        }
    }

    IEnumerator PlayWarningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (warningAudio != null && !warningAudio.isPlaying)
        {
            warningAudio.Play();
        }
    }
}
