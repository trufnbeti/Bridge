using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Stage[] stages;
    
    [System.Serializable]
    private struct Stage {
        public int rows, columns;
        public Transform parent;
    }

    private int currentStage;

    private void Start() {
        currentStage = 1;
        SpawnBrick();
    }

    private void SpawnBrick() {
        int rows = stages[currentStage - 1].rows / 2;
        int cols = stages[currentStage - 1].columns / 2;
        Transform parent = stages[currentStage - 1].parent;
        for (int row = rows - 1; row > 0; row -= 2) {
            for (int col = cols - 1; col > 0; col -= 2) {
                Pool.Ins.Spawn<PlatformBrick>(PoolType.PlatformBrick, new Vector3(row, parent.position.y, col), Quaternion.identity).transform.SetParent(parent);
                Pool.Ins.Spawn<PlatformBrick>(PoolType.PlatformBrick, new Vector3(row, parent.position.y, -col), Quaternion.identity).transform.SetParent(parent);
                Pool.Ins.Spawn<PlatformBrick>(PoolType.PlatformBrick, new Vector3(-row, parent.position.y, col), Quaternion.identity).transform.SetParent(parent);
                Pool.Ins.Spawn<PlatformBrick>(PoolType.PlatformBrick, new Vector3(-row, parent.position.y, -col), Quaternion.identity).transform.SetParent(parent);
            }
        }
    }

}
