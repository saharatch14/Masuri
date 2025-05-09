using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public float restartDelay = 1f;

    public void CompleteLevel()
    {

    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Invoke("Restart", restartDelay);
        }

    }
    void Restart()
    {
        SceneManager.LoadScene(2);
    }
}
