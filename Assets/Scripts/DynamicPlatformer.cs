using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPlatformer : MonoBehaviour
{
    public float speed = 3;
    public float distance = 5;

    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPos = transform.position;
        newPos.x = startPos.x + (Mathf.Sin(Time.time * speed) * distance);

        transform.position = newPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }
    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }


}
