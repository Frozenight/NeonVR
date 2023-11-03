using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [System.NonSerialized] public BulletImpact blastOffEffectObj;

    void Start()
    {
        Destroy(gameObject, 5);
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
        if (blastOffEffectObj != null)
        {
            BulletImpact newBlastEffect = Instantiate(blastOffEffectObj, transform.position, Quaternion.identity);
            newBlastEffect.TriggerEffect();
        }

        Destroy(gameObject);
    }
}
