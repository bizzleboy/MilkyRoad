using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleSlope : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Raycast downwards to detect the slope and adjust the rotation
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
    }
}
