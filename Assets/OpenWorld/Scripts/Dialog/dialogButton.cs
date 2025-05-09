using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class dialogButton : MonoBehaviour
{
    public GameObject dialogUI;
    [SerializeField] private dialogController dialog;
    [SerializeField] private TMP_Text pickUpText;
    [SerializeField] private List<dialogStroy> currentScene;
    private bool pickUpAllowed = false;
    private int Isintro = 1;
    // Start is called before the first frame update
    private void Start()
    {
        Isintro = PlayerDataManager.inter.startalready;
        Debug.Log(Isintro);
        dialogUI.SetActive(false);
        if(PlayerDataManager.inter.startalready == 0)
        {
            foreach (dialogStroy g in Resources.LoadAll("OpenWorld/Dialog/1"))
            {
                currentScene.Add(g);
            }
        }
        else
        {
            foreach (dialogStroy g in Resources.LoadAll("OpenWorld/Dialog/1"))
            {
                currentScene.Add(g);
            }
            foreach (dialogStroy g in Resources.LoadAll("OpenWorld/Dialog/ReachScore"))
            {
                currentScene.Add(g);
            }
        }
    }
    private void Update()
    {
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E) && GameManagerWorld.instance.dialogStart == false && GameManagerWorld.instance.gameHasStarted)
            PickUp();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject)
        {
            pickUpText.gameObject.SetActive(true);
            pickUpAllowed = true;
            pickUpText.text = "Press " + "<color=yellow>E</color> " + "To Talk";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject)
        {
            pickUpText.gameObject.SetActive(false);
            pickUpAllowed = false;
        }
    }

    private void PickUp()
    {
        dialogUI.SetActive(true);
        if(PlayerDataManager.inter.startalready != 0)
        {
            int probilty = Random.Range(0, 101);
            if (probilty <= 50 && probilty >= 26 && PlayerDataManager.inter.ScoreKnockOut > 0)
            {
                dialog.PlayScene(currentScene[4]);
            }
            else if (probilty <= 75 && probilty >= 51 && PlayerDataManager.inter.ScoreGunBlast > 0)
            {
                dialog.PlayScene(currentScene[5]);
            }
            else if (probilty <= 100 && probilty >= 76 && PlayerDataManager.inter.ScoreRingToss > 0)
            {
                dialog.PlayScene(currentScene[6]);
            }
            else
            {
                dialog.PlayScene(currentScene[Random.Range(1, 4)]);
            }    
        }
        else
        {
            if(Isintro == 0)
            {
                dialog.PlayScene(currentScene[0]);
                Isintro = 1;
            }
            else
            {
                dialog.PlayScene(currentScene[Random.Range(1, 4)]);
            }
        }
        dialog.IsStart();
        //GameManagerWorld.instance.gameHasStarted = false;
        GameManagerWorld.instance.dialogStart = true;
    }
}
