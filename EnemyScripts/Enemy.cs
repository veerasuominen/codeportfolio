using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public PlayerController playerController;
    public NavMeshAgent enemy;
    public Transform player, bulletSpawn;
    public LayerMask groundLayer, playerLayer, obstacle;
    public float health;

    //Patroling

    public GameObject visionCone;
    public Collider visionConeCollider;
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkRange;

    //Attacking

    public float attackSpeed;
    public bool alreadyAttacked;
    public GameObject projectile;

    //Ranges

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Start()
    {
        player = GameObject.Find("Fox").transform;
        enemy = GetComponent<NavMeshAgent>();
        playerInSightRange = false;
        playerInAttackRange = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInSightRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInSightRange = false;
        }
    }

    private void Update()
    {
        //checks if player is in range
        //playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        //playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            enemy.SetDestination(walkPoint);
        }

        //calculates the distance to walk point
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //when walkpoint reached(when distance to it is less than 1)
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkRange, walkRange);
        float randomX = Random.Range(-walkRange, walkRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        enemy.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        enemy.SetDestination(transform.position);

        //Rotate to look at player
        transform.LookAt(player);

        //if attack hasnt happened or hasnt reset yet
        //invokes attack reset, spawns a bullet and damages player
        if (!alreadyAttacked)
        {
            Instantiate(projectile, bulletSpawn.transform.position, transform.rotation);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackSpeed);
            playerController.health--;
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    //draws the circles representing attack and sightrange but i didnt end up needing them
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, sightRange);
    //}
}