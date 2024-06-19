using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    [SerializeField] private int monsterType;// ���� Ÿ�Ժ��� ��,���Ÿ� �����ϰ� ��
    [SerializeField] private Transform[] waypoints; 
    [SerializeField] private Transform target;
    [SerializeField] private float targetDetectionRange = 10f;
    public float stoppingDistance = 1f; // Ÿ�ٿ� �����ϴ� �Ÿ�
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
            agent.SetDestination(targetPosition);
            if (gameObject.transform.position == targetPosition)
            {
                monsterAnimator.SetTrigger("isMonsterAttack");
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
            return;

        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}
