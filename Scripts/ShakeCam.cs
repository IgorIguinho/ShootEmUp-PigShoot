using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ShakeCam : MonoBehaviour
{
    public static ShakeCam Instance { get; private set; }

    public AnimationCurve curve;
    public float duration;
    public bool start;


    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            StartCoroutine(Shaking());
            start = false;
        }
    }

   public  IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;


        while (elapsedTime < duration)
        {
           elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition; 
    }
}
