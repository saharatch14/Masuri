using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager_RingToss : MonoBehaviour
{
    public static UIManager_RingToss instance;

    public GameObject StartPanel;

    public GameObject gameScene;
    public GameObject GameUI;
    public GameObject blackFG;
    public GameObject GameOverUI;

    public int score;
    public TMP_Text scoreText;
    public TMP_Text FinalscoreText;

    public int scoreMultiplier = 1;
    public GameObject scoreMultiImage;
    public Text scoreMultiText;

    public TMP_Text AllringText;


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
        StartCoroutine(StartRoutine());
    }

    public void B_Exit()
    {
        Application.Quit();
    }

    IEnumerator StartRoutine()
    {
        UpdateRing();
        ShowBlackFade();
        yield return new WaitForSeconds(.5f);
        gameScene.SetActive(true);
    }

    public void ShowBlackFade()
    {
        StartCoroutine("FadeRoutine");
    }

    IEnumerator FadeRoutine()
    {
        blackFG.SetActive(true);
        blackFG.GetComponent<Animator>().Play("fade_intro");
        yield return new WaitForSeconds(1.5f);
        blackFG.SetActive(false);
    }

    public void UpdateRing()
    {
        int ballCount = GameManage_RingToss.instance.totalRings;
        AllringText.text = ballCount.ToString();
    }

    public void UpdateScore()
    {
        score += scoreMultiplier * 1;
        scoreText.text = score.ToString();
        FinalscoreText.text = score.ToString();
        GameManage_RingToss.instance.countringin();
    }

    public void OnClick_StartGame()
    {
        GameManage_RingToss.instance.StartGame();
        StartPanel.SetActive(false);
    }
}
