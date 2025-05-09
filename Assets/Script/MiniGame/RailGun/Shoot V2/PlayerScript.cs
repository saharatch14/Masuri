using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private UiMenu userinterface;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && userinterface.isPause() == false && GameManager.instance.IsGameReady() == true)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 50f))
            {
                if (hit.collider != null)
                {
                    IHitable hitable = hit.collider.GetComponent<IHitable>();
                    AudioController.instance.PlaySFX("GunShot");

                    if (hitable != null)
                    {
                        hitable.Hit(hit);
                    }
                    //AudioController.instance.PlaySFX("HitWall");
                    //Debug.Log(hit.collider.gameObject.name);
                }
            }
        }
    }
}
