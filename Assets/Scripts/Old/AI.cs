using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{

    public GameObject _target0;
    public GameObject _target1;
    public GameObject _target2;
    private NavMeshAgent _agent;
    public int rightDes;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _target0 = GameObject.Find("Target 0");
        _target1 = GameObject.Find("Target 1");
        _target2 = GameObject.Find("Target 2");
    }
    private void Update()
    {
        if (rightDes == 0)
        {
            _agent.destination = _target0.transform.position;
        }

        if (rightDes == 1)
        {
            _agent.destination = _target1.transform.position;
        }

        if (rightDes == 2)
        {
            _agent.destination = _target2.transform.position;
        }
    }
}
