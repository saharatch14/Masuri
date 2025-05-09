using UnityEngine;

public class Bomb : MonoBehaviour, IHitable
{
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;
            FindObjectOfType<GameManager>().Explode();
        }
    }*/
    public void Hit(RaycastHit hit)
    {
        GetComponent<Collider>().enabled = false;
        FindObjectOfType<GameManager>().Explode();
    }

}
