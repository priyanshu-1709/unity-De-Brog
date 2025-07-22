using UnityEngine;
using TMPro;

public class WavelengthCalculator : MonoBehaviour
{
    public TMP_InputField massInput;
    public TMP_InputField velocityInput;
    public TMP_Text resultText;

    private float h = 6.626f * Mathf.Pow(10, -34); // Planck’s constant

    public void CalculateWavelength()
    {
        if (float.TryParse(massInput.text, out float mass) && float.TryParse(velocityInput.text, out float velocity))
        {
            if (mass > 0 && velocity > 0)
            {
                float lambda = h / (mass * velocity);
                resultText.text = $"λ = {lambda:e2} meters";
            }
            else
            {
                resultText.text = "Mass and velocity must be > 0";
            }
        }
        else
        {
            resultText.text = "Enter valid numbers!";
        }
    }
}
