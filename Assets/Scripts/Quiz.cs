using UnityEngine;
using TMPro; // Required for TextMeshPro

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI feedbackText;

    public void CheckAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            feedbackText.text = "✅ Correct!";
        }
        else
        {
            feedbackText.text = "❌ Try Again!";
        }
    }
}
