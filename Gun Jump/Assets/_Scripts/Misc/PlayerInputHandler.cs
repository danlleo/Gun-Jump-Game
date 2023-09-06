using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class PlayerInputHandler
{
    private const int INTERACTABLE_UI_LAYER = 31;

    public static bool IsMouseButtonDownThisFrame()
        => Input.GetMouseButtonDown(0);

    public static bool IsMouseButtonHeldThisFrame()
        => Input.GetMouseButton(0);

    public static bool IsMouseButtonUpThisFrame()
        => Input.GetMouseButtonUp(0);

    public static bool IsMouseOverInteractableUIElement()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        if (results.Count > 0)
        {
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.layer == INTERACTABLE_UI_LAYER)
                    return true;
            }
        }

        return false;
    }
}
