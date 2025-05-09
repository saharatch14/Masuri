using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplayManager : MonoBehaviour
{
    static public UIGameplayManager instance;
    public GameObject StartPanel;
    public GameObject blackFG;

    [Header("Settings")]
    public GameObject UI_ComboPanel;
    public Slider SliderComboDuration;
    public Text TextComboCounter;
    public Text TextMultiplier;

    private bool isComboActive = false;

    private void Awake()
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

    private void Start()
    {
        PlayerDataManager.inter.startalready = 1;
        UI_ComboPanel.SetActive(false);
        //GameManager.instance.InitScoreManager();
    }

    private void Update()
    {
        if (!isComboActive)
            return;
        UpdateComboUI();
    }

    public void SetComboUIActive(COMBODATA data)
    {
        isComboActive = true;
        UI_ComboPanel.SetActive(true);
        SliderComboDuration.maxValue = data.comboDuration;
        SliderComboDuration.value = data.comboDuration;
        TextComboCounter.text = "Combo " + data.comboCounter.ToString();
        TextMultiplier.text = "x " + data.multiplier.ToString("0.0");
    }

    private void UpdateComboUI()
    {
        SliderComboDuration.value -= Time.deltaTime;
        if (SliderComboDuration.value <= 0)
        {
            UI_ComboPanel.SetActive(false);
            isComboActive = false;
        }
    }

    public void DisbaleComboUIActive()
    {
        UI_ComboPanel.SetActive(false);
    }

    public IEnumerator OnTime_StartGame()
    {
        blackFG.SetActive(true);
        blackFG.GetComponent<Animator>().Play("fade_intro");
        yield return new WaitForSeconds(1.5f);
        blackFG.SetActive(false);
        yield return new WaitForSecondsRealtime(3f);
        GameManager.instance.NewGame();
        StartPanel.SetActive(false);
    }
    public void OnClick_StartGame()
    {
        GameManager.instance.NewGame();
        StartPanel.SetActive(false);
    }
}

public class COMBODATA
{
    public int comboCounter;
    public float multiplier;
    public float comboDuration;
}
