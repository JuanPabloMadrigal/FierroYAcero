using UnityEngine;
using UnityEngine.InputSystem;

public class OrthoCameraMovement : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 20f;
    [SerializeField] private float panSpeed = 10f;

    [Header("Smoothing Settings")]
    [SerializeField] private float movementSmoothTime = 0.2f;
    [SerializeField] private float zoomSmoothTime = 0.15f;
    [SerializeField] private float rotationSmoothTime = 0.15f;

    [Header("Camera Orientation")]
    [SerializeField] private Vector3 cameraAngle = new Vector3(45f, 45f, 0f);
    [SerializeField] private float heightOffset = 10f;

    private Camera mainCamera;
    private Vector2 lastMousePosition;
    private bool isDragging = false;
    private Plane groundPlane;

    // Smoothing variables
    private Vector3 currentVelocity;
    private Vector3 targetPosition;
    private float zoomVelocity;
    private float targetZoom;
    private Vector3 rotationVelocity;
    private Vector3 targetRotation;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        if (!mainCamera.orthographic)
        {
            Debug.LogWarning("Camera is not set to orthographic mode!");
            mainCamera.orthographic = true;
        }

        // Initialize positions and rotations
        transform.rotation = Quaternion.Euler(cameraAngle);
        transform.position = new Vector3(0, heightOffset, 0);
        targetPosition = transform.position;
        targetZoom = mainCamera.orthographicSize;
        targetRotation = cameraAngle;

        groundPlane = new Plane(Vector3.up, Vector3.zero);
    }

    private void Update()
    {
        HandleZoom();
        HandlePanning();
        UpdateCameraTransform();
    }

    private void HandleZoom()
    {
        float scrollValue = Mouse.current.scroll.ReadValue().y;

        if (scrollValue != 0)
        {
            // Update target zoom instead of directly modifying camera
            float newZoom = targetZoom - (scrollValue * zoomSpeed * Time.deltaTime);
            targetZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);
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

            Ray rayFromCurrent = mainCamera.ScreenPointToRay(currentMousePosition);
            Ray rayFromLast = mainCamera.ScreenPointToRay(lastMousePosition);

            if (groundPlane.Raycast(rayFromCurrent, out float currentDistance) &&
                groundPlane.Raycast(rayFromLast, out float lastDistance))
            {
                Vector3 currentWorldPoint = rayFromCurrent.GetPoint(currentDistance);
                Vector3 lastWorldPoint = rayFromLast.GetPoint(lastDistance);

                Vector3 worldSpaceMovement = lastWorldPoint - currentWorldPoint;

                // Update target position instead of directly moving camera
                targetPosition += worldSpaceMovement * panSpeed * Time.deltaTime;
            }

            lastMousePosition = currentMousePosition;
        }
    }

    private void UpdateCameraTransform()
    {
        // Smooth position movement
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref currentVelocity,
            movementSmoothTime
        );

        // Smooth zoom
        mainCamera.orthographicSize = Mathf.SmoothDamp(
            mainCamera.orthographicSize,
            targetZoom,
            ref zoomVelocity,
            zoomSmoothTime
        );

        // Smooth rotation
        Vector3 currentRotation = transform.rotation.eulerAngles;
        Vector3 smoothedRotation = Vector3.SmoothDamp(
            currentRotation,
            targetRotation,
            ref rotationVelocity,
            rotationSmoothTime
        );
        transform.rotation = Quaternion.Euler(smoothedRotation);
    }

    public void FocusOn(Vector3 worldPosition)
    {
        // Update target position instead of directly moving camera
        targetPosition = new Vector3(worldPosition.x, heightOffset, worldPosition.z);
    }

    
}