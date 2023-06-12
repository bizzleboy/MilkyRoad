using System.Collections;
using UnityEngine;

public class QuickTimeEvent : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject playerModel;  // Reference to the PlayerModel
    public AnimationClip enemyCrawlAnimation;
    public int requiredPresses = 15;
    public float timeLimit = 5f;

    private bool eventTriggered = false;
    private float eventStartTime;
    private int presses;

    void Update()
    {
       
        if (eventTriggered)
        {
            // Count the return key presses
            if (Input.GetKeyDown(KeyCode.Return))
            {
                presses++;
            }

            // Check if player has won or lost
            if (presses >= requiredPresses)
            {
                eventTriggered = false;
                // Player won, add code to handle that here
                Debug.Log("Win");
            }
            else if (Time.time - eventStartTime > timeLimit)
            {
                Debug.Log("Lose");
                eventTriggered = false;
                GetComponent<PlayerBehavior>().PlayerDies();
                FindObjectOfType<LevelManager>().LevelLost();// Trigger the player's death
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "QTERat" && !eventTriggered)
        {
            Debug.Log("collided");
            eventTriggered = true;
            eventStartTime = Time.time;
            presses = 0;

            // Instantiate enemy behind the player

            // Instantiate enemy behind the player
            float xOffset = .200f; // Modify this to set how far left of the player to spawn the enemy
            float yOffset = .0501f; // Modify this to set how far above the player to spawn the enemy
            float zOffset = .5001f; // Modify this to set how far behind the player to spawn the enemy

            // Calculate the enemy's spawn position
            Vector3 enemyPosition = playerModel.transform.position
                                    - playerModel.transform.right * xOffset  // Left of the player
                                    + playerModel.transform.up * yOffset     // Above the player
                                    - playerModel.transform.forward * zOffset; // Behind the player
            GameObject enemy = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);

            // Play enemy animation
            enemy.GetComponent<Animation>().clip = enemyCrawlAnimation;
            enemy.GetComponent<Animation>().Play();
        }
    }
}
