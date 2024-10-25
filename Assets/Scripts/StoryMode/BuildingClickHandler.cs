using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingClickHandler : MonoBehaviour
{
    [SerializeField] private GameObject windowPrefab;
    private GameObject currentWindow;
    BoxCollider buildingCollider;

    void OnMouseDown()
    {
        if (currentWindow == null)
        {
            if (currentWindow != null)
            {
                Destroy(currentWindow);
            }

            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas != null && currentWindow == null)
            {

                // Create window at mouse position
                currentWindow = Instantiate(windowPrefab, canvas.transform);
                // Convert world position to screen position
                Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

                // If canvas is in Screen Space - Overlay mode
                if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                {
                    currentWindow.GetComponent<RectTransform>().position = screenPoint;
                }
                // If canvas is in Screen Space - Camera or World Space mode
                else
                {
                    Vector2 localPoint;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        canvas.transform as RectTransform,
                        screenPoint,
                        canvas.worldCamera,
                        out localPoint);
                    currentWindow.GetComponent<RectTransform>().localPosition = localPoint;
                }
            }

        }
    }
        
}
