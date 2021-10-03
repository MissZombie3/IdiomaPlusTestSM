using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirPersonCamera : MonoBehaviour
{
    public Vector3 offset;
    [Range(0, 1)] public float lerpValue;
    
    private Transform target;
    public float sensitivity;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpValue);
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * sensitivity, Vector3.up) * offset;

        transform.LookAt(target);
    }
}
