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


    //[Header("Attack")]
    //[SerializeField] private KeyCode _atkKey = KeyCode.A;
    //[SerializeField] private GameObject Prefab_AtkObject;
    //[SerializeField] private Transform Tranform_AtkSpawnPos;

    //[SerializeField] private int _health = 4;

    private void Update()
    {
        PlayerMoveOnUpdate();
    }


    //private void SetHealthBarOnUpdate(int health)
    //{
    //    TextMesh_HealthBar.text = new string('-', health);
    //}

    private void PlayerMoveOnUpdate()
    {
        if (Input.GetMouseButtonDown(0)) // ��Ŭ�� ��
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point);
                NavAgent_Player.SetDestination(hit.point); // Ŭ���� ��ġ�� �̵�
            }
        }

        // �ִϸ��̼��̳� �ٸ� ������ �߰��� �� �ֽ��ϴ�.
        // ���� ���, NavMeshAgent�� �̵� ������ Ȯ���ϰ� �ִϸ��̼� ���¸� ������Ʈ�մϴ�.
        if (NavAgent_Player.remainingDistance > NavAgent_Player.stoppingDistance)
        {
            Animator_Player.SetBool("isMove", true);
        }
        else
        {
            Animator_Player.SetBool("isMove", false);
        }
    }
}
