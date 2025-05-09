using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManage_RingToss : MonoBehaviour
{
	public static GameManage_RingToss instance;
	public GameObject ring;
	public GameObject button;
	public float ringforce;
	//public Transform ringTarget;
	public int totalRings;
	public bool readyToshoot;
	public bool beenSpawnedOnce;

	public Ring ringScript;
	public bool gameHasStarted = false;

	public int shootedRing;
	[SerializeField] private int totalgetringScore = 0;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}
	}

	public void StartGame()
	{
		Debug.Log(PlayerDataManager.inter.ScoreRingToss);
		gameHasStarted = true;
		readyToshoot = true;
		beenSpawnedOnce = true;
	}

	public void countringin()
    {
		totalgetringScore += 1;
		AudioController.instance.PlaySFX("Confirm");
	}

	void Update()
	{
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));

		if (Input.GetMouseButton(0) && readyToshoot)
		{
			ring.transform.position = new Vector3(mousePos.x, ring.transform.position.y, ring.transform.position.z);
		}
	}

	public void shootRing(float power)
    {
		ring.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.Impulse);
		button.SetActive(false);
		readyToshoot = false;
		beenSpawnedOnce = false;
		shootedRing++;
		totalRings--;
		UIManager_RingToss.instance.UpdateRing();
	}

	public void SpwaneRing(Vector3 locae, Quaternion rota,bool canspawn)
    {
		if (totalRings <= 0 && canspawn == false)
		{
			readyToshoot = false;
			beenSpawnedOnce = true;
			ring.GetComponent<Ring>().StopAllCoroutines();
			//Check Game over
			CheckGameOver();
		}
		else if(totalRings > 0 && canspawn == true)
        {
			button.SetActive(true);
			if (beenSpawnedOnce == false)
			{
				ring = Instantiate(ring);
				ring.transform.position = locae;
				ring.transform.rotation = rota;
			}
		}
	}

	IEnumerator BacktoScreen()
	{
		UIManager_RingToss.instance.GameOverUI.GetComponent<Animator>().enabled = false;
		yield return new WaitForSeconds(4.0f);
		PlayerDataManager.inter.gold += UIManager_RingToss.instance.score;
		UIManager_RingToss.instance.GameUI.SetActive(false);
		UIManager_RingToss.instance.GameOverUI.SetActive(true);

#if UNITY_EDITOR
		Debug.Log($"TotalScore : {UIManager_RingToss.instance.score}");
#endif

		if (UIManager_RingToss.instance.score > PlayerDataManager.inter.ScoreRingToss)
		{
			PlayerDataManager.inter.ScoreRingToss = UIManager_RingToss.instance.score;
		}

		PlayerDataManager.inter.startalready = 1;
		yield return new WaitForSeconds(2.0f);
		UIManager_RingToss.instance.GameOverUI.GetComponent<Animator>().enabled = true;
		UIManager_RingToss.instance.GameOverUI.GetComponent<Animator>().Play("fade_out");
		yield return new WaitForSeconds(2.0f);
		SceneManager.LoadScene("OpenWorld");

	}

	void CheckGameOver()
    {
		StartCoroutine(BacktoScreen());
	}
}
