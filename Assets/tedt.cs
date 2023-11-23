using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class tedt : MonoBehaviour {
    public Transform target;
    public NavMeshAgent nav;

    private void Update() {
        nav.SetDestination(target.position);
    }
}
