using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ActiveButton : MonoBehaviour
{
    [SerializeField] private TMP_Text pickUpText;
    [SerializeField] private string screenname;
    private bool pickUpAllowed;
    [SerializeField] private bool isshop;
    private Transform lastlocate;

    // Use this for initialization
    private void Start()
    {
        pickUpText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E) && isshop == false)
            PickUp();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject && !isshop)
        {
            pickUpText.gameObject.SetActive(true);
            pickUpAllowed = true;
            pickUpText.text = "Press " + "<color=yellow>E</color> " + "To go " + screenname;
            lastlocate = collision.gameObject.transform;
        }
        else if(collision.gameObject && isshop)
        {
            pickUpAllowed = true;
            GameManagerWorld.instance.OpenShop();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject)
        {
            pickUpText.gameObject.SetActive(false);
            pickUpAllowed = false;
            GameManagerWorld.instance.CloseShop();
        }
    }

    private void PickUp()
    {
        PlayerDataManager.inter.SaveLocate(lastlocate);
        StartCoroutine(WaitForSec());
    }
    IEnumerator WaitForSec()
    {
       GameManagerWorld.instance.Intro.SetActive(true);
       GameManagerWorld.instance.Intro.GetComponent<Animator>().Play("fade_out");
       yield return new WaitForSeconds(2);
       SceneManager.LoadScene(screenname);
    }

}
