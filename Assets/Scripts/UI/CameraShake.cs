using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 1f;
    [SerializeField] private float shakeMagnitude = 0.1f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    Coroutine shakeRunning;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void Play()
    { 
        if(shakeRunning == null)
            StopCoroutine(Shake());
        shakeRunning = StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
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
    }
}
