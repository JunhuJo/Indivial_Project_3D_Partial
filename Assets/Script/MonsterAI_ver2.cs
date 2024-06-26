using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.AI;

public class MonsterAI_ver2 : MonoBehaviour
{

    [SerializeField] private int monsterType; // 몬스터 타입별로 근, 원거리 공격하게 함
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float targetDetectionRange = 10f;
    public float stoppingDistance = 1.5f; // 타겟에 근접하는 거리
    public float stoppingLongDistance = 5.0f; // 타겟에 근접하는 거리
    private Animator monsterAnimator;
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    private Transform target;

    void Start()
    {
        monsterAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        MoveToNextWaypoint();
    }

    void Update()
    {
        FindTarget();
        MonsterAi();
    }

    private void FindTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float closestDistance = targetDetectionRange;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = player.transform;
            }
        }

        if (closestDistance == targetDetectionRange)
        {
            target = null; // 탐지 반경 내에 타겟이 없으면 null로 설정
        }
    }

    private void MonsterAi()
    {
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= targetDetectionRange)
            {
                // 타겟이 일정 범위 내에 있을 때 타겟 근처로 이동
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - directionToTarget * stoppingDistance;
                Vector3 targetLongDistancePosition = target.position - directionToTarget * stoppingLongDistance;
                agent.SetDestination(targetPosition);

                if (distanceToTarget <= stoppingDistance+0.5f && monsterType == 1)
                {
                    monsterAnimator.SetTrigger("isMonsterAttack");
                }
                else if (distanceToTarget <= stoppingLongDistance && monsterType == 2)
                {
                    monsterAnimator.SetTrigger("isLongDistanceMonsterAttack");
                }
                //else if (Vector3.Distance(transform.position, targetLongDistancePosition) <= stoppingLongDistance && gameObject.CompareTag("Turret"))
                //{
                //    TurretRotate turretRotate = GetComponent<TurretRotate>();
                //    turretRotate.rotateSpeed = 0;
                //}
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
        {
            return;
        }

        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}
