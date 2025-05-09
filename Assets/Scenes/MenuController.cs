using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public string loaderScene;

    public TextMeshProUGUI musicValue;
    public AudioMixer musicMixer;
    public TextMeshProUGUI soundsValue;
    public AudioMixer soundsMixer;
    public Button loadButton;
    public Sprite[] pic;
    public GameObject fullimage;
    public TMP_Text FinalscoreText;

    private Animator animator;
    private int _window = 0;
    private int _windowUnlock = 0;

    public void Start()
    {
        Time.timeScale = 1;
        animator = GetComponent<Animator>();
#if UNITY_EDITOR
        Debug.Log(PlayerPrefs.GetInt("FinalScore"));
#endif
        if (PlayerPrefs.GetInt("FinalScore") <= 0)
        {
            loadButton.interactable = false;
        }
        else if (PlayerPrefs.GetInt("FinalScore") > 0)
        {
            loadButton.interactable = true;
        }
#if UNITY_EDITOR
        Debug.Log(PlayerPrefs.GetInt("FinalScore"));
        Debug.Log($"||RingToss: {PlayerPrefs.GetInt("ScoreRingToss")} || KnockOut: {PlayerPrefs.GetInt("ScoreKnockOut")} || GunBlast: {PlayerPrefs.GetInt("ScoreGunBlast")}");
#endif
        //FinalscoreText.text = PlayerPrefs.GetInt("FinalScore").ToString();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _window == 1)
        {
            animator.SetTrigger("HideOptions");
            _window = 0;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && _windowUnlock == 1)
        {
            animator.SetTrigger("HideUnlock");
            _windowUnlock = 0;
        }
        FinalscoreText.text = PlayerPrefs.GetInt("FinalScore").ToString();
    }

    public void NewGame()
    {
        Load();
    }

    public void Load()
    {
        SceneManager.LoadScene(loaderScene, LoadSceneMode.Additive);
    }

    public void ShowUnlockOptions()
    {
        animator.SetTrigger("ShowUnlock");
        _windowUnlock = 1;
    }

    public void HideUnlockOptions()
    {
        animator.SetTrigger("HideUnlock");
        _windowUnlock = 0;
    }

    public void ShowOptions()
     {
         animator.SetTrigger("ShowOptions");
        _window = 1;
     }

    public void HideOptions()
    {
        animator.SetTrigger("HideOptions");
        _window = 0;
    }

    public void ZoomImage(int code)
    {
        switch (code)
        {
            case 0:
                if (PlayerPrefs.GetInt("ScoreRingToss") >= 300)
                {
                    fullimage.SetActive(true);
                    fullimage.GetComponentInChildren<Image>().sprite = pic[code];
                }
                else
                {

                }
                break;

            case 1:
                if (PlayerPrefs.GetInt("ScoreKnockOut") >= 50)
                {
                    fullimage.SetActive(true);
                    fullimage.GetComponentInChildren<Image>().sprite = pic[code];
                }
                else
                {

                }
                break;

            case 2:
                if (PlayerPrefs.GetInt("ScoreGunBlast") >= 1250)
                {
                    fullimage.SetActive(true);
                    fullimage.GetComponentInChildren<Image>().sprite = pic[code];
                }
                else
                {

                }
                break;

            case 3:
                if (PlayerPrefs.GetInt("FinalScore") > 2500)
                {
                    fullimage.SetActive(true);
                    fullimage.GetComponentInChildren<Image>().sprite = pic[code];
                }
                else
                {

                }
                break;
        }
    }

    public void ResetScore()
    {
        PlayerDataManager.inter.Reset();
        loadButton.interactable = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public void OnMusicChanged(float value)
    {
        musicValue.SetText(value + "%");
        musicMixer.SetFloat("volume", -50 + value / 2);
    }
    
    public void OnSoundsChanged(float value)
    {
        soundsValue.SetText(value + "%");
        soundsMixer.SetFloat("volume", -50 + value / 2);
    }
}
