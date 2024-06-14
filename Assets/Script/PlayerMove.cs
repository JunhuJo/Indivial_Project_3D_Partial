using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject EffectPrefab; // 이펙트 프리팹 변수

    [Header("Attack")]
    [SerializeField] private KeyCode _atkKey = KeyCode.A;
    [SerializeField] private GameObject bullet_Prefab;
    [SerializeField] private Transform Tranform_AtkSpawnPos;

    [Header("ShootSound")]
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip shootsound;


    //[SerializeField] private int _health = 4;

    private void Start()
    {
        shootsound = AudioSource.clip;
    }



    private void Update()
    {
        PlayerMoveOnUpdate();
    }

    private void PlayerMoveOnUpdate()
    {
        //이동
        if (Input.GetMouseButtonDown(0)) // 좌클릭 시
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point);
                NavAgent_Player.SetDestination(hit.point); // 클릭한 위치로 이동
            }

            GameObject pointer_Effect = Instantiate(EffectPrefab, hit.point, Quaternion.identity);
            Destroy(pointer_Effect, 0.7f);
        }


        // 이동 중인지 확인하고 애니메이션 상태를 업뎃
        if (NavAgent_Player.remainingDistance > NavAgent_Player.stoppingDistance)
        {
            Animator_Player.SetBool("isMove", true);
        }
        else
        {
            Animator_Player.SetBool("isMove", false);
        }


        //공격
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(CommandAtk());
        }

    }

    IEnumerator CommandAtk()
    {
        if (Animator_Player.GetBool("isMove"))
        {
            Animator_Player.SetBool("isAttack", true);
        }
        else if (!Animator_Player.GetBool("isMove"))
        {
            Animator_Player.SetBool("isStopAttack", true);
        }


        if (Animator_Player.GetBool("isAttack") || Animator_Player.GetBool("isStopAttack"))
        {
            yield return new WaitForSeconds(0.3f);
            AudioSource.PlayOneShot(shootsound);
            GameObject atkObjectForSpawn = Instantiate(bullet_Prefab, Tranform_AtkSpawnPos.transform.position, Tranform_AtkSpawnPos.transform.rotation);
            Destroy(atkObjectForSpawn, 2.0f);
            yield return new WaitForSeconds(0.5f);
        }


        if (Animator_Player.GetBool("isAttack") && !Animator_Player.GetBool("isStopAttack"))
        {
            Animator_Player.SetBool("isAttack", false);
        }
        else
        {
            Animator_Player.SetBool("isStopAttack", false);
        }


        
    }


    //private void CommandAtk()
    //{
    //    Animator_Player.SetBool("isAttack", true);
    //    GameObject atkObjectForSpawn = Instantiate(bullet_Prefab, Tranform_AtkSpawnPos.transform.position, Tranform_AtkSpawnPos.transform.rotation);
    //    ShoutSound.PlayOneShot(Shoot_Sound_Clip);
    //    Destroy(atkObjectForSpawn, 2.0f);
    //    if()
    //    //Animator_Player.SetBool("isAttack", false);
    //
    //    //private void SetHealthBarOnUpdate(int health)
    //    //{Tranform_AtkSpawnPos.transform.rotation
    //    //    TextMesh_HealthBar.text = new string('-', health);
    //    //}
    //}
}
