using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggle : MonoBehaviour
{
    private float localNoise;

    private Vector3 angularInit;
    public Vector3 angularWiggle = Vector3.zero;
    public float angularWiggleSpeed = 1f;

    private Vector3 positionInit;
    public Vector3 positionWiggle = Vector3.zero;
    public float positionWiggleSpeed = 1f;

    private Vector3 scaleInit;
    public Vector3 scaleWiggle = Vector3.zero;
    public float scaleWiggleSpeed = 1f;

    void Awake() {
        localNoise = Random.Range(0f, 10f);
        angularInit = transform.localEulerAngles;
        positionInit = transform.localPosition;
        scaleInit = transform.localScale;
    }

    void Update() {
        float ad = Mathf.Sin((Time.time + localNoise) * angularWiggleSpeed * Mathf.PI);
        float pd = Mathf.Sin((Time.time + localNoise) * positionWiggleSpeed * Mathf.PI);
        float sd = Mathf.Sin((Time.time + localNoise) * scaleWiggleSpeed * Mathf.PI);

        transform.localEulerAngles = angularInit + angularWiggle * ad;
        transform.localPosition = positionInit + positionWiggle * pd;
        transform.localScale = scaleInit + scaleWiggle * sd;
    }
}
