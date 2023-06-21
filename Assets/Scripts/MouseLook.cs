using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    Transform playerBody;
    public bool isMenu = false;
    public static float mouseSensitivity = 50;
    public Slider sensitivitySlider;
    public Text sensitivityDisplay;

    float pitch = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!isMenu && !PauseMenu.isPaused)
        {
            playerBody = transform.parent.transform;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            sensitivitySlider.value = mouseSensitivity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerBehavior.fpsMode && !LevelManager.isGameOver)
        {
            float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            //yaw
            playerBody.Rotate(moveX * Vector3.up);

            //pitch
            pitch -= moveY;

            pitch = Mathf.Clamp(pitch, -90f, 90f);
            transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
        sensitivitySlider.value = mouseSensitivity;
        sensitivityDisplay.text = mouseSensitivity.ToString("0.00");
    }

    public void moveToFPS()
    {
        transform.localPosition = new Vector3(0, 0.264f, 0);
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    public void AdjustSensitivity(float newSpeed)
    {
        mouseSensitivity = newSpeed;
    }
}
