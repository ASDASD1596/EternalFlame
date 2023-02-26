using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private float followSpeed = 14F;
    [SerializeField] private Transform target;

    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y, 10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
