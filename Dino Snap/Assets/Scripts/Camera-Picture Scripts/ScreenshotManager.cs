using System;
using System.IO;
using UnityEngine;

public static class ScreenshotManager 
{
    // Called when a screenshot should be taken 
    public static event Action TakeScreenshot;

    // Called when a session ends and you have to delete all the screenshot data
    public static event Action DeleteScreenshot;

    // Keeps track of screenshots taken in this session 
    public static int screenshotCount;

    public static void RequestScreenshot() // 
    {
        TakeScreenshot?.Invoke();
    }

    public static void DeleteFiles() // Deletes the screenshots and clears other values 
    {
        DeleteScreenshot?.Invoke();
    }

    // Takes a screenshot and sends it to a temporary cache file, this way when the application ends all files automaticaly get deleted
    public static void CaptureScreenshot()
    {
        screenshotCount++;
        var path = Path.Combine(Application.temporaryCachePath, screenshotCount.ToString());

        ScreenCapture.CaptureScreenshot(path);
        Debug.Log($"Saved screenshot to: {path}");
    }
  
    // Whenever a new session/run starts, delete all files 
    public static void DeleteAllScreenshots() 
    {
        string folder = Application.temporaryCachePath;
        string[] files = Directory.GetFiles(folder);

        foreach (string file in files)
        {
            File.Delete(file);
        }
        
        Debug.Log("All screenshots deleted.");
    }

    // Resets screenshot counter 
    public static void ResetCount() 
    {
        screenshotCount = 0;
    }
}
