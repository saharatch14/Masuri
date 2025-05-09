using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class GameManage_KnockOut : MonoBehaviour
{

	public static GameManage_KnockOut instance;
	public GameObject ball;
	public float ballforce;
	public Transform ballTarget;
	public int totalBalls;
	public bool readyToshoot;
	public GameObject[] allLevels;
	public int currentLevel;
	Plane plane = new Plane(Vector3.forward, 0);

	public Ball ballScript;
	public bool gameHasStarted;

	public int shootedBall;


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
		gameHasStarted = true;
		readyToshoot = true;
	}

    void Start()
    {
		gameHasStarted = false;
		readyToshoot = false;
		Debug.Log(PlayerDataManager.inter.ScoreKnockOut);
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
			ball.GetComponent<Animator>().enabled = false;

			ball.transform.position = new Vector3(mousePos.x, ball.transform.position.y, ball.transform.position.z);

		}


		Vector3 dir = ballTarget.position - ball.transform.position;
		if (Input.GetMouseButtonUp(0) && readyToshoot)
		{
			//Shoot the ball
			ball.GetComponent<Rigidbody>().AddForce(dir * ballforce, ForceMode.Impulse);
			readyToshoot = false;
			shootedBall++;
			totalBalls--;
			UIManager.instance.UpdateBallIcons();

			if (totalBalls <= 0)
			{
				//Check Game over
				//print("GameOver");
				StartCoroutine(CheckGameOver());
			}

		}

		//place the target
		float dist;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (plane.Raycast(ray, out dist))
		{
			Vector3 point = ray.GetPoint(dist);
			ballTarget.position = new Vector3(point.x, point.y, 0);
		}

	}

	public void GroupFallenCheck()
	{

		if (AllGrounded())
		{
			// Load next level
			LoadNextLevel();
		}


	}

	bool AllGrounded()
	{
		Transform canSet = allLevels[currentLevel].transform;
		foreach (Transform t in canSet)
		{
			if (t.GetComponent<Can>().hasFallen == false)
			{
				return false;
			}
		}

		return true;
	}

	public void LoadNextLevel()
	{
		if (gameHasStarted)
		{
			StartCoroutine(LoadNextLevelRoutine());
		}
	}

	IEnumerator LoadNextLevelRoutine()
	{
#if UNITY_EDITOR
		Debug.Log("Loading Next Level");
#endif
		yield return new WaitForSeconds(1.5f);
		UIManager.instance.ShowBlackFade();
		readyToshoot = false;
		allLevels[currentLevel].SetActive(false);
		currentLevel++;

		if (currentLevel >= allLevels.Length)
		{
			//currentLevel = 0;
			StartCoroutine(InstndGameOver());
		}
		else
        {
			yield return new WaitForSeconds(1.0f);
			UIManager.instance.UpdateScoreMultiplier();
			shootedBall = 0;
			allLevels[currentLevel].SetActive(true);
			UIManager.instance.UpdateBallIcons();
			ballScript.RepositionBall();
			AddExtraBall(1);
		}
	}


	public void AddExtraBall(int count)
	{
		if (totalBalls < 5)
		{
			totalBalls += count;
			UIManager.instance.UpdateBallIcons();
		}
		StopAllCoroutines();
	}


	IEnumerator CheckGameOver()
	{
		yield return new WaitForSeconds(3.0f);
		if (AllGrounded() == false)
		{
#if UNITY_EDITOR
			Debug.Log($"TotalScore : {UIManager.instance.score}");
#endif
			UIManager.instance.UpdateFinalScore();
			//PlayerPrefs.SetInt("ScoreKnockOut", UIManager.instance.score);
			PlayerDataManager.inter.ScoreKnockOut = UIManager.instance.score;
			UIManager.instance.GameOverUI.SetActive(true);
			yield return new WaitForSeconds(3.0f);
			UIManager.instance.GameOverUI.GetComponent<Animator>().enabled = true;
			UIManager.instance.GameOverUI.GetComponent<Animator>().Play("fade_out");
			//SceneManager.LoadScene(1);			
			yield return new WaitForSeconds(2.0f);
			SceneManager.LoadScene("OpenWorld");
		}
	}

	IEnumerator InstndGameOver()
    {
		yield return new WaitForSeconds(3.0f);
#if UNITY_EDITOR
		Debug.Log($"TotalScore : {UIManager.instance.score}");
#endif
		UIManager.instance.UpdateFinalScore();
		UIManager.instance.GameUI.SetActive(false);
		UIManager.instance.gameScene.SetActive(false);
		//PlayerPrefs.SetInt("ScoreKnockOut", UIManager.instance.score);
		//PlayerDataManager.inter.ScoreKnockOut = UIManager.instance.score;

		if (UIManager.instance.score > PlayerDataManager.inter.ScoreKnockOut)
		{
			PlayerDataManager.inter.ScoreKnockOut = UIManager.instance.score;
		}

		PlayerDataManager.inter.gold += UIManager.instance.score;
		UIManager.instance.GameOverUI.SetActive(true);
		PlayerDataManager.inter.startalready = 1;
		yield return new WaitForSeconds(3.0f);
		UIManager.instance.GameOverUI.GetComponent<Animator>().enabled = true;
		UIManager.instance.GameOverUI.GetComponent<Animator>().Play("fade_out");
		yield return new WaitForSeconds(2.0f);
		SceneManager.LoadScene("OpenWorld");
	}
}
