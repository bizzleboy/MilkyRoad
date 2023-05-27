using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour
{
    public GameObject projectPrefab;
    public float speed = 20;

    public AudioClip throwPizzaSFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(projectPrefab,
                transform.position + transform.forward, transform.rotation);

            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);

            projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjecttileParent").transform);

            AudioSource.PlayClipAtPoint(throwPizzaSFX, transform.position);
        }
    }

}
