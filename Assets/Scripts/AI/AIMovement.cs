using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // Adjust the speed of rotation as needed
    private EnemyManager enemyManager;

    public GameObject[] Trails;
    Collider[] rigColliders;
    Rigidbody[] rigRigidbodies;
    public Rigidbody mainRb;
    public Collider mainCollider;
    public Collider capsuleCollider;

    private bool _isDead = false;
    private Animator anim;

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsPlayer, whatIsGround;

    public Vector3 patrollingPoint;
    bool movePointSet;
    public float patrollingPointRange;

    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    public float sightRange, sightAngle, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public AIGun gun;
    private bool agentActive = true;
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
        if (!agent.enabled)
            agentActive = false;
        enemyManager = FindFirstObjectByType<EnemyManager>();
    }

    void Update()
    {
        if (_isDead)
            return;

        playerInSightRange = FieldOfViewCheck(sightRange);
        playerInAttackRange = FieldOfViewCheck(attackRange);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private bool FieldOfViewCheck(float range)
    {
        bool canSeePlayer;
        Collider[] rangeChecks = Physics.OverlapSphere(gun.transform.position, range, whatIsPlayer);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            //Makes sure the raycast goes straight to the player and not the ground below
            Vector3 position = new Vector3(target.position.x, target.position.y + 1f, target.position.z);
            Vector3 directionToTarget = (position - gun.transform.position).normalized;

            if (Vector3.Angle(gun.transform.forward, directionToTarget) < sightAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(gun.transform.position, target.position);

                //Debug.DrawRay(gun.transform.position, directionToTarget * distanceToTarget, Color.yellow);
                if (!Physics.Raycast(gun.transform.position, directionToTarget, distanceToTarget, whatIsGround))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else
            canSeePlayer = false;

        return canSeePlayer;
    }

    private void Patrolling()
    {
        if (!movePointSet) SearchPatrollingPoint();

        if (movePointSet && agentActive)
            agent.SetDestination(patrollingPoint);

        Vector3 distanceToMovePoint = transform.position - patrollingPoint;

        if (distanceToMovePoint.magnitude < 1f)
            movePointSet = false;
    }
    private void SearchPatrollingPoint()
    {
        float randomZ = Random.Range(-patrollingPointRange, patrollingPointRange);
        float randomX = Random.Range(-patrollingPointRange, patrollingPointRange);

        patrollingPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(patrollingPoint, -transform.up, 2f, whatIsGround))
            movePointSet = true;
    }

    public void getBlasted(float blastForce, Vector3 explosionPosition, float blastRadius, float upwardModifier)
    {
        _isDead = true;
        anim.enabled = false;
        agent.enabled = false;
        StartRagdoll();
        Destroy(gameObject, 5);
        foreach (Rigidbody rb in rigRigidbodies)
        {
            rb.AddExplosionForce(blastForce, explosionPosition, blastRadius, upwardModifier, ForceMode.Impulse);
        }
    }

    private void ChasePlayer()
    {
        if (agentActive)
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        if (agentActive)
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            if (gun != null)
                gun.Shoot(player);

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
            if (rb != mainRb)
            rb.isKinematic = false;
        }
        foreach (Collider col in rigColliders)
        {
            if (col != capsuleCollider)
            col.enabled = true;
        }
    }

    private void StopRagdoll()
    {
        foreach (Rigidbody rb in rigRigidbodies)
        {
            if (rb != mainRb)
                rb.isKinematic = true;
        }
        foreach (Collider col in rigColliders)
        {
            if (col != capsuleCollider)
                col.enabled = false;
        }
    }

    public void ActivateTrails()
    {
        StartCoroutine(startTrails());
    }

    private IEnumerator startTrails()
    {
        Trails[0].GetComponent<TrailRenderer>().enabled = true;
        Trails[1].GetComponent<TrailRenderer>().enabled = true;
        yield return new WaitForSeconds(3);
        Trails[0].GetComponent<TrailRenderer>().enabled = false;
        Trails[1].GetComponent<TrailRenderer>().enabled = false;
    }
}
