using System.Collections;
using UnityEngine;

public class PullBullet : MonoBehaviour
{
    public float pullForce = 20f; // Adjust this value to control the strength of the pull
    public float pullDuration = 2f; // Adjust this value to control how long the pull lasts

    private Vector3 shotPosition;
    private bool isPulled = false;

    void Start()
    {
        Destroy(gameObject, 10);
        shotPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gun") || other.gameObject.CompareTag("Hand"))
            return;

        Debug.Log(other.name);
        PullObject(other.transform);
        Destroy(gameObject, pullDuration);
    }

    void PullObject(Transform target)
    {
        Vector3 pullDirection = (shotPosition - target.position).normalized;
        StartCoroutine(PullCoroutine(target, pullDirection));
    }

    IEnumerator PullCoroutine(Transform target, Vector3 pullDirection)
    {
        float elapsedTime = 0f;

        while (elapsedTime < pullDuration)
        {
            if (!isPulled)
            {
                target.position += pullDirection * pullForce * Time.deltaTime;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
