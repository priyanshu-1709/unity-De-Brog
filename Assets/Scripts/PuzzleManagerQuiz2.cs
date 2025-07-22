using UnityEngine;

public class PuzzleManagerQuiz2 : MonoBehaviour
{
    [Header("Blocks to Check")]
    public Transform[] blocks; // Assign top and bottom block in order

    [Header("Target Rotations (Y axis)")]
    public float[] targetYRotations; // Assign expected Y rotation for each block

    [Header("Rotation Tolerance (degrees)")]
    public float tolerance = 5f;

    [Header("Graph Panel to Show")]
    public GameObject graphPanel; // Assign your UI panel here

    private bool puzzleSolved = false;

    void Start()
    {
        if (graphPanel != null)
        {
            graphPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (puzzleSolved) return;

        if (CheckPuzzleSolved())
        {
            Debug.Log("✅ Quiz 2 Puzzle Solved!");
            puzzleSolved = true;
            if (graphPanel != null)
            {
                graphPanel.SetActive(true);
            }
        }
    }

    bool CheckPuzzleSolved()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            float currentY = blocks[i].rotation.eulerAngles.y;
            float targetY = targetYRotations[i];

            float diff = Mathf.Abs(Mathf.DeltaAngle(currentY, targetY));
            if (diff > tolerance)
            {
                return false;
            }
        }
        return true;
    }
}
