using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public enum BossState
    {
        Idle,
        Chasing,
        Attacking,
        UsingSkill
    }

    [SerializeField] private float targetDetectionRange = 10f;
    [SerializeField] private float stoppingDistance = 1.5f; // Ÿ�ٿ� �����ϴ� �Ÿ�
    private Animator bossAnimator;
    private NavMeshAgent agent;
    private Transform target;
    private BossSkill bossSkill;
    private BossState currentState;

    void Start()
    {
        bossSkill = GetComponent<BossSkill>();
        bossAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        currentState = BossState.Idle;

    }

    void Update()
    {
       
        FindTarget();
        MonsterAi();
       
    }

    private void FindTarget()
    {
        if (currentState == BossState.Attacking || currentState == BossState.UsingSkill)
            return;

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
            target = null; // Ž�� �ݰ� ���� Ÿ���� ������ null�� ����
        }
    }

    private void MonsterAi()
    {
        if (currentState == BossState.Attacking || currentState == BossState.UsingSkill)
            return;

        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= targetDetectionRange)
            {
                // Ÿ���� ���� ���� ���� ���� �� Ÿ�� ��ó�� �̵�
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - directionToTarget * stoppingDistance;
                //Vector3 targetLongDistancePosition = target.position - directionToTarget * stoppingLongDistance;
                agent.SetDestination(targetPosition);

                if (transform.position != Vector3.zero)
                {
                    bossAnimator.SetBool("isBossRun", true);
                }
                else
                {
                    bossAnimator.SetBool("isBossRun", false);
                }

                if (distanceToTarget <= stoppingDistance + 0.8f)
                {
                    agent.velocity = Vector3.zero;
                    bossAnimator.SetTrigger("isBossAttack");
                    currentState = BossState.Attacking;
                }
            }
        }
        else
        {
            bossAnimator.SetBool("isBossRun", false);
        }
    }

    public void OnAttackComplete()
    {
        Debug.Log("Attack complete!");
        currentState = BossState.Chasing;
        bossAnimator.ResetTrigger("isBossAttack");
    }

    public void OnSkillComplete()
    {
        Debug.Log("Skill complete!");
        currentState = BossState.Chasing;
    }

    public void UseSkill()
    {
        if (currentState != BossState.Attacking)
        {
            currentState = BossState.UsingSkill;
            bossSkill.PrepareSkill();
            // ��ų ��� �ִϸ��̼� �� ����
        }
    }
}
