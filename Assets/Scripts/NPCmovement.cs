using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCmovement : MonoBehaviour
{
     public Vector3 pointA = new Vector3(0, 0, 0);
     public Vector3 pointB = new Vector3(1, 0, 0);

     public float starttime;
     public float walkduration = 1;
     void OnEnable() 
     {
       starttime = Time.time;
     }
     void Update()
     {
         transform.localPosition = Vector3.Lerp(pointA, pointB, Mathf.Clamp01((Time.time-starttime)/walkduration));
     }
}
