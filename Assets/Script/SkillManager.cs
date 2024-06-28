using Cinemachine.Utility;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Skill_Q")]
    [SerializeField] Text skill_Q_CoolTime_Text;
    [SerializeField] private GameObject darkness_Shoot_Bullet;
    [SerializeField] private GameObject darkness_Shoot_Effect;
    [SerializeField] private SkillCooldownUI qSkillCooldownUI; // Q 스킬 쿨타임 UI 추가
    private AudioClip darkness_SoundClip;
    

    [Header("Skill_W")]
    [SerializeField] Text skill_W_CoolTime_Text;
    [SerializeField] private GameObject sniping_shoot_Bullet;
    [SerializeField] private GameObject sniping_shoot_Effect;
    [SerializeField] private SkillCooldownUI wSkillCooldownUI; // W 스킬 쿨타임 UI 추가
    private AudioClip sniping_SoundClip;
    

    [Header("Skill_E")]
    [SerializeField] Text skill_E_CoolTime_Text;
    [SerializeField] private GameObject sword_Attack_Effect;
    [SerializeField] private AudioSource sword_Attack_Sound;
    [SerializeField] private SkillCooldownUI eSkillCooldownUI; // E 스킬 쿨타임 UI 추가
    private AudioClip sword_Attack_SoundClip;
    [SerializeField] private GameObject rifle;
    [SerializeField] private GameObject katana;
    [SerializeField] private GameObject katana_Sub;
    

    [Header("Skill_R")]
    [SerializeField] Text skill_R_CoolTime_Text;
    [SerializeField] private ParticleSystem battle_Mode_Change_Effect_First;
    [SerializeField] private ParticleSystem battle_Mode_Change_Effect_Scound;
    [SerializeField] private SkillCooldownUI rSkillCooldownUI; // R 스킬 쿨타임 UI 추가
    [SerializeField] private GameObject battle_Mode_Trigger;
    private Animator battle_Mode_Animator;
    
    [SerializeField] private RuntimeAnimatorController battle_Mode;
    [SerializeField] private RuntimeAnimatorController base_Mode;
    public GameObject battle_Mode_Weapon_Start;
    [SerializeField] private GameObject battle_Mode_Weapon_Saya;
    public GameObject battle_Mode_Weapon_Attack;

    [Header("Skill_R_New")]
    [SerializeField] private ParticleSystem skill_R_New_Effect_First;
    [SerializeField] private AudioClip gun_SoundClip;
    [SerializeField] private GameObject gun;
   
    [Header("BattleMode_Katana_SKill")]
    [SerializeField] private ParticleSystem skill_Q_Effect;
    [SerializeField] private ParticleSystem skill_W_Effect;
    [SerializeField] private ParticleSystem skill_E_Effect;
    [SerializeField] private ParticleSystem skill_R1_Effect;
    [SerializeField] private ParticleSystem skill_R2_Effect;
    
    [Header("Common")]
    [SerializeField] private AudioSource shoot_Sound;
    [SerializeField] private SoundChanger changer;
    [SerializeField] private Transform attack_Pos;
    [SerializeField] private Transform effect_Pos;
    private PlayerMove playerMove;
    private NavMeshAgent player_nav;
    public bool SetbattleMode = false;

    [SerializeField] private AudioSource battleModeSound;
    [SerializeField] private KatanaSoundChanger katanaSoundChanger;

    public float skill_Q_CooldownTime = 3.5f; // Q 스킬 쿨타임 시간
    public float skill_W_CooldownTime = 5f; // W 스킬 쿨타임 시간
    public float skill_E_CooldownTime = 2f; // E 스킬 쿨타임 시간
    public float skill_R_CooldownTime = 2f; // R 스킬 쿨타임 시간

    private bool set_Skill_Q_Cooldown = false;
    private bool set_Skill_W_Cooldown = false;
    private bool set_Skill_E_Cooldown = false;
    private bool set_Skill_R_Cooldown = false;

    private float skill_Q_CooldownTimer = 0f;
    private float skill_W_CooldownTimer = 0f;
    private float skill_E_CooldownTimer = 0f;
    private float skill_R_CooldownTimer = 0f;

    public int coolDown_Q;
    public int coolDown_W;
    public int coolDown_E;
    public int coolDown_R;

    private AudioSource skill_Voice;
    [SerializeField] private AudioClip darkNess_Shoot_Voice;
    [SerializeField] private AudioClip sit_Shoot_Voice;
    [SerializeField] private AudioClip sword_Attack_Voice;
    [SerializeField] private AudioClip chain_Shoot_Voice;
    [SerializeField] private AudioClip awake_Change_Voice;

    [SerializeField] private AudioClip count_Attack_Voice;
    [SerializeField] private AudioClip count_Attack_Two_Voice;
    [SerializeField] private AudioClip air_Attack_Voice;
    [SerializeField] private AudioClip awake_Skill_Voice;
    [SerializeField] private AudioClip awake_Close_Voice;


    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        sword_Attack_SoundClip = sword_Attack_Sound.clip;
       
        battle_Mode_Animator = GetComponent<Animator>();
        skill_Voice = GetComponent<AudioSource>();
        player_nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        BattleStyle();
        UpdateCooldowns();
    }

    public void UseSkillTrun()
    {
        // 캐릭터 회전
        RotateTowardsMousePointer();

        // 이동 중지 및 경로 초기화
        player_nav.isStopped = true;
        player_nav.ResetPath();

        // 1.5초 후 이동 재개
        Invoke("ResumeMovement", 1.5f);
    }

    private void RotateTowardsMousePointer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            Vector3 direction = (hit.point - transform.position).normalized;
            direction.y = 0; // 높이 차이는 무시
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360); // 즉시 회전
        }
    }

    private void ResumeMovement()
    {
        player_nav.isStopped = false;
    }

    private void UpdateCooldowns()
    {
        if (set_Skill_Q_Cooldown)
        {
            skill_Q_CooldownTimer -= Time.deltaTime;
            coolDown_Q = Mathf.CeilToInt(skill_Q_CooldownTimer);
            if (skill_Q_CooldownTimer <= 0f)
            {
                set_Skill_Q_Cooldown = false;
                skill_Q_CooldownTimer = 0f;
            }
        }

        if (set_Skill_W_Cooldown)
        {
            skill_W_CooldownTimer -= Time.deltaTime;
            coolDown_W = Mathf.CeilToInt(skill_W_CooldownTimer);
            if (skill_W_CooldownTimer <= 0f)
            {
                set_Skill_W_Cooldown = false;
                skill_W_CooldownTimer = 0f;
            }
            //wSkillCooldownUI.cooldownOverlay.fillAmount = skill_W_CooldownTimer / skill_W_CooldownTime; // W 스킬 쿨타임 UI 업데이트
        }

        if (set_Skill_E_Cooldown)
        {
            skill_E_CooldownTimer -= Time.deltaTime;
            coolDown_E = Mathf.CeilToInt(skill_E_CooldownTimer);
            
            if (skill_E_CooldownTimer <= 0f)
            {
                set_Skill_E_Cooldown = false;
                skill_E_CooldownTimer = 0f;
            }
            //eSkillCooldownUI.cooldownOverlay.fillAmount = skill_E_CooldownTimer / skill_E_CooldownTime; // E 스킬 쿨타임 UI 업데이트
        }

        if (set_Skill_R_Cooldown)
        {
            skill_R_CooldownTimer -= Time.deltaTime;
            coolDown_R = Mathf.CeilToInt(skill_R_CooldownTimer);
            if (skill_R_CooldownTimer <= 0f)
            {
                set_Skill_R_Cooldown = false;
                skill_R_CooldownTimer = 0f;
            }
            //rSkillCooldownUI.cooldownOverlay.fillAmount = skill_R_CooldownTimer / skill_R_CooldownTime; // R 스킬 쿨타임 UI 업데이트
        }
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
        if (Input.GetKeyDown(KeyCode.Q) && !set_Skill_Q_Cooldown)
        {
            UseSkillTrun();
            
            StartCoroutine(OnDarknessShoot());
            set_Skill_Q_Cooldown = true;
            skill_Q_CooldownTimer = skill_Q_CooldownTime;
            //qSkillCooldownUI.StartCooldown(); // Q 스킬
        }

        if (Input.GetKeyDown(KeyCode.W) && !set_Skill_W_Cooldown)
        {
            UseSkillTrun();

            StartCoroutine(OnSitShoot());
            set_Skill_W_Cooldown = true;
            skill_W_CooldownTimer = skill_W_CooldownTime;
        }

        if (Input.GetKeyDown(KeyCode.E) && !set_Skill_E_Cooldown)
        {
            UseSkillTrun();

            StartCoroutine(OnSwordAttack());
            set_Skill_E_Cooldown = true;
            skill_E_CooldownTimer = skill_E_CooldownTime;
        }

        if (Input.GetKeyDown(KeyCode.R) && !set_Skill_R_Cooldown)
        {
            UseSkillTrun();

            StartCoroutine(Chain_Shoot());
            set_Skill_R_Cooldown = true;
            skill_R_CooldownTimer = skill_R_CooldownTime;
        }

        if (Input.GetKeyDown(KeyCode.G) && !set_Skill_R_Cooldown)
        {
            StartCoroutine(OnBattlModeChange());
            set_Skill_R_Cooldown = true;
            skill_R_CooldownTimer = skill_R_CooldownTime;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            transform.position = new Vector3(-13.042224884033204f, -2.2649998664855959f, 129.14707946777345f);
        }
    }

    #region BaseBattleMode
    IEnumerator OnDarknessShoot() // 스킬 Q
    {
        

        skill_Voice.clip = darkNess_Shoot_Voice;
        playerMove.enabled = false;
        skill_Voice.Play();
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
        

        skill_Voice.clip = sit_Shoot_Voice;
        playerMove.enabled = false;
        skill_Voice.Play();
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
        
        skill_Voice.clip = sword_Attack_Voice;
        playerMove.enabled = false;
        skill_Voice.Play();
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

    IEnumerator Chain_Shoot()//스킬 R
    {
        shoot_Sound.clip = gun_SoundClip;
        skill_Voice.clip = chain_Shoot_Voice;
        playerMove.enabled = false;
        skill_Voice.Play();
        gun.SetActive(true);
        animator.SetTrigger("isChainShoot");
        
        yield return new WaitForSeconds(0.5f);
        shoot_Sound.PlayOneShot(shoot_Sound.clip);
        skill_R_New_Effect_First.Play();
        yield return new WaitForSeconds(1.4f);
        gun.SetActive(false);
        playerMove.enabled = true;
    }


    IEnumerator OnBattlModeChange()
    {
        battleModeSound.clip = katanaSoundChanger.battleMode_Change;
        skill_Voice.clip = awake_Change_Voice;
        skill_Voice.Play();
        playerMove.enabled = false;
        rifle.gameObject.SetActive(false);
        katana_Sub.gameObject.SetActive(false);
        battle_Mode_Trigger.gameObject.SetActive(true);
        
        animator.SetTrigger("isBattleModeChange");
        yield return new WaitForSeconds(1.5f);
        battleModeSound.PlayOneShot(battleModeSound.clip);
        battle_Mode_Change_Effect_First.Play();
        battle_Mode_Change_Effect_Scound.Play();
        yield return new WaitForSeconds(4.0f);

        battle_Mode_Trigger.gameObject.SetActive(false);
        battle_Mode_Animator.runtimeAnimatorController = battle_Mode;

        battle_Mode_Weapon_Start.SetActive(true);
        battle_Mode_Weapon_Saya.SetActive(true);
       
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

        //스킬 F 각성해제(임시)
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(AwakClose());
        }
    }

    #region AwakeMode
    IEnumerator KatanaPowerAttack()// 각성 Q스킬
    {
        skill_Voice.clip = count_Attack_Voice;
        battleModeSound.clip = katanaSoundChanger.battleMode_skill_Q;
        animator.SetTrigger("isCountAttack");
        skill_Voice.Play();
        battleModeSound.PlayOneShot(battleModeSound.clip);
        yield return new WaitForSeconds(0.6f);
        battle_Mode_Weapon_Start.SetActive(false);
        battle_Mode_Weapon_Attack.SetActive(true);
        skill_Q_Effect.Play();
        yield return new WaitForSeconds(0.5f);
        skill_Q_Effect.Stop();
        battle_Mode_Weapon_Start.SetActive(true);
        battle_Mode_Weapon_Attack.SetActive(false);
    }

    IEnumerator KatanaCircleAttack()// 각성 W스킬
    {
        skill_Voice.clip = count_Attack_Two_Voice;
        battleModeSound.clip = katanaSoundChanger.battleMode_skill_W;
        animator.SetTrigger("isCountAttackTwo");
        skill_Voice.Play();
        yield return new WaitForSeconds(0.2f);
        battleModeSound.PlayOneShot(battleModeSound.clip);
        battle_Mode_Weapon_Start.SetActive(false);
        battle_Mode_Weapon_Attack.SetActive(true);
        skill_W_Effect.Play();
        
        yield return new WaitForSeconds(1.0f);
        skill_W_Effect.Stop();
        battle_Mode_Weapon_Start.SetActive(true);
        battle_Mode_Weapon_Attack.SetActive(false);
    }

    IEnumerator KatanaDash()// 각성 E스킬
    {
        skill_Voice.clip = air_Attack_Voice;
        battleModeSound.clip = katanaSoundChanger.battleMode_skill_E;
        battleModeSound.PlayOneShot(battleModeSound.clip);
        skill_Voice.Play();
        animator.SetTrigger("isSmash");
        battle_Mode_Weapon_Start.SetActive(false);
        battle_Mode_Weapon_Attack.SetActive(true);
        battleModeSound.clip = katanaSoundChanger.battleMode_skill_E_Sub;
        battleModeSound.PlayOneShot(battleModeSound.clip);
        yield return new WaitForSeconds(0.5f);
        skill_E_Effect.Play();
        yield return new WaitForSeconds(0.5f);
        skill_E_Effect.Stop();
        yield return new WaitForSeconds(1.0f);
        battle_Mode_Weapon_Start.SetActive(true);
        battle_Mode_Weapon_Attack.SetActive(false);
    }

    IEnumerator KatanaAwakeAttack()// 각성 R스킬(각성기)
    {
        skill_Voice.clip = awake_Skill_Voice;
        battleModeSound.clip = katanaSoundChanger.battleMode_skill_R;
        animator.SetTrigger("isAwake");
        skill_Voice.Play();
        yield return new WaitForSeconds(1.2f);
        battle_Mode_Weapon_Start.SetActive(false);
        battle_Mode_Weapon_Attack.SetActive(true);
        battleModeSound.PlayOneShot(battleModeSound.clip);
        skill_R1_Effect.Play();
        skill_R2_Effect.Play();
        yield return new WaitForSeconds(0.5f);
        skill_R1_Effect.Play();
        skill_R2_Effect.Play();
        yield return new WaitForSeconds(1.0f);
        skill_R1_Effect.Stop();
        skill_R2_Effect.Stop();
        yield return new WaitForSeconds(0.3f);
        battle_Mode_Weapon_Start.SetActive(true);
        battle_Mode_Weapon_Attack.SetActive(false);
    }


    IEnumerator AwakClose()// 각성 해제
    {
        //battleModeSound.clip = katanaSoundChanger.battleMode_Change;
        skill_Voice.clip = awake_Close_Voice;
        playerMove.enabled = false;
        animator.SetTrigger("isModeEnd");
        skill_Voice.Play();
        yield return new WaitForSeconds(2.0f);
        battle_Mode_Change_Effect_First.Stop();
        //battle_Mode_Trigger.gameObject.SetActive(false);
        rifle.gameObject.SetActive(true);
        katana_Sub.gameObject.SetActive(true);

        battle_Mode_Weapon_Start.SetActive(false);
        battle_Mode_Weapon_Saya.SetActive(false);
        battle_Mode_Animator.runtimeAnimatorController = base_Mode;
        //battleModeSound.PlayOneShot(battleModeSound.clip);
        //battle_Mode_Change_Effect_First.Play();
        //battle_Mode_Change_Effect_Scound.Play();
        yield return new WaitForSeconds(0.5f);
        //battle_Mode_Trigger.gameObject.SetActive(true);
        
        SetbattleMode = false;
        playerMove.enabled = true;
    }
    #endregion
}
