using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileShoot : MonoBehaviour
{
    public GameObject projectPrefab;
    public float speed = 60;

    public AudioClip throwPizzaSFX;

    public Image reticleImage;
    public Color reticleWindowcolor;

    Color originalReticleColor;

    // Start is called before the first frame update
    void Start()
    {
        originalReticleColor = reticleImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerBehavior.fpsMode)
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

    private void FixedUpdate()
    {
        ReticleEffect();
    }

    void ReticleEffect()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Window"))
            {
                reticleImage.color = Color.Lerp(reticleImage.color,
                    reticleWindowcolor, Time.deltaTime * 2);

                reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale,
                    new Vector3(0.7f, 0.7f, 1), Time.deltaTime * 2);
            }
        }
        else
        {
            reticleImage.color = Color.Lerp(reticleImage.color,
                    originalReticleColor, Time.deltaTime * 2);

            reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale,
                Vector3.one, Time.deltaTime * 2);
        }
    }
}
