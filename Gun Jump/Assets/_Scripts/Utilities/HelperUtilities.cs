using UnityEngine;

public static class HelperUtilities
{
    private static Camera s_camera;

    public static bool IsObjectWithingScreenBoundaries(Vector3 objectWorldSpace)
    {
        if (s_camera == null)
            s_camera = Camera.main;

        Vector3 objectScreenPosition = s_camera.WorldToScreenPoint(objectWorldSpace);

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        bool isObjectWithinScreen =
            objectScreenPosition.x >= 0 && objectScreenPosition.x <= screenWidth &&
            objectScreenPosition.y >= 0 && objectScreenPosition.y <= screenHeight;

        return isObjectWithinScreen;
    }
}
