using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    public float parallaxEffectMultiplier;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    private void Update()
    {
        float deltaX = cameraTransform.position.x - lastCameraPosition.x;
        float backgroundTargetX = transform.position.x + (deltaX * parallaxEffectMultiplier);

        Vector3 backgroundTargetPosition = new Vector3(backgroundTargetX, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, backgroundTargetPosition, Time.deltaTime);

        lastCameraPosition = cameraTransform.position;
    }
}
