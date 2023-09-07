using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.Misc
{
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
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            return results.Count > 0 && results.Any(result => result.gameObject.layer == INTERACTABLE_UI_LAYER);
        }
    }
}
