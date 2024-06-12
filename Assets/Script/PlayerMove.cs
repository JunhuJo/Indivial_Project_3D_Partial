using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    [Header("Componets")]
    //[SerializeField] private NavMeshAgent NavAgent_Player;
    [SerializeField] private Animator Animator_Player;
    [SerializeField] private TextMesh TextMesh_HealthBar; // HP는 슬라이더로 대체 될 예정
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

    public void Update()
    {
        //SetHealthBarOnUpdate(_health);

        PlayerMoveOnUpdate();
    }

    //private void SetHealthBarOnUpdate(int health)
    //{
    //    TextMesh_HealthBar.text = new string('-', health);
    //}

    private void PlayerMoveOnUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            Debug.DrawLine(ray.origin, hit.point);
            Vector3 lookRotate = new Vector3(hit.point.x, Transform_Player.position.y, hit.point.z);
            Transform_Player.LookAt(lookRotate);

            float horizontal = Input.GetAxis("Horizontal");
            transform.Rotate(0, horizontal * _ratationSpeed * Time.deltaTime, 0);

            float vetrial = Input.GetAxis("Vertical");
            transform.Translate(Vector3.forward * vetrial * _moveSpeed * Time.deltaTime);


            //if (Input.GetKeyDown(_atkKey))
            //{
            //    //CommandAtk();
            //}
        }
    }

    //private void CommandAtk()
    //{
    //    GameObject atkObjectForSpawn = Instantiate(Prefab_AtkObject, Tranform_AtkSpawnPos.transform.position, Tranform_AtkSpawnPos.transform.rotation);
    //}
}
