using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public static LongClickButton instance;
	private bool pointerDown;
	//private float pointerDownTimer;
	private float power = 0;
	public float maxPower = 12;
	public float chargeSpeed = 10;
	private float timer = 0.0f;

	public Slider slider;

	/*[SerializeField]
	private float requiredHoldTime;*/

	public UnityEvent onLongClick;

	/*[SerializeField]
	private Image fillImage;*/

	public void OnPointerDown(PointerEventData eventData)
	{
		pointerDown = true;
		Debug.Log("OnPointerDown");
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Reset();
		Debug.Log("OnPointerUp");
	}

	private void Update()
	{
		if (pointerDown && power < maxPower)
		{
			/*pointerDownTimer += Time.deltaTime;
			if (pointerDownTimer >= requiredHoldTime)
			{
				if (onLongClick != null)
					onLongClick.Invoke();

				Reset();
			}*/
			//fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
			//GameManage_KnockOut.shootRing();
			//power += Time.deltaTime * chargeSpeed;
			//power = Mathf.PingPong(Time.time, maxPower
			//power = Mathf.PingPong(Time.time * chargeSpeed, maxPower);
			timer += Time.deltaTime;
			power = Mathf.PingPong(timer * chargeSpeed, maxPower);
			//Debug.Log(power);
			slider.value = power;
		}
	}

	private void Reset()
	{
		Debug.Log("Stop on: " + power);
		if (power >= 0.0f && power <= 4.0f)
		{
			Debug.Log("Can't Shoot Too low");
			pointerDown = false;
			//pointerDownTimer = 0;
			power = 0;
			timer = 0.0f;
			slider.value = 0.0f;
			//fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
		}
		else
        {
			GameManage_RingToss.instance.shootRing(power);
			pointerDown = false;
			//pointerDownTimer = 0;
			power = 0;
			timer = 0.0f;
			slider.value = 0.0f;
			//fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
		}
	}

}