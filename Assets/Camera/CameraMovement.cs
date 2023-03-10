using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public float cameraXMin = 0f;
    public float cameraXMax = 50f;
    public float cameraYMin = 0f;
    public float cameraYMax = 25f;

    private Vector3 velocity = Vector3.zero;

    private float halfScreenWidth;

    private void Start()
    {
        halfScreenWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
    }

    public void LateUpdate()
    {
        if (target)
        {
            // Calculate the target position with clamping
            float targetX = Mathf.Clamp(target.position.x, cameraXMin + halfScreenWidth, cameraXMax - halfScreenWidth);
            float targetY = Mathf.Clamp(target.position.y, cameraYMin + Camera.main.orthographicSize, cameraYMax - Camera.main.orthographicSize);
            Vector3 targetPosition = new Vector3(targetX, targetY, -10);

            // Smoothly move the camera to the target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
        }
    }
}
