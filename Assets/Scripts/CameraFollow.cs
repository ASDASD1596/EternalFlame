using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraFollow : MonoBehaviour
{
    private float followSpeed = 2F;
    [SerializeField] private Transform target;

     void Update()
     {
         Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f);
         transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
     }
}
