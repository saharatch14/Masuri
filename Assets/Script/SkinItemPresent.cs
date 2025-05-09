using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Matsuri;

public class SkinItemPresent : MonoBehaviour
{
    public int ID = 0;
    public int price = 100;
    public bool isUnlocked = false;
    public bool isPicked = false;
    public TMP_Text ButtomText;
    public TMP_Text PriceText;
    public ImageAsset skinImage;
    private void Start()
    {
        if (PriceText != null)
            PriceText.text = price.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        isPicked = ID == PlayerDataManager.skinPickedID;
        ButtomText.text = isPicked ? "PICKED" : "CHOOSE";

        if (!isUnlocked)
        {
            isUnlocked = PlayerDataManager.isSkinUnlocked(ID);
            ButtomText.text = "Lock";
        }
        //isPicked = ID == PlayerDataManager.skinPickedID;
    }
}