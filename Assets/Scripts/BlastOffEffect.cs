using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastOffEffect : BulletImpact
{
    public float blastRadius = 5.0f;
    public float blastForce = 700.0f;
    public float upwardModifier = 0.4f; // To give a slightly upward force
    public LayerMask blastMask; // A mask to determine which objects are affected by the blast. Set to everything by default.

    // This function can be called when the grenade "explodes"
    public override void TriggerEffect()
    {
        // Find all the colliders in the blast radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius, blastMask);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            AIMovement ai = nearbyObject.GetComponent<AIMovement>();
            if (rb != null)
            {
                // Add an explosion force to this object
                rb.AddExplosionForce(blastForce, transform.position, blastRadius, upwardModifier, ForceMode.Impulse);
            }

            if (ai != null)
            {
                ai.getBlasted(blastForce, transform.position, blastRadius, upwardModifier);
            }
        }
    }
}

