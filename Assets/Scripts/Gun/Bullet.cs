using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [System.NonSerialized] public BulletImpact blastOffEffectObj;

    void Start()
    {
        Destroy(gameObject, 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gun") || collision.gameObject.CompareTag("Hand"))
            return;

        if (blastOffEffectObj != null)
        {
            BulletImpact newBlastEffect = Instantiate(blastOffEffectObj, transform.position, Quaternion.identity);
            newBlastEffect.TriggerEffect();
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gun") || other.gameObject.CompareTag("Hand"))
            return;

        if (blastOffEffectObj != null)
            if (blastOffEffectObj != null)
        {
            BulletImpact newBlastEffect = Instantiate(blastOffEffectObj, transform.position, Quaternion.identity);
            newBlastEffect.TriggerEffect();
        }

        Destroy(gameObject);
    }
}
