using UnityEngine;

public class ScreenshotHooks : MonoBehaviour
{
    private void OnEnable()
    {
        ScreenshotManager.TakeScreenshot += PictureScoreCalculator.CalculatePoints;
        ScreenshotManager.DeleteScreenshot += PictureScoreCalculator.ResetDictionary;

        ScreenshotManager.TakeScreenshot += ScreenshotManager.CaptureScreenshot;
        ScreenshotManager.DeleteScreenshot += ScreenshotManager.DeleteAllScreenshots;
    }

    private void OnDisable()
    {
        ScreenshotManager.TakeScreenshot -= PictureScoreCalculator.CalculatePoints;
        ScreenshotManager.DeleteScreenshot -= PictureScoreCalculator.ResetDictionary;

        ScreenshotManager.TakeScreenshot -= ScreenshotManager.CaptureScreenshot;
        ScreenshotManager.DeleteScreenshot -= ScreenshotManager.DeleteAllScreenshots;
    }
}
