using UnityEngine;

public class Spin : MonoBehaviour
{
    private GameObject playerObject;
    private float originalYPosition;
    private bool isMovingUp;
    private float rotationSpeed = 360f;
    private float moveSpeed = 10f;

    private void Start()
    {
        // Find the game object with the "Player" tag
        playerObject = GameObject.FindGameObjectWithTag("Player");
        originalYPosition = playerObject.transform.position.y;
        isMovingUp = true;
    }

    private void Update()
    {
        // Move the player object up and perform a 360 rotation
        if (isMovingUp)
        {
            Vector3 newPosition = playerObject.transform.position + Vector3.up * moveSpeed * Time.deltaTime;
            playerObject.transform.position = newPosition;

            playerObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            if (playerObject.transform.position.y >= originalYPosition + 10f)
            {
                isMovingUp = false;
            }
        }
        // Move the player object down to its original position
        else
        {
            Vector3 newPosition = playerObject.transform.position - Vector3.up * moveSpeed * Time.deltaTime;
            playerObject.transform.position = newPosition;

            playerObject.transform.rotation = Quaternion.identity;

            if (playerObject.transform.position.y <= originalYPosition)
            {
                playerObject.transform.position = new Vector3(playerObject.transform.position.x, originalYPosition, playerObject.transform.position.z);
                isMovingUp = true;
            }
        }
    }
}
