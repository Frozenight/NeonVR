using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletImpact blastOffEffectObj;

    void Start()
    {
        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gun"))
            return;
        if (blastOffEffectObj != null)
        {
            Instantiate(blastOffEffectObj, transform.position, Quaternion.identity);
            blastOffEffectObj.TriggerEffect();
        }

        if (collision.gameObject.CompareTag("Player"))
            Debug.Log("Hit");

        Destroy(gameObject);
    }
}
