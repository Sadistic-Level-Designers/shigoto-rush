using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCmovement : MonoBehaviour
{
     public Vector3 pointA = new Vector3(0, 0, 0);
     public Vector3 pointB = new Vector3(0, 0, 0);

     public float speed = 0.25f;
     void OnEnable() 
     {
        timeOffset = Random.Range(0, 10f);
     }
     private float pt = 0;
     private float timeOffset = 0;
     void Update()
     {
        float t = Mathf.Sin( Mathf.PingPong((Time.time + timeOffset) * speed, 1f) * Mathf.PI/2 );
         transform.localPosition = Vector3.Lerp(pointA, pointB, t);


         transform.localEulerAngles = new Vector3(0f, 180f * ((pt - t) > 0 ? 1f : 0f), 0f);
         pt = t;
     }
}
