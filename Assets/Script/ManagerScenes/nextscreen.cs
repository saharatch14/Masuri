using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Play, Won, Lost }

public class nextscreen : MonoBehaviour
{
    public string winSceneName = "WinScene";
    public GameState gameState { get; private set; }
    float m_TimeLoadEndGameScene;
    public float endSceneLoadDelay = 3f;
    public float delayBeforeFadeToBlack = 4f;
    private int nextSceneTolLoad;
    string m_SceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        //nextSceneTolLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //SceneManager.LoadScene(nextSceneTolLoad);
        // unlocks the cursor before leaving the scene, to be able to click buttons
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Remember that we need to load the appropriate end scene after a delay
        m_SceneToLoad = winSceneName;
        m_TimeLoadEndGameScene = Time.time + endSceneLoadDelay + delayBeforeFadeToBlack;
    }

    /*void EndGame(bool win)
    {
        // unlocks the cursor before leaving the scene, to be able to click buttons
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Remember that we need to load the appropriate end scene after a delay
        gameState = win ? GameState.Won : GameState.Lost;
        if (win)
        {
            m_SceneToLoad = winSceneName;
            m_TimeLoadEndGameScene = Time.time + endSceneLoadDelay + delayBeforeFadeToBlack;

        }
    }*/
}
