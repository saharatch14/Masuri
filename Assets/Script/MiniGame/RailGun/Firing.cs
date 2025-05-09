using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{
    public float fireRange;
    //public GameObject Scatter1;
    //public GameObject Scatter2;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hit,fireRange))
        {
            Debug.Log(hit.transform.name);

            //GameObject ImpactGO1 = Instantiate(Scatter1,hit.point, Quaternion.LookRotation(hit.normal))
            //Destroy(ImpactGO1,1f);
            //GameObject ImpactGO2 = Instantiate(Scatter2,hit.point, Quaternion.LookRotation(hit.normal))
            //Destroy(ImpactGO2,1f);
        }
    }
}
