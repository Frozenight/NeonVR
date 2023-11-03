using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastOffEffect : BulletImpact
{
    public float blastRadius = 5.0f;
    public float blastForce = 700.0f;
    public float upwardModifier = 0.4f;
    public LayerMask blastMask;

    public override void TriggerEffect()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius, blastMask);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            AIMovement ai = nearbyObject.GetComponent<AIMovement>();
            PlayerMovementController player = nearbyObject.GetComponent<PlayerMovementController>();
            Debug.Log(nearbyObject.gameObject.name);
            if (rb != null)
            {
                // Add an explosion force to this object
                rb.AddExplosionForce(blastForce, transform.position, blastRadius, upwardModifier, ForceMode.Impulse);
            }

            if (ai != null)
            {
                ai.getBlasted(blastForce, transform.position, blastRadius, upwardModifier);
            }

            if (player != null)
            {
                player.GetBlasted(transform.position, blastForce, blastRadius);
            }
        }
    }
}

