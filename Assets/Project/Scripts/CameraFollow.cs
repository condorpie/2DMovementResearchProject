using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private Vector2 dampeningZoneSize = new Vector2(1f, 1f);

    private CameraShake cameraShake;

    private void Start()
    {
        cameraShake = GetComponent<CameraShake>();
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 currentPosition = transform.position;

        if (Mathf.Abs(desiredPosition.x - currentPosition.x) > dampeningZoneSize.x)
        {
            currentPosition.x = Mathf.Lerp(currentPosition.x, desiredPosition.x, smoothSpeed * Time.deltaTime);
        }

        if (Mathf.Abs(desiredPosition.y - currentPosition.y) > dampeningZoneSize.y)
        {
            currentPosition.y = Mathf.Lerp(currentPosition.y, desiredPosition.y, smoothSpeed * Time.deltaTime);
        }

        transform.position = currentPosition;
    }

    public void TriggerShake(float duration)
    {
        cameraShake.TriggerShake(duration);
    }
}
