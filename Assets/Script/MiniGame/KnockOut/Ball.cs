using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	Vector3 ballSpwanPos;

	// Use this for initialization
	void Start () {
		ballSpwanPos = transform.position;
	}

	void OnTriggerEnter(Collider other)
	{
		
		if(other.gameObject.CompareTag ("Resetter") )
		{
			if(GameManage_KnockOut.instance.totalBalls > 0)
			{
				RepositionBall();

			}

		}
			
	}

	public void RepositionBall()
	{
		gameObject.SetActive(false);
		transform.position = ballSpwanPos;
		this.GetComponent<Animator>().enabled = true;
		gameObject.SetActive(true);
		StartCoroutine(SetReadyToShoot());

	}

	IEnumerator SetReadyToShoot()
	{
		gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		yield return new WaitForSeconds(2.0f);
		GameManage_KnockOut.instance.readyToshoot = true;
	}
}
