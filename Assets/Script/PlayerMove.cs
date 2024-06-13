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
    [SerializeField] private GameObject EffectPrefab; // ����Ʈ ������ ����

    [Header("Attack")]
    [SerializeField] private KeyCode _atkKey = KeyCode.A;
    [SerializeField] private GameObject bullet_Prefab;
    [SerializeField] private Transform Tranform_AtkSpawnPos;
    


    //[SerializeField] private int _health = 4;

    private void Update()
    {
        PlayerMoveOnUpdate();
    }

    private void PlayerMoveOnUpdate()
    {
        //�̵�
        if (Input.GetMouseButtonDown(0)) // ��Ŭ�� ��
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point);
                NavAgent_Player.SetDestination(hit.point); // Ŭ���� ��ġ�� �̵�
            }

            GameObject pointer_Effect = Instantiate(EffectPrefab, hit.point, Quaternion.identity);
            Destroy(pointer_Effect, 0.7f);
        }


        // �̵� ������ Ȯ���ϰ� �ִϸ��̼� ���¸� ����
        if (NavAgent_Player.remainingDistance > NavAgent_Player.stoppingDistance)
        {
            Animator_Player.SetBool("isMove", true);
        }
        else
        {
            Animator_Player.SetBool("isMove", false);
        }


        //����
        if (Input.GetKeyDown(_atkKey))
        {
            Animator_Player.SetBool("isAttack", true);
            CommandAtk();
        }

    }


    private void CommandAtk()
    {

        GameObject atkObjectForSpawn = Instantiate(bullet_Prefab, Tranform_AtkSpawnPos.transform.position, Tranform_AtkSpawnPos.transform.rotation);
        Destroy(atkObjectForSpawn, 2.0f);
        //Animator_Player.SetBool("isAttack", false);

        //private void SetHealthBarOnUpdate(int health)
        //{Tranform_AtkSpawnPos.transform.rotation
        //    TextMesh_HealthBar.text = new string('-', health);
        //}
    }
}
