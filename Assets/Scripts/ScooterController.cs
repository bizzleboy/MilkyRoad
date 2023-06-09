using UnityEngine;

public class ScooterController : MonoBehaviour
{
    public GameObject playerModel;
    public float accelerationRate = 1f;
    public float maxSpeed = 10f;
    public float minSpeedForTurn = 2f;
    public float turnSpeed = 100f;
    public float gravity = 9.8f;
    public float driftStrength = 20f;
    public float driftSlowDownFactor = 0.8f; // Represents how much to slow down when drifting. 0.8 means 80% of current speed.
    public bool playerControl = true; 
    public float brakeDecelerationRate = 2f;

    public float qteSpeed = 1f; // Speed during Quick Time Event

    public bool canMove = true;


    public float driftBoostTime = 3f;
    public float speedBoostAmount = 5f;
    public float speedBoostDuration = 2f;

    private CharacterController characterController;
    private float speed = 0f;
    private Vector3 moveDirection = Vector3.zero;
    private bool isCurrentlyDrifting = false;
    private float driftDirection = 0f;

    private float driftStartTime;
    private float boostEndTime = 0f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!LevelManager.isGameOver)
        {
            if (canMove)
            {

                float verticalInput = Input.GetAxis("Vertical");
                float horizontalInput = Input.GetAxis("Horizontal");

                bool isDrifting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

                if (isDrifting && !isCurrentlyDrifting)
                {
                    playerModel.transform.Rotate(0, 0, -10f * Mathf.Sign(horizontalInput));
                    isCurrentlyDrifting = true;
                    driftDirection = Mathf.Sign(horizontalInput);
                    driftStartTime = Time.time;

                    // Slow down the scooter when starting to drift
                    speed *= driftSlowDownFactor;
                }
                else if (!isDrifting && isCurrentlyDrifting)
                {
                    playerModel.transform.Rotate(0, 0, 10f * Mathf.Sign(driftDirection));
                    isCurrentlyDrifting = false;
                    driftDirection = 0f;

                    // Check for boost when the player stops drifting
                    if (Time.time - driftStartTime >= driftBoostTime)
                    {
                        speed += speedBoostAmount;
                        boostEndTime = Time.time + speedBoostDuration;
                    }
                    else
                    {
                        boostEndTime = 0f;
                    }
                }

                if (verticalInput > 0) // Acceleration when "W" is pressed
                {
                    speed += verticalInput * accelerationRate * Time.deltaTime;
                    if (speed > maxSpeed && Time.time > boostEndTime)
                    {
                        speed = maxSpeed;
                    }
                }
                else if (verticalInput < 0) // Deceleration when "S" is pressed or reversal if speed is already zero
                {
                    if (speed > 0) // If moving forward, decelerate
                    {
                        speed += verticalInput * brakeDecelerationRate * Time.deltaTime;
                        if (speed < 0)
                        {
                            speed = 0;
                        }
                    }
                    else if (speed <= 0) // If not moving or moving backward, reverse
                    {
                        speed += verticalInput * accelerationRate * Time.deltaTime;
                        if (speed < -maxSpeed)
                        {
                            speed = -maxSpeed;
                        }
                    }
                }
                else // Natural deceleration when no key is pressed
                {
                    speed = Mathf.Lerp(speed, 0, Time.deltaTime * accelerationRate);
                }

                if (boostEndTime > 0f && Time.time > boostEndTime && Mathf.Abs(speed) > maxSpeed)
                {
                    speed -= speedBoostAmount;
                    boostEndTime = 0f;
                }

                if (Mathf.Abs(speed) > minSpeedForTurn)
                {
                    // During drift, turn in the drift direction irrespective of current horizontal input.
                    if (isCurrentlyDrifting)
                    {
                        horizontalInput = driftDirection;
                    }

                    float actualTurnSpeed = isDrifting ? turnSpeed + driftStrength : turnSpeed;
                    transform.Rotate(0, horizontalInput * actualTurnSpeed * Time.deltaTime, 0);
                }

                if (characterController.isGrounded)
                {
                    moveDirection = transform.forward * speed;
                }

                moveDirection.y -= gravity * Time.deltaTime;

                characterController.Move(moveDirection * Time.deltaTime);
            }
            else
            {
                speed = 0;
            }

        }
    }




    public void LaunchUpward(float power)
    {
        if (characterController.isGrounded)
        {
            moveDirection.y += power;
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    public void BoostSpeed(float boostAmount, float boostDuration)
    {
        speed += boostAmount;
        boostEndTime = Time.time + boostDuration;
    }


}
