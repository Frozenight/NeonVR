using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // Adjust the speed of rotation as needed

    Collider[] rigColliders;
    Rigidbody[] rigRigidbodies;
    public Collider mainCollider;

    private bool _isDead = false;
    private Animator anim;

    void Start()
    {
        rigColliders = GetComponentsInChildren<Collider>();
        rigRigidbodies = GetComponentsInChildren<Rigidbody>();
        anim = GetComponent<Animator>();
        StopRagdoll();
        mainCollider.enabled = true;
    }

    void Update()
    {
        if (_isDead)
            return;
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    public void getBlasted(float blastForce, Vector3 explosionPosition, float blastRadius, float upwardModifier)
    {
        _isDead = true;
        anim.enabled = false;
        StartRagdoll();

        foreach (Rigidbody rb in rigRigidbodies)
        {
            rb.AddExplosionForce(blastForce, explosionPosition, blastRadius, upwardModifier, ForceMode.Impulse);
        }
    }

    private void StartRagdoll()
    {
        foreach (Rigidbody rb in rigRigidbodies)
        {
            rb.isKinematic = false;
        }
        foreach (Collider col in rigColliders)
        {
            col.enabled = true;
        }
    }

    private void StopRagdoll()
    {
        foreach (Rigidbody rb in rigRigidbodies)
        {
            rb.isKinematic = true;
        }
        foreach (Collider col in rigColliders)
        {
            col.enabled = false;
        }
    }
}
