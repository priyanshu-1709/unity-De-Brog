using UnityEngine;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public RotatingBlock topBlock;
    public RotatingBlock middleBlock;
    public RotatingBlock bottomBlock;

    public GameObject graphPanel;
    public TMP_Text feedbackText;

    private bool isSolved = false;

    void Start()
    {
        graphPanel.SetActive(false);
        feedbackText.text = "";
    }

    public void CheckSolution()
    {
        if (isSolved) return;

        string top = topBlock.GetCurrentLetter();
        string middle = middleBlock.GetCurrentLetter();
        string bottom = bottomBlock.GetCurrentLetter();

        Debug.Log($"CheckSolution: Top={top}, Middle={middle}, Bottom={bottom}");

        if (top == "H" && middle == "M" && bottom == "V")
        {
            isSolved = true;
            feedbackText.text = "Correct!";
            graphPanel.SetActive(true);
        }
        else
        {
            feedbackText.text = "Keep rotating!";
        }
    }
}
