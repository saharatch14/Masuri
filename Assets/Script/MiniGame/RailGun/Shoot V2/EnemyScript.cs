using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour, IHitable
{
    [SerializeField] int maxHealth;
    [SerializeField] Transform targetPos;

    private int currentHealth;
    private Transform player;
    private bool isDead;
    private NavMeshAgent agent;
    private ShootOutPoint shootOutPoint;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = Camera.main.transform;

        agent.updateRotation = false;
    }

    public void Init(ShootOutPoint point)
    {
        currentHealth = maxHealth;
        shootOutPoint = point;

        if (agent != null)
        {
            agent.SetDestination(targetPos.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && !isDead)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0f;

            transform.rotation = Quaternion.LookRotation(direction);
        }
    }


    public void Hit(RaycastHit hit)
    {
        if (isDead)
            return;

        currentHealth--;
        Debug.Log("Enemy Shot!");

        if (currentHealth <= 0)
        {
            isDead = true;
            agent.enabled = false;
            shootOutPoint.EnemyKilled();
            Destroy(gameObject);
        }
    }
}
