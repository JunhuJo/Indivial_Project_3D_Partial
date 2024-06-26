using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    [SerializeField] private int monsterType;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Transform target;
    [SerializeField] private float targetDetectionRange = 10f;
    [SerializeField] private float stoppingDistance = 1.5f; 
    [SerializeField] private float stoppingLongDistance = 5.0f; 
    
    private Animator monsterAnimator;
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;

    void Start()
    {
        monsterAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        MoveToNextWaypoint();
    }

    void Update()
    {
        MonsterAi();
    }

    private void MonsterAi()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= targetDetectionRange)
        {
            // Ÿ���� ���� ���� ���� ���� �� Ÿ�� ��ó�� �̵�
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            Vector3 targetPosition = target.position - directionToTarget * stoppingDistance;
            Vector3 targetLongDistancePosition = target.position - directionToTarget * stoppingLongDistance;
            agent.SetDestination(targetPosition);

            if (gameObject.transform.position == targetPosition && monsterType == 1)
            {
                monsterAnimator.SetTrigger("isMonsterAttack");
            }
            else if (gameObject.transform.position == targetLongDistancePosition && monsterType == 2)
            {
                monsterAnimator.SetTrigger("isLongDistanceMonstgerAttack");
            }
            else if( gameObject.transform.position == targetLongDistancePosition && gameObject.CompareTag("Turret"))
            { 
                TurretRotate turretRotate = GetComponent<TurretRotate>();
                turretRotate.rotateSpeed = 0;
            }
        }
        else
        {
            // Waypoint�� �����ߴ��� Ȯ��
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                MoveToNextWaypoint();
            }
        }
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0)
        {
            return;
        }

        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}
