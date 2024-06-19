using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    [SerializeField] private int monsterType;// 몬스터 타입별로 근,원거리 공격하게 함
    [SerializeField] private Transform[] waypoints; 
    [SerializeField] private Transform target;
    [SerializeField] private float targetDetectionRange = 10f;
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
            agent.SetDestination(target.position);

            if (monsterType == 1 || distanceToTarget == 1) // 근거리
            {
                monsterAnimator.SetTrigger("isMonsterAttack");
            }

            if (monsterType == 2 || distanceToTarget == 3) { }// 원거리
            {
                monsterAnimator.SetTrigger("isLongDistanceMonsterAttack");
            }
        }
        else
        {
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
