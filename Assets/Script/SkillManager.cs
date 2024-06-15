using System.Collections;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject Darkness_Bullet;
    [SerializeField] private GameObject SkillB;
    [SerializeField] private GameObject Sword_Attack;
    [SerializeField] private Transform Sword_Attack_Pos;
    private PlayerMove playerMove;


    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        //스킬 E(검스킬)
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(OnSwordAttack());
        }
    }

    IEnumerator OnSwordAttack()
    {
        animator.SetTrigger("isSwordAttack");
        playerMove.enabled = false;
        yield return new WaitForSeconds(1.0f);
        playerMove.enabled = true;
    }






}
