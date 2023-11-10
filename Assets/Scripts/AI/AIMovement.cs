using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // Adjust the speed of rotation as needed

    Collider[] rigColliders;
    Rigidbody[] rigRigidbodies;
    public Collider mainCollider;

    private bool _isDead = false;
    private Animator anim;

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsPlayer, whatIsGround;

    public Vector3 movePoint;
    bool movePointSet;
    public float movePointRange;

    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    //Might need to change to vison field?
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public AIGun gun;
    void Start()
    {
        rigColliders = GetComponentsInChildren<Collider>();
        rigRigidbodies = GetComponentsInChildren<Rigidbody>();
        anim = GetComponent<Animator>();
        StopRagdoll();
        mainCollider.enabled = true;

    }
    private void Awake()
    {
        player = FindFirstObjectByType<PlayerBody>().transform;
        gun = this.GetComponentInChildren<AIGun>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_isDead)
            return;

        //transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)Patrolling();
        if (playerInSightRange && !playerInAttackRange)ChasePlayer();
        if (playerInSightRange && playerInAttackRange)AttackPlayer();

    }

    private void Patrolling()
    {
        if(!movePointSet) SearchMovePoint();

        if(movePointSet)
            agent.SetDestination(movePoint);

        Vector3 distanceToMovePoint = transform.position - movePoint;

        if(distanceToMovePoint.magnitude < 1f)
            movePointSet = false;
    }
    private void SearchMovePoint() 
    {
        float randomZ = Random.Range(-movePointRange, movePointRange);
        float randomX = Random.Range(-movePointRange, movePointRange);

        movePoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(movePoint, -transform.up, 2f, whatIsGround))
            movePointSet = true;
    }

    public void getBlasted(float blastForce, Vector3 explosionPosition, float blastRadius, float upwardModifier)
    {
        _isDead = true;
        anim.enabled = false;
        agent.enabled = false;
        StartRagdoll();

        foreach (Rigidbody rb in rigRigidbodies)
        {
            rb.AddExplosionForce(blastForce, explosionPosition, blastRadius, upwardModifier, ForceMode.Impulse);
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        Vector3 playerPositionXZ = new Vector3(player.position.x, 0, player.position.z);
        transform.LookAt(playerPositionXZ);

        if (!alreadyAttacked)
        {
            if(gun != null)
            gun.Shoot();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void StartRagdoll()
    {
        foreach (Rigidbody rb in rigRigidbodies)
        {
            rb.isKinematic = false;
        }
        foreach (Collider col in rigColliders)
        {
            col.enabled = true;
        }
    }

    private void StopRagdoll()
    {
        foreach (Rigidbody rb in rigRigidbodies)
        {
            rb.isKinematic = true;
        }
        foreach (Collider col in rigColliders)
        {
            col.enabled = false;
        }
    }
}
