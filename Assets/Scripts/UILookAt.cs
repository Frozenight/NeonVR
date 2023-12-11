using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAt : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        transform.LookAt(player);
        transform.Rotate(0, 180, 0);
    }
}
