using UnityEngine;
using UnityEngine.InputSystem;

public class OrthoCameraMovement : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 20f;
    [SerializeField] private float panSpeed = 10f;
    
    [Header("Camera Orientation")]
    [SerializeField] private Vector3 cameraAngle = new Vector3(45f, 45f, 0f); // Default isometric-like angle
    [SerializeField] private float heightOffset = 10f; // Initial camera height
    
    private Camera mainCamera;
    private Vector2 lastMousePosition;
    private bool isDragging = false;
    private Plane groundPlane;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        if (!mainCamera.orthographic)
        {
            Debug.LogWarning("Camera is not set to orthographic mode!");
            mainCamera.orthographic = true;
        }

        // Initialize camera position and rotation
        transform.rotation = Quaternion.Euler(cameraAngle);
        transform.position = new Vector3(0, heightOffset, 0);
        
        // Initialize the ground plane for ray casting
        groundPlane = new Plane(Vector3.up, Vector3.zero);
    }

    private void Update()
    {
        HandleZoom();
        HandlePanning();
    }

    private void HandleZoom()
    {
        float scrollValue = Mouse.current.scroll.ReadValue().y;
        
        if (scrollValue != 0)
        {
            float newSize = mainCamera.orthographicSize - (scrollValue * zoomSpeed * Time.deltaTime);
            mainCamera.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
            
            // Optionally adjust height offset based on zoom level
            // heightOffset = baseHeight * (mainCamera.orthographicSize / minZoom);
        }
    }

    private void HandlePanning()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            isDragging = true;
            lastMousePosition = Mouse.current.position.ReadValue();
        }
        else if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector2 currentMousePosition = Mouse.current.position.ReadValue();
            Vector2 mouseDelta = currentMousePosition - lastMousePosition;

            // Convert screen point to ray for current and last position
            Ray rayFromCurrent = mainCamera.ScreenPointToRay(currentMousePosition);
            Ray rayFromLast = mainCamera.ScreenPointToRay(lastMousePosition);

            // Calculate intersection points with the ground plane
            if (groundPlane.Raycast(rayFromCurrent, out float currentDistance) &&
                groundPlane.Raycast(rayFromLast, out float lastDistance))
            {
                Vector3 currentWorldPoint = rayFromCurrent.GetPoint(currentDistance);
                Vector3 lastWorldPoint = rayFromLast.GetPoint(lastDistance);
                
                // Calculate the world space movement
                Vector3 worldSpaceMovement = lastWorldPoint - currentWorldPoint;
                
                // Apply movement
                transform.position += worldSpaceMovement * panSpeed * Time.deltaTime;
            }

            lastMousePosition = currentMousePosition;
        }
    }

    // Optional: Method to set camera angle dynamically
    public void SetCameraAngle(Vector3 newAngle)
    {
        cameraAngle = newAngle;
        transform.rotation = Quaternion.Euler(cameraAngle);
    }

    // Optional: Method to focus camera on a specific world position
    public void FocusOn(Vector3 worldPosition)
    {
        transform.position = new Vector3(worldPosition.x, heightOffset, worldPosition.z);
    }
}