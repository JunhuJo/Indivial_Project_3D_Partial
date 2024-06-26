using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private NavMeshAgent NavAgent_Player;
    [SerializeField] private Animator Animator_Player;
    [SerializeField] private TextMesh TextMesh_HealthBar;
    [SerializeField] private TextMesh Nick_Name;
    [SerializeField] private Transform Transform_Player;

    [Header("Movement")]
    [SerializeField] private float _ratationSpeed = 100.0f;
    [SerializeField] private float _moveSpeed = 5.0f;

    [Header("PointerEffect")]
    [SerializeField] private GameObject EffectPrefab; // ����Ʈ ������ ����

    [Header("Attack")]
    [SerializeField] private KeyCode _atkKey = KeyCode.A;
    [SerializeField] private GameObject bullet_Prefab;
    [SerializeField] private Transform Tranform_AtkSpawnPos;
    [SerializeField] private float shootpoint = 5.0f;
    [SerializeField] private GameObject shoot_Effect;
    [SerializeField] private Transform shoot_Effect_Pos;
    private bool baseAttack = false;
    private SkillManager skillManager;
    

    [Header("ShootSound")]
    [SerializeField] private AudioSource AudioSource;
    private AudioClip shootsound;


    //[SerializeField] private int _health = 4;

    private void Start()
    {
        shootsound = AudioSource.clip;
        skillManager = GetComponent<SkillManager>();

        // NavMeshAgent�� ȸ�� �ڵ� ������Ʈ ��Ȱ��ȭ///////
        //NavAgent_Player.updateRotation = false;

    }

    private void Update()
    {
        PlayerMoveOnUpdate();
    }


    private void PlayerMoveOnUpdate()
    {
        //�̵�
        if (Input.GetMouseButtonDown(0))// ��Ŭ�� ��
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point);
                NavAgent_Player.SetDestination(hit.point); // Ŭ���� ��ġ�� �̵�
                if (Input.GetKeyDown(KeyCode.Q)|| Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E)|| Input.GetKeyDown(KeyCode.R))
                { 
                    hit.point = gameObject.transform.position;
                }
            }

            GameObject pointer_Effect = Instantiate(EffectPrefab, hit.point, Quaternion.identity);
            Destroy(pointer_Effect, 0.7f);
        }

        //// ��ų ��� �� ĳ���� ȸ��
        //if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
        //{
        //    RotateTowardsMousePointer();
        //}



        // �̵� ������ Ȯ���ϰ� �ִϸ��̼� ���¸� ����
        if (NavAgent_Player.remainingDistance > NavAgent_Player.stoppingDistance && gameObject.transform.position != Vector3.zero)
        {
            Animator_Player.SetBool("isRun", true);
        }
        else
        {
            Animator_Player.SetBool("isRun", false);
        }

        //// �̵� ������ Ȯ���ϰ� �ִϸ��̼� ���¸� ������Ʈ
        //if (NavAgent_Player.remainingDistance > NavAgent_Player.stoppingDistance)
        //{
        //    Animator_Player.SetBool("isRun", true);
        //    RotateTowardsDestination();
        //}
        //else
        //{
        //    Animator_Player.SetBool("isRun", false);
        //}



        //�⺻ ����
        if (Input.GetMouseButtonDown(1))
        {
            if (!baseAttack && !skillManager.SetbattleMode)
            {
                StartCoroutine(CommandAtk());
            }
            else if(skillManager.SetbattleMode)
            {
                StartCoroutine(AwakeBaseAttack());
            }
        }
    }

    // ���콺 ������ �������� ȸ�� ///////
    private void RotateTowardsMousePointer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 500))
        {
            Vector3 targetDirection = hit.point - transform.position;
            targetDirection.y = 0; // ���� ���̴� ����
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _ratationSpeed * Time.deltaTime);
        }
    }

    private void RotateTowardsDestination()
    {
        if (NavAgent_Player.hasPath)
        {
            Vector3 direction = NavAgent_Player.steeringTarget - transform.position;
            direction.y = 0; // ���� ���̴� ����
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _ratationSpeed * Time.deltaTime);
        }
    }



    IEnumerator CommandAtk()
    {
        baseAttack = true;
        if (Animator_Player.GetBool("isRun"))
        {
            Animator_Player.SetBool("isRunAttack", true);
        }
        else if(!Animator_Player.GetBool("isRun"))
        {
            Animator_Player.SetBool("isAttack", true);
        }
       
        yield return new WaitForSeconds(0.5f);
        GameObject shootEffect = Instantiate(shoot_Effect, shoot_Effect_Pos.transform.position, shoot_Effect_Pos.transform.rotation);
        Destroy(shootEffect, 0.5f);
        AudioSource.PlayOneShot(shootsound);

        yield return new WaitForSeconds(0.1f);
        GameObject atkObjectForSpawn = Instantiate(bullet_Prefab, Tranform_AtkSpawnPos.transform.position, Tranform_AtkSpawnPos.transform.rotation);
        Destroy(atkObjectForSpawn, 2.0f);
        yield return new WaitForSeconds(0.5f);

        Animator_Player.SetBool("isRunAttack", false);
        Animator_Player.SetBool("isAttack", false);
        baseAttack = false;
    }

    IEnumerator AwakeBaseAttack()
    {
        Animator_Player.SetTrigger("isBaseAttack");
        skillManager.battle_Mode_Weapon_Start.SetActive(false);
        skillManager.battle_Mode_Weapon_Attack.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        skillManager.battle_Mode_Weapon_Start.SetActive(true);
        skillManager.battle_Mode_Weapon_Attack.SetActive(false);
    }
}
