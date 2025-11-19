using UnityEngine;

public class PictureScoreCalculator : MonoBehaviour
{
    [SerializeField] private int maxScore; // Max score per category 
    private void OnEnable()
    {
        Screenshot.TakeScreenshot += CalculatePoints;
    }
    private void OnDisable()
    {
        Screenshot.TakeScreenshot -= CalculatePoints;
    }

    private void CalculatePoints()
    {

    }
}
