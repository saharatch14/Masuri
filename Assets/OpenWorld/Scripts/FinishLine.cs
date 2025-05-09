using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField]
    private string screenname;

    private PlayerController player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject)
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            player.canmove = false;
            player.animate.SetFloat("Speed", 0);
            collision.gameObject.GetComponent<AudioSource>().mute = true;
            StartCoroutine(WaitForSec(player));
        }
    }

    IEnumerator WaitForSec(PlayerController player)
    {
        PlayerDataManager.inter.SaveFinalScore();
        PlayerDataManager.inter.startalready = 0;
        player.animate.SetBool("IsEnd",true);
        yield return new WaitForSeconds(1);
        player.animate.SetBool("IsEnd", false);
        yield return new WaitForSeconds(3);
        GameManagerWorld.instance.Intro.SetActive(true);
        GameManagerWorld.instance.Intro.GetComponent<Animator>().Play("fade_out");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(screenname);
    }
}
