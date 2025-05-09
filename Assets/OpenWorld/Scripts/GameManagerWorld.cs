using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerWorld : MonoBehaviour
{
    public static GameManagerWorld instance;
    public GameObject Intro;
    public GameObject ShopUI;
    public GameObject block;
    public TMP_Text Coinhave;
    public SkinItemPresent[] listSkin;
    public SkinItemPresent currentSkinPresent { get; set; }
    public PlayerController player;
    public int currentID = 0;

    public bool gameHasStarted = false;
    public bool dialogStart = false;
    public bool introStart = false;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        CloseShop();
        Debug.Log(PlayerDataManager.skinPickedID);
        currentID = PlayerDataManager.skinPickedID;
        currentSkinPresent = listSkin[currentID];
        Coinhave.text = PlayerPrefs.GetInt("PlayerGold").ToString();

        if(gameHasStarted == false && PlayerDataManager.inter.startalready == 0)
        {
            block.SetActive(false);
            StartCoroutine(StartGame());
        }
        else
        {
            block.SetActive(true);
            gameHasStarted = true;
            StartCoroutine(ContinueGame());
            Debug.Log(gameHasStarted);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator StartGame()
    {
        block.SetActive(false);
        introStart = true;
        //Print the time of when the function is first called.
#if UNITY_EDITOR
        Debug.Log("Started Animetion Coroutine at timestamp : " + Time.time);
        Debug.Log(gameHasStarted);
#endif
        Intro.SetActive(true);
        Intro.GetComponent<Animator>().Play("fade_intro");
        yield return new WaitForSeconds(3f);
        Intro.SetActive(false);
        gameHasStarted = true;
#if UNITY_EDITOR
        Debug.Log(gameHasStarted);
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Animetion Coroutine at timestamp : " + Time.time);
#endif
        block.SetActive(true);
        introStart = false;
    }
    IEnumerator ContinueGame()
    {
        Intro.SetActive(true);
        Intro.GetComponent<Animator>().Play("fade_intro");
        yield return new WaitForSeconds(2.5f);
        Intro.SetActive(false);
        block.SetActive(true);
    }

    public void EndGame()
    {
        gameHasStarted = false;
    }

    public void SetItem(SkinItemPresent skin)
    {
        currentSkinPresent = skin;
    }

    public void OpenShop()
    {
        ShopUI.SetActive(true);
    }

    public void CloseShop()
    {
        ShopUI.SetActive(false);
    }
    public void Click()
    {
        currentSkinPresent = listSkin[currentID];
        if (currentSkinPresent.isUnlocked)
            Pick();
        else
            Buy();
    }

    public void ChoiceID(int num)
    {
        currentID = num;
    }

    void Buy()
    {
#if UNITY_EDITOR
        Debug.Log("Buy");
#endif
        if (currentSkinPresent.price <= PlayerDataManager.inter.gold)
        {
            PlayerDataManager.inter.gold -= currentSkinPresent.price;
            //StartCoroutine(NakaGameManager.Instance.BuyItem(currentBallPresent.price));
            //SoundManager.PlaySfx(SoundManager.Instance.soundUnlockBall);
            //StartCoroutine(NakaGameManager.Instance.SaveSkinItems(currentBallPresent.ID, 1));

            PlayerDataManager.UnlockSkin(currentSkinPresent.ID);
            PlayerDataManager.PickSkin(currentSkinPresent.ID);
            Pick();
            AudioController.instance.PlaySFX("Buy");
            Coinhave.text = PlayerPrefs.GetInt("PlayerGold").ToString();
        }
        else
        {
            //SoundManager.PlaySfx(SoundManager.Instance.soundNoEnoughGem);
            AudioController.instance.PlaySFX("CantBuy");
            //Debug.Log("Can't Buy");
        }
    }

    void Pick()
    {
#if UNITY_EDITOR
        Debug.Log($"||currentSkinPresent.ID: {currentSkinPresent.ID} || PlayerDataManager.skinPickedID: {PlayerDataManager.skinPickedID}");
#endif
        if (currentSkinPresent.ID != PlayerDataManager.skinPickedID)
        {
           //SoundManager.PlaySfx(SoundManager.Instance.soundPickBall);
            PlayerDataManager.PickSkin(currentSkinPresent.ID);
            PlayerDataManager.skinPickedID = currentSkinPresent.ID;
            player.SetSkin(listSkin[PlayerDataManager.skinPickedID].skinImage);
            AudioController.instance.PlaySFX("Swap");
        }
    }
}
