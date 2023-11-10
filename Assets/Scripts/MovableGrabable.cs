using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableGrabable : MonoBehaviour
{
    public float speed = 1.0f; // Speed of the movement

    void Update()
    {
        // Move the object upwards and to the right each frame
        transform.position += new Vector3(-speed * Time.deltaTime, speed * Time.deltaTime, 0);
    }
}
