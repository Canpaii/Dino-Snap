using System;
using System.IO;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    public static event Action TakeScreenshot;

    private int screenshotCount;

    private void OnEnable()
    {
        TakeScreenshot += CaptureScreenshot;
    }

    private void OnDisable()
    {
        TakeScreenshot -= CaptureScreenshot;
    }

    public static void RequestScreenshot()
    {
        TakeScreenshot?.Invoke();
    }

    private void CaptureScreenshot()
    {
        screenshotCount++;
        var path = Path.Combine(Application.temporaryCachePath, screenshotCount.ToString());

        ScreenCapture.CaptureScreenshot(path);
        Debug.Log($"Saved screenshot to: {path}");
    }
  
    // Whenever a new session/run starts, delete all files 
    public static void DeleteAllScreenshots() 
    {
        string folder = Application.temporaryCachePath; // or persistentDataPath
        string[] files = Directory.GetFiles(folder);

        foreach (string file in files)
        {
            File.Delete(file);
        }
        
        Debug.Log("All screenshots deleted.");
    }
    
    public void ResetCount()
    {
        screenshotCount = 0;
    }
}
