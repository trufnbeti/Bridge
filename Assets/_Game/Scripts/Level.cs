using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Stage> stages;
    public NavMeshData navMeshData;
    public int numBot = 3;
    public Transform startPoint;
    public Transform finishPoint;
    
    public void OnInit() {
        foreach (var stage in stages) {
            stage.OnInit();
        }
    }

}
