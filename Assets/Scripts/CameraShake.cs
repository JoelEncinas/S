using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 1.5f;
    [SerializeField] private float shakeMagnitude = 0.5f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        Play();
    }


    public void Play()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float time = 0f;
        
        while(time <= shakeDuration)
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            time += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        transform.position = initialPosition;
    }
}
