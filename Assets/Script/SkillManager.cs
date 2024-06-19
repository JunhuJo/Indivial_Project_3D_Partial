using System.Collections;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Skill_Q")]
    [SerializeField] private GameObject darkness_Shoot_Bullet;
    [SerializeField] private GameObject darkness_Shoot_Effect;
    private AudioClip darkness_SoundClip;

    [Header("Skill_W")]
    [SerializeField] private GameObject sniping_shoot_Bullet;
    [SerializeField] private GameObject sniping_shoot_Effect;
    private AudioClip sniping_SoundClip;
    
    [Header("Skill_E")]
    [SerializeField] private GameObject sword_Attack_Effect;
    [SerializeField] private AudioSource sword_Attack_Sound;
    private AudioClip sword_Attack_SoundClip;
    [SerializeField] private GameObject rifle;
    [SerializeField] private GameObject katana;
    [SerializeField] private GameObject katana_Sub;

    [Header("Skill_R")]
    [SerializeField] private ParticleSystem battle_Mode_Change_Effect_First;
    [SerializeField] private ParticleSystem battle_Mode_Change_Effect_Scound;
    [SerializeField] private GameObject battle_Mode_Trigger;
    private Animator battle_Mode_Animator;
    [SerializeField] private RuntimeAnimatorController battle_Mode;
    [SerializeField] private GameObject battle_Mode_Weapon_Start;
    [SerializeField] private GameObject battle_Mode_Weapon_Saya;

    [SerializeField] private GameObject battle_Mode_Weapon_Attack;
    

    [Header("Common")]
    [SerializeField] private AudioSource shoot_Sound;
    [SerializeField] private RifleSoundChanger changer;
    [SerializeField] private Transform attack_Pos;
    [SerializeField] private Transform effect_Pos;
    private PlayerMove playerMove;
    private bool SetbattleMode = false;



    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        sword_Attack_SoundClip = sword_Attack_Sound.clip;
        battle_Mode_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        BattleStyle();
    }

    private void BattleStyle()
    {
        if (!SetbattleMode)
        {
            BaseBattleModeSkill();
        }
        else if (SetbattleMode)
        {
            AwakeBattleModeSkill();
        }

    }


    private void BaseBattleModeSkill()
    {
        //스킬 Q(라이플스킬)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(OnDarknessShoot());
        }

        //스킬 W(라이플스킬)
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(OnSitShoot());
        }

        //스킬 E(소드스킬)
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(OnSwordAttack());
        }

        //스킬 R(배틀모드 변환)
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(OnBattlModeChange());
        }
    }

    #region BaseBattleMode
    IEnumerator OnDarknessShoot() // 스킬 Q
    {
        playerMove.enabled = false;
        animator.SetTrigger("isDarkness");
        yield return new WaitForSeconds(0.8f);
        changer.temp_SoundClip = shoot_Sound.clip;
        shoot_Sound.clip = changer.darkness_Shoot_SoundClip;
        shoot_Sound.PlayOneShot(shoot_Sound.clip);
        GameObject darknessShootBulletEffect = Instantiate(darkness_Shoot_Effect, effect_Pos.transform.position, effect_Pos.transform.rotation);
        Destroy(darknessShootBulletEffect, 0.5f);

        GameObject darknessShootBullet = Instantiate(darkness_Shoot_Bullet, attack_Pos.transform.position, attack_Pos.transform.rotation);
        Destroy(darknessShootBullet,2.0f);
        shoot_Sound.clip = changer.temp_SoundClip;
        yield return null;
        playerMove.enabled = true;
    }


    IEnumerator OnSitShoot()//스킬 W
    {
        playerMove.enabled = false;
        changer.temp_SoundClip = shoot_Sound.clip;
        shoot_Sound.clip = changer.sniping_SoundClip_First;
        animator.SetBool("isSitShootA", true);
        shoot_Sound.PlayOneShot(shoot_Sound.clip);
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
        rifle.gameObject.SetActive(false);
        katana.gameObject.SetActive(true);
        katana_Sub.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.0f);

        sword_Attack_Effect.gameObject.SetActive(true);
        sword_Attack_Sound.PlayOneShot(sword_Attack_SoundClip);
        animator.SetBool("isRun", false);
        yield return new WaitForSeconds(0.5f);

        katana.gameObject.SetActive(false);
        sword_Attack_Effect.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.10f);

        rifle.gameObject.SetActive(true);
        katana_Sub.gameObject.SetActive(true);
        playerMove.enabled = true;
    }

    IEnumerator OnBattlModeChange()//스킬 R
    {
        playerMove.enabled = false;
        rifle.gameObject.SetActive(false);
        katana_Sub.gameObject.SetActive(false);
        battle_Mode_Trigger.gameObject.SetActive(true);
        
        animator.SetTrigger("isBattleModeChange");
        yield return new WaitForSeconds(1.5f);

        battle_Mode_Change_Effect_First.Play();
        battle_Mode_Change_Effect_Scound.Play();
        yield return new WaitForSeconds(4.0f);

        battle_Mode_Trigger.gameObject.SetActive(false);
        battle_Mode_Animator.runtimeAnimatorController = battle_Mode;

        battle_Mode_Weapon_Start.SetActive(true);
        battle_Mode_Weapon_Saya.SetActive(true);
        //rifle.gameObject.SetActive(true);
        //katana_Sub.gameObject.SetActive(true);

        yield return null;
        SetbattleMode = true;
        playerMove.enabled = true;
    }
    #endregion


    private void AwakeBattleModeSkill()
    {
        //스킬 Q(검 스킬)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(KatanaPowerAttack());
        }

        //스킬 W(발도)
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(KatanaCircleAttack());
        }
        
        //스킬 E(대쉬)
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(KatanaDash());  
        }
        

        //스킬 R(각성기)
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(KatanaAwakeAttack());
        }

    }

    IEnumerator KatanaPowerAttack()// 각성 Q스킬
    {
        animator.SetTrigger("isCountAttack");
        yield return new WaitForSeconds(0.6f);
        battle_Mode_Weapon_Start.SetActive(false);
        battle_Mode_Weapon_Attack.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        battle_Mode_Weapon_Start.SetActive(true);
        battle_Mode_Weapon_Attack.SetActive(false);
    }

    IEnumerator KatanaCircleAttack()// 각성 W스킬
    {
        animator.SetTrigger("isCountAttackTwo");
        yield return new WaitForSeconds(0.2f);
        battle_Mode_Weapon_Start.SetActive(false);
        battle_Mode_Weapon_Attack.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        battle_Mode_Weapon_Start.SetActive(true);
        battle_Mode_Weapon_Attack.SetActive(false);
    }


    IEnumerator KatanaDash()// 각성 E스킬
    {
        animator.SetTrigger("isSmash");
        
        battle_Mode_Weapon_Start.SetActive(false);
        battle_Mode_Weapon_Attack.SetActive(true);
        yield return new WaitForSeconds(2.5f);  

        battle_Mode_Weapon_Start.SetActive(true);
        battle_Mode_Weapon_Attack.SetActive(false);
    }


    IEnumerator KatanaAwakeAttack()// 각성 R스킬(각성기)
    {
        animator.SetTrigger("isAwake");
        yield return new WaitForSeconds(1.2f);
        battle_Mode_Weapon_Start.SetActive(false);
        battle_Mode_Weapon_Attack.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        battle_Mode_Weapon_Start.SetActive(true);
        battle_Mode_Weapon_Attack.SetActive(false);
    }
}
