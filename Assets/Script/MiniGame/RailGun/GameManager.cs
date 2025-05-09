using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    public Text scoreText;
    public GameObject GameOverScreen;
    public TMP_Text finalScore;
    public Image fadeImage;
    private bool IsGameStart = false;

    private Spawner spawner;
    public float score { get; private set; }

    [Header("Combo System")]
    public float comboDuration = 3;
    public float comboMultiplier = 1.5f;
    public float comboMultiplierIncrease = 0.1f;

    [SerializeField] private int comboCounter;
    [SerializeField] private bool isComboStart = false;
    [SerializeField] private float comboExpireTimer;
    [SerializeField] private float multiplier;
    [SerializeField] private float totalBonus = 0;
    [SerializeField] private float totalHitScore = 0;
    public int HighestCombo { get; private set; }
    public int HitCount { get; private set; }
    public int TotalShoot { get; private set; }
    public float Accuracy { get; private set; }
    public int TotalFiredScore { get; set; }

    private int tempComboCounter = 0;

    private void Awake()
    {
        //blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameOverScreen.SetActive(false);
        spawner.enabled = false;
        Physics.gravity = new Vector3(0, -35F, 0);
        Debug.Log(PlayerDataManager.inter.ScoreGunBlast);
        UIGameplayManager.instance.StartCoroutine(UIGameplayManager.instance.OnTime_StartGame());
        //NewGame();
    }

    private void Update()
    {
        if (isComboStart == false)
            return;

        comboExpireTimer -= Time.deltaTime;
        if (comboExpireTimer <= 0)
        {
            isComboStart = false;

            ResetCombo();
        }
    }

    public void InitScoreManager()
    {
        score = 0;
        HighestCombo = 0;
        HitCount = 0;
        TotalShoot = 0;
        totalHitScore = 0;
        totalBonus = 0;
        comboCounter = 0;
        multiplier = 0;
        tempComboCounter = 0;
        TotalFiredScore = 0;
    }

    public void NewGame()
    {
        IsGameStart = true;
        InitScoreManager();
        Time.timeScale = 1f;

        ClearScene();

        //blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoreText.text = score.ToString();
    }

    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach (Fruit fruit in fruits) {
            Destroy(fruit.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs) {
            Destroy(bomb.gameObject);
        }
    }
    private void GameOverScene()
    {
        Time.timeScale = 1f;

        ClearScene();
        scoreText.gameObject.SetActive(false);
        UIGameplayManager.instance.DisbaleComboUIActive();
        Physics.gravity = new Vector3(0, -75F, 0);
        finalScore.text = score.ToString();
        GameOverScreen.SetActive(true);
#if UNITY_EDITOR
        Debug.Log($"|| Score : {score} || TotalScore : {totalHitScore} || Bonus : {totalBonus}");
#endif
        //PlayerPrefs.SetInt("ScoreGunBlast", (int)score);
        PlayerDataManager.inter.ScoreGunBlast = (int)score;

        if ((int)score > PlayerDataManager.inter.ScoreGunBlast)
        {
            PlayerDataManager.inter.ScoreGunBlast = (int)score;
        }

        PlayerDataManager.inter.gold += (int)score/10;
        StartCoroutine(BacktoScreen());
    }

    public void IncreaseScore(int points)
    {
        if (comboCounter == 0)
        {
            isComboStart = true;
            multiplier = comboMultiplier;
        }
        else
        {
            multiplier += comboMultiplierIncrease;
        }

        float bonus = (comboExpireTimer * (multiplier * comboCounter));
        totalBonus += (int)bonus;
        totalHitScore += points;

        //score += points;
        //scoreText.text = score.ToString();

        score = totalHitScore + totalBonus;
        //Score = Mathf.Round(Score * 10) / 10;
        score = (int)score + TotalFiredScore;
        scoreText.text = score.ToString();

        comboExpireTimer = comboDuration;
        comboCounter++;

        tempComboCounter++;
        if (tempComboCounter > HighestCombo)
            HighestCombo = tempComboCounter;

        ShowComboUI();
    }

    private void ResetCombo()
    {
        comboCounter = 0;
        multiplier = 0;
        tempComboCounter = 0;

    }

    public void Explode()
    {
        spawner.enabled = false;

        StartCoroutine(ExplodeSequence());
    }

    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        // Fade to white
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
        AudioController.instance.PlaySFX("Bomb");
        yield return new WaitForSecondsRealtime(1f);
        //NewGame();
        GameOverScene();

        elapsed = 0f;

        // Fade back in
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
    }
    private IEnumerator BacktoScreen()
    {
        PlayerDataManager.inter.startalready = 1;
        yield return new WaitForSecondsRealtime(2f);
        GameOverScreen.GetComponent<Animator>().enabled = true;
        GameOverScreen.GetComponent<Animator>().Play("fade_out");
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("OpenWorld");
    }
    private void ShowComboUI()
    {
        COMBODATA comboData = new COMBODATA();
        comboData.comboDuration = comboExpireTimer;
        comboData.comboCounter = comboCounter;
        comboData.multiplier = multiplier;
        UIGameplayManager.instance.SetComboUIActive(comboData);
    }

    public bool IsGameReady()
    {
        return IsGameStart;
    }

}
