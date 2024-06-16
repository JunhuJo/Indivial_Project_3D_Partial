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
    [SerializeField] private GameObject Katana_Sub;


    private PlayerMove playerMove;


    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        Sword_Attack_SoundClip = Sword_Attack_Sound.clip;


    }

    private void Update()
    {
        //스킬 B(라이플스킬)
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(OnSitShoot());
        }

        //스킬 C(소드스킬)
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(OnSwordAttack());
        }
    }

    IEnumerator OnSitShoot()
    {
        //yield return null;
        animator.SetBool("isSitShootA", true);
        playerMove.enabled = false;
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("isSitShootA", false);
        animator.SetBool("isSitShootB", true);
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("isSitShootB", false);
        animator.SetBool("isSitShootC", true);
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("isSitShootC", false);
        yield return new WaitForSeconds(0.8f);
        playerMove.enabled = true;
    }


    IEnumerator OnSwordAttack()
    {
        playerMove.enabled = false;
        animator.SetTrigger("isSwordAttack");
        Rifle.gameObject.SetActive(false);
        Katana.gameObject.SetActive(true);
        Katana_Sub.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.0f);

        Sword_Attack_Effect.gameObject.SetActive(true);
        Sword_Attack_Sound.PlayOneShot(Sword_Attack_SoundClip);
        animator.SetBool("isRun", false);

        yield return new WaitForSeconds(0.5f);
        Katana.gameObject.SetActive(false);
        Sword_Attack_Effect.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.10f);
        Rifle.gameObject.SetActive(true);
        Katana_Sub.gameObject.SetActive(true);
        //yield return new WaitForSeconds(0.6f);
        playerMove.enabled = true;
    }






}
