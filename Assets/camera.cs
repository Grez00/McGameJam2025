using System;
using UnityEngine;

public class CameraRunnerScript : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.2f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        Vector3 targetPosition = player.position + offset;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -20, 20);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -20, 20);
        targetPosition.z = -20;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
    }
}
