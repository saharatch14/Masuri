using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogPlay : MonoBehaviour
{
    public dialogController bottomBar;
    public GameObject dialogUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && bottomBar.IsdialogPlaystate())
        {        
            if (bottomBar.IsCompleted())
            {
                bottomBar.StopTyping();
                if (bottomBar.IsLastSentence())
                {
                    bottomBar.IsStop();
                    dialogUI.SetActive(false);
                    //GameManagerWorld.instance.gameHasStarted = true;
                    GameManagerWorld.instance.dialogStart = false;
                }
                else
                {
                    bottomBar.PlayNextSentence();
                }
            }
            else
            {
                bottomBar.SpeedUp();
            }
        }
    }
}
