using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float levelTime = 0;
    public static bool isGameOver = false;

    public AudioClip gameOverSFX;
    public AudioClip gameWonSFX;

    public string nextLevel;

    public Text gameText;
    public Text timerText;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        levelTime = 0;
        SetTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            levelTime += Time.deltaTime;

            SetTimerText();
            SetScoreText();
        }
    }

    public void LevelBeat()
    {
        isGameOver = true;
        gameText.text = "YOU WIN!";
        SetScoreText();

        //Camera.main.GetComponent<AudioSource>().pitch = 2;
        //AudioSource.PlayClipAtPoint(gameWonSFX, Camera.main.transform.position);

        if (!string.IsNullOrEmpty(nextLevel))
        {
            Invoke("LoadNextLevel", 2);
        }
    }

    public void LevelLost()
    {
        isGameOver = true;
        gameText.text = "GAME OVER!";

        //Camera.main.GetComponent<AudioSource>().pitch = 1;
        //AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);

        Invoke("LoadCurrentLevel", 2);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    private void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SetTimerText()
    {
        timerText.text = levelTime.ToString("0.00");
    }

    private void SetScoreText()
    {
        scoreText.text = WindowHit.score.ToString();
    }
}
