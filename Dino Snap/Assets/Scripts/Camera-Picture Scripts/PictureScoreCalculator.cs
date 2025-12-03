using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class PictureScoreCalculator 
{
    public static Dictionary<int, int> pictureScores = new Dictionary<int, int>();

    public static void CalculatePoints()
    {
        // Find all objects in the scene that can be photographed
        Photographable[] targets = Object.FindObjectsByType<Photographable>(FindObjectsSortMode.None).ToArray();

        // Screenshotcount as dictionary key
        int screenshotIndex = ScreenshotManager.screenshotCount;
        int totalScore = 0;

        // Calculate planes of the camera's view frustum to check visibility
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        foreach (var t in targets)
        {
            if (t.targetRenderer == null) continue;

            Bounds bounds = t.targetRenderer.bounds;
            if (!GeometryUtility.TestPlanesAABB(frustumPlanes, bounds)) continue; //Outside camera view, skip

            CheckCentering(t, out float centerFactor);

            totalScore = Mathf.RoundToInt(t.centeringPoints * centerFactor + t.basePoints + t.posePoints);
        }

        pictureScores[screenshotIndex] = totalScore;
    }

    private static void CheckCentering(Photographable target, out float centerFactor)
    {
        centerFactor = 0;

        // Get the world and viewport positions 
        Vector3 worldPos = target.targetRenderer.bounds.center;
        Vector3 viewPos = Camera.main.WorldToViewportPoint(worldPos);

        // Camera center  in viewport space 0-1 
        Vector2 center = new Vector2(0.5f, 0.5f);
        Vector2 pos2D = new Vector2(viewPos.x, viewPos.y);

        // Calculate distance from the center 
        float distanceFromCenter = Vector2.Distance(center, pos2D);

        // Max center distance for points
        float maxDist = 0.7f;

        // Convert to 0-1 factor 
        centerFactor = Mathf.Clamp01(1f - (distanceFromCenter / maxDist));
    }

    // Clears all stored pictures 
    public static void ResetDictionary()
    {
        pictureScores.Clear();  
    }
}
