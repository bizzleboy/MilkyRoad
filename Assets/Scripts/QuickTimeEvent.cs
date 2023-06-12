using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuickTimeEvent : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject playerModel;  // Reference to the PlayerModel
    public AnimationClip enemyCrawlAnimation;
    public int requiredPresses = 15;
    public float timeLimit = 5f;

    public Image image1; // Reference to the first image
    public Image image2; // Reference to the second image
    public AudioClip winSound; // Sound to play when the player wins

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
                playerModel.GetComponent<ScooterController>().playerControl = true;
              // AudioSource.PlayClipAtPoint(winSound, playerModel.transform.position);
                Debug.Log("Win");
                StopCoroutine("FlashImages");

            }
            else if (Time.time - eventStartTime > timeLimit)
            {
                Debug.Log("Lose");
                eventTriggered = false;
             
                GetComponent<PlayerBehavior>().PlayerDies();
                FindObjectOfType<LevelManager>().LevelLost();
                StopCoroutine("FlashImages");
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

            playerModel.GetComponent<ScooterController>().playerControl = false;
            StartCoroutine("FlashImages");
        }
    }

    IEnumerator FlashImages()
    {
        while (true)
        {
            image1.enabled = true;
            image2.enabled = false;
            yield return new WaitForSeconds(0.5f);
            image1.enabled = false;
            image2.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
