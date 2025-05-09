using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenUnlock : MonoBehaviour
{
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        switch (id)
        {
            case 0:
                if (PlayerPrefs.GetInt("ScoreRingToss") >= 300)
                {
                    gameObject.GetComponent<Image>().color = Color.white;
                }
                else
                {
                    gameObject.GetComponent<Image>().color = Color.black;
                }
                break;

            case 1:
                if (PlayerPrefs.GetInt("ScoreKnockOut") >= 50)
                {
                    gameObject.GetComponent<Image>().color = Color.white;
                }
                else
                {
                    gameObject.GetComponent<Image>().color = Color.black;
                }
                break;

            case 2:
                if (PlayerPrefs.GetInt("ScoreGunBlast") >= 1250)
                {
                    gameObject.GetComponent<Image>().color = Color.white;
                }
                else
                {
                    gameObject.GetComponent<Image>().color = Color.black;
                }
                break;

            case 3:
                if (PlayerPrefs.GetInt("FinalScore") > 2500)
                {
                    gameObject.GetComponent<Image>().color = Color.white;
                }
                else
                {
                    gameObject.GetComponent<Image>().color = Color.black;
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
