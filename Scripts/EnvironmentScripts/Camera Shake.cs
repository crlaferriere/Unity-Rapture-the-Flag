 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    /* Transform of the camera to shake. */
    // References for camera position, shake duration, and the amplitude of the shake.
    public Transform camTransform;
    public float shakeDuration = 0f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 0f;
    // Reference for the original position of the camera
    Vector3 originalPos;
    // Sets up the camera if it is null
    void Awake()
    {
        if (camTransform == null)
            camTransform = GetComponent(typeof(Transform)) as Transform;
    }
    // Set the original position
    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }
    // Set up the shaking using ints and registered positions
    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
            shakeDuration = 0f;
    }
}