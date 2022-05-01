using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 1f;
    [SerializeField] private float shakeMagnitude = 0.1f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool shakeRunning = false;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void Play()
    { 
        if(shakeRunning)
            StopCoroutine(Shake());
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        shakeRunning = true;
        float time = 0f;
        
        while(time <= shakeDuration)
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 2f)+ initialRotation.z);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = initialPosition;
        transform.rotation = initialRotation;
        shakeRunning = false;
    }
}
