using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _cameraOffset;

    [SerializeField] private Vector2 minValues, maxValue;

    [Range(1, 10)]
    [SerializeField] private float smoothFactor;

    void FixedUpdate()
    {
        CalculateCameraPosition();
    }

    private void CalculateCameraPosition()
    {
        Vector3 targetPosition = _player.position + _cameraOffset;

        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValues.x, maxValue.x),
            Mathf.Clamp(targetPosition.y, minValues.y, maxValue.y),
            -10f);

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);

        transform.position = smoothPosition;
    }
}
