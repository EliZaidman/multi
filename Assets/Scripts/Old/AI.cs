using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{
   
    private GameObject _target;
    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _target = GameObject.Find("Tower2");
    }
    private void Update()
    {
        _agent.destination = _target.transform.position;
    }
}
