using System.Collections;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Skill_A")]
    [SerializeField] private GameObject Darkness_Bullet;

    [Header("Skill_B")]
    [SerializeField] private GameObject SkillB;

    [Header("Skill_C")]
    [SerializeField] private GameObject Sword_Attack_Effect;
    [SerializeField] private AudioSource Sword_Attack_Sound;
    private AudioClip Sword_Attack_SoundClip;
    [SerializeField] private Transform Sword_Attack_Pos;
    [SerializeField] private GameObject Rifle;
    [SerializeField] private GameObject Katana;


    private PlayerMove playerMove;


    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        Sword_Attack_SoundClip = Sword_Attack_Sound.clip;


    }

    private void Update()
    {
        //��ų E(�˽�ų)
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(OnSwordAttack());
        }
    }

    IEnumerator OnSwordAttack()
    {
        animator.SetTrigger("isSwordAttack");
        Rifle.gameObject.SetActive(false);
        Katana.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        Sword_Attack_Effect.gameObject.SetActive(true);
        Sword_Attack_Sound.PlayOneShot(Sword_Attack_SoundClip);
        playerMove.enabled = false;
        animator.SetBool("isRun", false);
        yield return new WaitForSeconds(0.5f);
        Katana.gameObject.SetActive(false);
        Sword_Attack_Effect.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        Rifle.gameObject.SetActive(true);
        //yield return new WaitForSeconds(0.6f);
        playerMove.enabled = true;
    }






}
