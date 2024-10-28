using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void GameScene()
    {
        SceneManager.LoadScene("PlayerMove"); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
