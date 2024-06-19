using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    [SerializeField] private int monsterType;// 몬스터 타입별로 근,원거리 공격하게 함
    [SerializeField] private Transform[] waypoints; 
    [SerializeField] private Transform target;
    [SerializeField] private float targetDetectionRange = 10f;
    public float stoppingDistance = 1f; // 타겟에 근접하는 거리
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
            // 타겟이 일정 범위 내에 있을 때 타겟 근처로 이동
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
            // Waypoint에 도착했는지 확인
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
