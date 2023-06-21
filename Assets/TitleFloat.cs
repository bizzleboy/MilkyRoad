using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleFloat : MonoBehaviour
{
    public float height = 1;
    public float speed = 1;
    private float tempY;
    private Vector3 tempPos;

    // Start is called before the first frame update
    void Start()
    {
        tempY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        tempPos.y = tempY + height * Mathf.Sin(speed * Time.time);
        transform.position = tempPos;
    }
}
