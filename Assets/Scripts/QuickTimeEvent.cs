using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuickTimeEvent : MonoBehaviour
{
    public GameObject playerModel;
    public int requiredPresses = 15;
    public float timeLimit = 5f;
    public float rotationSpeed = 360f;  // Speed of the rotation
    public Image qteImage;  // Reference to the Quick Time Event Image
    public float scaleSpeed = 1f;  // Speed of the image scaling effect
    public Vector3 minScale = new Vector3(1f, 1f, 1f);  // Minimum scale of the image
    public Vector3 maxScale = new Vector3(2f, 2f, 2f);  // Maximum scale of the image

    private bool eventTriggered = false;
    private bool rotating = false;
    private float eventStartTime;
    private int presses;
    private float totalRotation = 0;
    private ScooterController playerMovement;

    private void Start()
    {
        playerMovement = gameObject.GetComponent<ScooterController>();
        if (playerModel == null)
        {
            Debug.Log("playerModel is null");
        }
        if (playerMovement == null)
        {
            Debug.Log("playerMovement is null");
        }
        if (qteImage == null)
        {
            Debug.Log("qteImage is null");
        }
        else
        {
            qteImage.gameObject.SetActive(false);  // Initially set the image to be inactive
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "QTERat" && !eventTriggered)
        {
            playerMovement.canMove = false;
            Debug.Log("collided");
            eventTriggered = true;
            eventStartTime = Time.time;
            presses = 0;
            if (qteImage != null)
            {
                qteImage.gameObject.SetActive(true);  // Set the image to be active when the event starts
            }
        }
    }

    void Update()
    {
        if (eventTriggered)
        {
            playerMovement.canMove = false;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                presses++;
            }

            if (presses >= requiredPresses)
            {
                Debug.Log("Win");
                playerMovement.canMove = true;
                eventTriggered = false;
                rotating = true;
                totalRotation = 0;
                if (qteImage != null)
                {
                    qteImage.gameObject.SetActive(false);  // Set the image to be inactive when the event ends
                }
            }
            else if (Time.time - eventStartTime > timeLimit)
            {
                Debug.Log("Lose");
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

        if (rotating)
        {
            // Apply rotation to player
            float rotation = rotationSpeed * Time.deltaTime;
            this.transform.Rotate(0, rotation, 0);

            totalRotation += rotation;
            if (totalRotation >= 360f)
            {
                rotating = false;
                // Fix possible overshoot by resetting to the exact final rotation
                this.transform.localEulerAngles = new Vector3(
                    this.transform.localEulerAngles.x,
                    this.transform.localEulerAngles.y % 360f,
                    this.transform.localEulerAngles.z);
            }
        }
    }
}
