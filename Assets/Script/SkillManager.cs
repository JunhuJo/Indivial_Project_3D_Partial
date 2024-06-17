using System.Collections;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Skill_Q")]
    [SerializeField] private GameObject darkness_Shoot;

    [Header("Skill_W")]
    [SerializeField] private GameObject sniping_shoot_Bullet;
    [SerializeField] private GameObject sniping_shoot_Effect;
    private AudioClip sniping_SoundClip;
    
    [Header("Skill_E")]
    [SerializeField] private GameObject sword_Attack_Effect;
    [SerializeField] private AudioSource sword_Attack_Sound;
    private AudioClip sword_Attack_SoundClip;
    [SerializeField] private GameObject Rifle;
    [SerializeField] private GameObject Katana;
    [SerializeField] private GameObject Katana_Sub;

    [Header("Skill_R")]
    [SerializeField] private ParticleSystem Battle_Mode_Change_Effect;

    [Header("Common")]
    [SerializeField] private AudioSource shoot_Sound;
    [SerializeField] private RifleSoundChanger changer;
    [SerializeField] private Transform attack_Pos;
    [SerializeField] private Transform effect_Pos;
    private PlayerMove playerMove;


    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        sword_Attack_SoundClip = sword_Attack_Sound.clip;
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

        //스킬 D(배틀모드 변환)
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(OnBattlModeChange());
        }
    }

    IEnumerator OnSitShoot()//스킬 W
    {
        changer.temp_SoundClip = shoot_Sound.clip;
        shoot_Sound.clip = changer.sniping_SoundClip_First;
        animator.SetBool("isSitShootA", true);
        shoot_Sound.PlayOneShot(shoot_Sound.clip);
        playerMove.enabled = false;
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("isSitShootA", false);
        animator.SetBool("isSitShootB", true);
        shoot_Sound.clip = changer.sniping_SoundClip_Scound;
        shoot_Sound.PlayOneShot(shoot_Sound.clip);
        GameObject sniping_Effect = Instantiate(sniping_shoot_Effect, effect_Pos.transform.position, effect_Pos.transform.rotation);
        Destroy(sniping_Effect,0.5f);
        GameObject sniping_Bullet = Instantiate(sniping_shoot_Bullet, attack_Pos.transform.position, attack_Pos.transform.rotation);
        Destroy(sniping_Bullet, 2.0f);
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("isSitShootB", false);
        animator.SetBool("isSitShootC", true);
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("isSitShootC", false);
        yield return new WaitForSeconds(0.8f);
        shoot_Sound.clip = changer.temp_SoundClip;
        playerMove.enabled = true;
    }


    IEnumerator OnSwordAttack()//스킬 E
    {
        playerMove.enabled = false;
        animator.SetTrigger("isSwordAttack");
        Rifle.gameObject.SetActive(false);
        Katana.gameObject.SetActive(true);
        Katana_Sub.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.0f);

        sword_Attack_Effect.gameObject.SetActive(true);
        sword_Attack_Sound.PlayOneShot(sword_Attack_SoundClip);
        animator.SetBool("isRun", false);

        yield return new WaitForSeconds(0.5f);
        Katana.gameObject.SetActive(false);
        sword_Attack_Effect.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.10f);
        Rifle.gameObject.SetActive(true);
        Katana_Sub.gameObject.SetActive(true);
        playerMove.enabled = true;
    }

    IEnumerator OnBattlModeChange()//스킬 R
    {
        if(Battle_Mode_Change_Effect != null)
        Battle_Mode_Change_Effect.Play();
        yield return null;
    }
}
