using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
public class Timer : MonoBehaviour
{
    public float timeLimit = 60f;
    public TMP_Text timerText;
    private float timeRemaining;

    public GameObject gameOverPanel;
    public TMP_Text gameOverText;


    void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("TimerText is not assigned in the inspector!");
        }
        timeRemaining = timeLimit;
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (timerText != null) 
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = Mathf.Max(0, timeRemaining).ToString("F2");

            if (timeRemaining <= 0)
            {
                StartCoroutine(GameOver());
            }
        }
    }

    private IEnumerator GameOver()
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = "GAME OVER!";
        yield return new WaitForSeconds(3f);
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene("SampleScene");
    }
}
