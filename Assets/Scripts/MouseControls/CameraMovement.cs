using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDrag : MonoBehaviour
{

    private Camera _mainCamera;
    public Transform cameraFocusPlayer;

    private bool _isDragging;

    public float mouseSensitivity_X;
    public float mouseSensitivity_Y;
    public float mouseSensitivity_Zoom;
    private float sensX;
    private float sensY;
    private float sensZoom;

    public float distFromFocusPlayer;
    private float distCam;

    public float minZoom;
    public float maxZoom;
    public float XLimits;
    public float ZLimits;

    private void Start()
    {
        _mainCamera = Camera.main;
        transform.position = cameraFocusPlayer.position - transform.forward * distFromFocusPlayer;
        sensX = mouseSensitivity_X;
        sensY = mouseSensitivity_Y;
        sensZoom = mouseSensitivity_Zoom;
        distCam = distFromFocusPlayer;
    }

    public void OnDrag(InputAction.CallbackContext ctx)
    {
        _isDragging = ctx.started || ctx.performed;
    }


    private void LateUpdate()
    {

        if (_isDragging) // Movimiento de camara arrastrando el mouse
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Vector3 rightMovement = _mainCamera.transform.right * -mouseX * sensX;
            Vector3 forwardDirection = Vector3.ProjectOnPlane(_mainCamera.transform.forward, Vector3.up).normalized;
            Vector3 forwardMovement = forwardDirection * -mouseY * sensY;

            cameraFocusPlayer.position += rightMovement + forwardMovement;
            if (cameraFocusPlayer.position.x < -XLimits) { cameraFocusPlayer.position = new Vector3(-XLimits, cameraFocusPlayer.position.y, cameraFocusPlayer.position.z); }
            else if (cameraFocusPlayer.position.x > XLimits) { cameraFocusPlayer.position = new Vector3(XLimits, cameraFocusPlayer.position.y, cameraFocusPlayer.position.z); }
            if (cameraFocusPlayer.position.z < -ZLimits) { cameraFocusPlayer.position = new Vector3(cameraFocusPlayer.position.x, cameraFocusPlayer.position.y, -ZLimits); }
            else if (cameraFocusPlayer.position.z > ZLimits) { cameraFocusPlayer.position = new Vector3(cameraFocusPlayer.position.x, cameraFocusPlayer.position.y, ZLimits); }

            float t = Mathf.InverseLerp(minZoom, maxZoom, distCam);
            sensX = Mathf.Lerp(mouseSensitivity_X * 0.5f, mouseSensitivity_X * 3f, t);
            sensY = Mathf.Lerp(mouseSensitivity_Y * 0.5f, mouseSensitivity_Y * 3f, t);

        }
        else
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0) // Zoom con rueda de mouse
            {

                distCam += Input.GetAxis("Mouse ScrollWheel") * -sensZoom;

                if (distCam < minZoom) { distCam = minZoom; }
                else if (distCam > maxZoom) { distCam = maxZoom; }
            }
        }

        transform.position = cameraFocusPlayer.position - transform.forward * distCam;
    }

}