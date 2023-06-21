using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuickTimeEvent : MonoBehaviour
{
    public int requiredPresses = 15;
    public float timeLimit = 5f;
    public Image qteImage;  // Reference to the Quick Time Event Image
    public float scaleSpeed = 1f;  // Speed of the image scaling effect
    public Vector3 minScale = new Vector3(1f, 1f, 1f);  // Minimum scale of the image
    public Vector3 maxScale = new Vector3(2f, 2f, 2f);  // Maximum scale of the image

    private bool eventTriggered = false;
    private float eventStartTime;
    private int presses;
    private ScooterController playerMovement;
    private int eventNum = 0;
    private float rotationPerPress;

    private void Start()
    {
        playerMovement = gameObject.GetComponent<ScooterController>();
        qteImage.gameObject.SetActive(false);  // Initially set the image to be inactive
        rotationPerPress = 360f / requiredPresses;
    }

    void OnTriggerEnter(Collider other)
    {
        if (eventNum == 0)
        {
            if (other.gameObject.tag == "QTERat" && !eventTriggered)
            {
                playerMovement.canMove = false;
                eventTriggered = true;
                eventStartTime = Time.time;
                presses = 0;
                eventNum++;
                if (qteImage != null)
                {
                    qteImage.gameObject.SetActive(true);  // Set the image to be active when the event starts
                }
            }
        }
    }

    void Update()
    {
        if (eventTriggered && eventNum == 1)
        {
            playerMovement.canMove = false;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                presses++;
                this.transform.Rotate(0, rotationPerPress, 0); // Rotate this GameObject each press
            }

            if (presses >= requiredPresses)
            {
                playerMovement.canMove = true;
                eventTriggered = false;
                if (qteImage != null)
                {
                    qteImage.gameObject.SetActive(false);  // Set the image to be inactive when the event ends
                }
            }
            else if (Time.time - eventStartTime > timeLimit)
            {
                eventTriggered = false;
                playerMovement.canMove = true;
                GetComponent<PlayerBehavior>().PlayerDies();
                FindObjectOfType<LevelManager>().LevelLost();
                if (qteImage != null)
                {
                    qteImage.gameObject.SetActive(false);  // Set the image to be inactive when the event ends
                }
            }

            if (qteImage != null)
            {
                // Create a scaling effect for the image while the event is in progress
                float scale = Mathf.PingPong(Time.time * scaleSpeed, 1);  // Returns value between 0 and 1
                qteImage.transform.localScale = Vector3.Lerp(minScale, maxScale, scale);  // Lerp between minScale and maxScale
            }
        }
    }
}
