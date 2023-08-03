using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float magnitude = 0.1f;
    [SerializeField] private float range = 0.2f;
    [SerializeField] private float shakeSpeed = 10f;

    public bool IsShaking { get; private set; }

    public IEnumerator Shake(float duration)
    {
        IsShaking = true;
        Vector3 originalPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float x = Random.Range(-range, range) * magnitude;
            float y = Random.Range(-range, range) * magnitude;
            Vector3 targetPosition = originalPosition + new Vector3(x, y, 0);

            transform.position = Vector3.Lerp(transform.position, targetPosition, shakeSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
        IsShaking = false;
    }

    public void TriggerShake(float duration)
    {
        if (!IsShaking)
        {
            StartCoroutine(Shake(duration));
        }
    }
}
