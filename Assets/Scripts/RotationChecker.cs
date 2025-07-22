using UnityEngine;

public class RotationChecker : MonoBehaviour
{
    [Header("Cubes to Check")]
    public Transform[] cubes;

    [Header("Target Rotations (Euler Angles)")]
    public Vector3[] targetRotations;

    [Header("Rotation Tolerance (degrees)")]
    public float tolerance = 5f;

    [Header("Graph Panel UI")]
    public GameObject graphPanel;

    private bool panelShown = false;

    void Start()
    {
        if (graphPanel != null)
        {
            graphPanel.SetActive(false);
        }
    }

    void Update()
    {
        bool matched = AllCubesMatchRotation();

        if (matched && !panelShown)
        {
            ShowGraphPanel();
        }
        else if (!matched && panelShown)
        {
            HideGraphPanel();
        }
    }

    bool AllCubesMatchRotation()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            if (i >= targetRotations.Length) return false;

            Vector3 currentEuler = cubes[i].rotation.eulerAngles;
            Vector3 targetEuler = targetRotations[i];

            if (!AnglesClose(currentEuler.x, targetEuler.x) ||
                !AnglesClose(currentEuler.y, targetEuler.y) ||
                !AnglesClose(currentEuler.z, targetEuler.z))
            {
                return false;
            }
        }
        return true;
    }

    bool AnglesClose(float a, float b)
    {
        float diff = Mathf.Abs(Mathf.DeltaAngle(a, b));
        return diff <= tolerance;
    }

    void ShowGraphPanel()
    {
        if (graphPanel != null)
        {
            graphPanel.SetActive(true);
            panelShown = true;
            Debug.Log("✅ Correct! Graph Panel Activated.");
        }
    }

    void HideGraphPanel()
    {
        if (graphPanel != null)
        {
            graphPanel.SetActive(false);
            panelShown = false;
            Debug.Log("❌ Not matching. Graph Panel hidden.");
        }
    }
}
