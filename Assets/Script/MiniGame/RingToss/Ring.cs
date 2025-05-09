using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
	Vector3 ringSpwanPos;
	Quaternion ringSpwanRot;
	public bool isringinside;
	// Use this for initialization
	void Start()
	{
		isringinside = false;
		ringSpwanPos = transform.position;
		ringSpwanRot = transform.rotation;
		//Debug.Log(ringSpwanPos);
		//Debug.Log(ringSpwanRot);
        //gameObject.GetComponent<Renderer>().material.color = Color.green;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Resetter"))
		{
			//Debug.Log("fall");
			/*if (GameManage_RingToss.instance.totalRings > 0)
			{
				RepositionBall();
			}*/
			StartCoroutine("SetReadyToShoot");
			//RepositionToss();
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Hold"))
		{
			//Debug.Log("goal");
			/*if (GameManage_RingToss.instance.totalRings > 0)
			{
				RepositionBall();
			}*/
			StartCoroutine("ReadyToCount");
			//RepositionToss();
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Hold"))
		{
			//Debug.Log("out");
			/*if (GameManage_RingToss.instance.totalRings > 0)
			{
				RepositionBall();
			}*/
			StopCoroutine("ReadyToCount");
			//RepositionToss();
		}
	}
    private void OnCollisionEnter(Collision other)
    {
		if (other.gameObject.CompareTag("Delete"))
		{
			StartCoroutine("SetReadyToShootfromout");
		}
	}

    /*public void RepositionToss()
	{
		gameObject.SetActive(false);
		transform.position = ringSpwanPos;
		transform.rotation = ringSpwanRot;
		Debug.Log(transform.position);
		while(transform.position != ringSpwanPos && transform.rotation != ringSpwanRot)
        {
			transform.position = ringSpwanPos;
			transform.rotation = ringSpwanRot;
		}
		//this.GetComponent<Animator>().enabled = true;
		gameObject.SetActive(true);
		StartCoroutine(SetReadyToShoot());
	}*/

    IEnumerator SetReadyToShoot()
	{
		if(GameManage_RingToss.instance.totalRings > 0)
        {
			yield return new WaitForSeconds(3.0f);
			GameManage_RingToss.instance.SpwaneRing(ringSpwanPos, ringSpwanRot, true);
			GameManage_RingToss.instance.readyToshoot = true;
			GameManage_RingToss.instance.beenSpawnedOnce = true;
		}
		else
        {
			yield return new WaitForSeconds(3.0f);
			GameManage_RingToss.instance.SpwaneRing(ringSpwanPos, ringSpwanRot, false);
			//GameManage_RingToss.instance.readyToshoot = false;
			//GameManage_RingToss.instance.beenSpawnedOnce = false;
		}
	}

	IEnumerator SetReadyToShootfromout()
	{
		if (GameManage_RingToss.instance.totalRings > 0)
		{
			yield return new WaitForSeconds(1.0f);
			GameManage_RingToss.instance.SpwaneRing(ringSpwanPos, ringSpwanRot, true);
			GameManage_RingToss.instance.readyToshoot = true;
			GameManage_RingToss.instance.beenSpawnedOnce = true;
			Destroy(gameObject);
		}
		else
		{
			yield return new WaitForSeconds(1.0f);
			GameManage_RingToss.instance.SpwaneRing(ringSpwanPos, ringSpwanRot, false);
			//GameManage_RingToss.instance.readyToshoot = false;
			//GameManage_RingToss.instance.beenSpawnedOnce = false;
			Destroy(gameObject);
		}
	}

	IEnumerator ReadyToCount()
	{
		yield return new WaitForSeconds(3.0f);
		if (isringinside == false)
		{
			isringinside = true;
			UIManager_RingToss.instance.UpdateScore();
			if (GameManage_RingToss.instance.totalRings > 0)
			{
				GameManage_RingToss.instance.SpwaneRing(ringSpwanPos, ringSpwanRot, true);
				GameManage_RingToss.instance.readyToshoot = true;
				GameManage_RingToss.instance.beenSpawnedOnce = true;
			}
			else
			{
				GameManage_RingToss.instance.SpwaneRing(ringSpwanPos, ringSpwanRot, false);
				//GameManage_RingToss.instance.readyToshoot = false;
				//GameManage_RingToss.instance.beenSpawnedOnce = false;
			}
		}
	}
}
