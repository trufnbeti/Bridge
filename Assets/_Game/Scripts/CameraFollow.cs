using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;
    

    private void LateUpdate() {
        Vector3 pos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
    }
}
