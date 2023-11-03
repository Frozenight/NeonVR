using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresencePhysics : MonoBehaviour
{
    public Transform target;
    private Rigidbody rb;
    public Renderer nonPhysicalHandRenderer;
    public float showNonPhysicalHandDistance = 0.2f;
    public float positionLerpRate = 100f;
    public float rotationLerpRate = 100f;
    private Collider[] handColliders;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 200;
        handColliders = GetComponentsInChildren<Collider>();

        // Set the interpolation mode
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    public void EnableHandCollider()
    {
        foreach (var item in handColliders)
        {
            item.enabled = true;
        }
    }

    public void EnableDelayedHandCollider(float delay)
    {
        Invoke("EnableHandCollider", delay);
    }

    public void DisableHandCollider()
    {
        foreach (var item in handColliders)
        {
            item.enabled = false;
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        nonPhysicalHandRenderer.enabled = distance > showNonPhysicalHandDistance;
    }

    void FixedUpdate()
    {
        Vector3 positionDelta = target.position - transform.position;
        rb.velocity = positionDelta * positionLerpRate;

        Quaternion rotationDelta = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDelta.ToAngleAxis(out float angleInDegrees, out Vector3 rotationAxis);
        rotationAxis.Normalize();

        Vector3 rotationDifferenceInDegrees = angleInDegrees * rotationAxis;
        rb.angularVelocity = (rotationDifferenceInDegrees * Mathf.Deg2Rad) * rotationLerpRate;
    }
}
