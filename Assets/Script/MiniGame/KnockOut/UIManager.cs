using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour {

    public static UIManager instance;
    public static bool isRestart;
    public GameObject gameScene;
    public GameObject GameUI;
    public GameObject StartPanel;
    public GameObject blackFG;
    public GameObject GameOverUI;

    public GameObject[] allBallsImg;
    public Sprite enabledBallImg;
    public Sprite disabledBallImg;

    public int score;
    public Text scoreText;
    public TMP_Text FinalscoreText;

    public int scoreMultiplier = 1;
    public GameObject scoreMultiImage;
    public Text scoreMultiText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayerDataManager.inter.startalready = 1;
        B_Start();
        //HomeUI.SetActive(true);
        //gameScene.SetActive(false);
        if (isRestart)
        {
            isRestart = false;
            gameScene.SetActive(true);
            GameManage_KnockOut.instance.StartGame();
        }
    }

    public void B_Start()
    {
        StartCoroutine(StartRoutine());
    }

	public void B_Exit()
    {
        Application.Quit();
    }

    IEnumerator StartRoutine()
    {
        blackFG.SetActive(true);
        blackFG.GetComponent<Animator>().Play("fade_intro");
        yield return new WaitForSeconds(1.5f);
        blackFG.SetActive(false);
        yield return new WaitForSeconds(.5f);
        gameScene.SetActive(true);

        //GameManage_KnockOut.instance.StartGame();
    }

    public void ShowBlackFade()
    {
        StartCoroutine("FadeRoutine");
    }

    IEnumerator FadeRoutine()
    {
        blackFG.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        blackFG.SetActive(false);
    }

    public void UpdateBallIcons()
    {
        int ballCount = GameManage_KnockOut.instance.totalBalls;
        for (int i = 0; i < 5; i++)
        {
            if (i < ballCount)
            {
                allBallsImg[i].GetComponent<Image>().sprite = enabledBallImg;
            }
            else
            {
                allBallsImg[i].GetComponent<Image>().sprite = disabledBallImg;
            }
        }
    }

    public void UpdateScore()
    {
        score += scoreMultiplier*1;
        scoreText.text = score.ToString();
    }

    public void UpdateFinalScore()
    {
        FinalscoreText.text = scoreText.text;
    }

    public void UpdateScoreMultiplier()
    {
        if(GameManage_KnockOut.instance.shootedBall == 1)
        {
            scoreMultiplier++;
            scoreMultiImage.SetActive(true);
            scoreMultiText.text = scoreMultiplier.ToString();
        }else
        {
            scoreMultiplier = 1;
            scoreMultiImage.SetActive(false);
        }
    }

    public void B_Restart()
    {
        StartCoroutine("RestartGameRoutine");
    }

    IEnumerator RestartGameRoutine ()
    {
        ShowBlackFade();
        isRestart = true;
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(0);
    }

    public void B_Bck_Yes()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }

    public void OnClick_StartGame()
    {
        GameManage_KnockOut.instance.StartGame();
        StartPanel.SetActive(false);
    }

}
