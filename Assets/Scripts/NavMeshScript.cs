using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMeshScript : MonoBehaviour
{
    [SerializeField] private Transform movePositionTransform;
    private NavMeshAgent navMeshAgent;
    private void Awake() {
      navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void update() {
        navMeshAgent.destination = movePositionTransform.position;
    }
}
