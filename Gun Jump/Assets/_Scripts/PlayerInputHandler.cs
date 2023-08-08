using UnityEngine;

public static class PlayerInputHandler
{
    public static bool IsMouseButtonDownThisFrame()
        => Input.GetMouseButtonDown(0);
}
