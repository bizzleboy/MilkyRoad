using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutscene : MonoBehaviour
{
    public float shotDuration = 4f;
    public GameObject shot1;
    public GameObject shot2;
    public GameObject shot3;
    public GameObject shot4;
    public GameObject shot5;
    public GameObject shot6;

    // Start is called before the first frame update
    void Start()
    {
        shot1.SetActive(true);
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        StartCoroutine(PlayShot(shot2, shot1));
        yield return new WaitForSeconds(shotDuration);
        StartCoroutine(PlayShot(shot3, shot2));
        yield return new WaitForSeconds(shotDuration);
        StartCoroutine(PlayShot(shot4, shot3));
        yield return new WaitForSeconds(shotDuration);
        StartCoroutine(PlayShot(shot5, shot4));
        yield return new WaitForSeconds(shotDuration);
        StartCoroutine(PlayShot(shot6, shot5));
        yield return new WaitForSeconds(shotDuration);

        EndCutscene();
    }

    IEnumerator PlayShot(GameObject currentShot, GameObject prevShot)
    {
        yield return new WaitForSeconds(shotDuration);
        currentShot.SetActive(true);
        prevShot.SetActive(false);
    }

    void EndCutscene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
