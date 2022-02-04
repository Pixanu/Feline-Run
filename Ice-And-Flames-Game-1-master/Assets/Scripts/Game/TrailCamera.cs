using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCamera : MonoBehaviour
{
    public Transform target;
    public float trailDistance = 5.0f;
    public float heightOffset = 3.0f;
    public float cameraDelay = 0.02f;

    /*
    private float transition = 0.0f;
    private float animationDuration = 2.0f;
    private Vector3 animationOffset = new Vector3(0, 5, 5);
    */

    void Update()
    {
        Vector3 followPos = target.position - target.forward * trailDistance;

        followPos.y += heightOffset;
        transform.position += (followPos - transform.position) * cameraDelay;

        transform.LookAt(target.transform);
    }
}
