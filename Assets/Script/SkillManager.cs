using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Skill_Q")]
    [SerializeField] Text skill_Q_CoolTime_Text;
    [SerializeField] private GameObject darkness_Shoot_Bullet;
    [SerializeField] private GameObject darkness_Shoot_Effect;
    [SerializeField] private SkillCooldownUI qSkillCooldownUI; // Q ��ų ��Ÿ�� UI �߰�
    private AudioClip darkness_SoundClip;
    

    [Header("Skill_W")]
    [SerializeField] Text skill_W_CoolTime_Text;
    [SerializeField] private GameObject sniping_shoot_Bullet;
    [SerializeField] private GameObject sniping_shoot_Effect;
    [SerializeField] private SkillCooldownUI wSkillCooldownUI; // W ��ų ��Ÿ�� UI �߰�
    private AudioClip sniping_SoundClip;
    

    [Header("Skill_E")]
    [SerializeField] Text skill_E_CoolTime_Text;
    [SerializeField] private GameObject sword_Attack_Effect;
    [SerializeField] private AudioSource sword_Attack_Sound;
    [SerializeField] private SkillCooldownUI eSkillCooldownUI; // E ��ų ��Ÿ�� UI �߰�
    private AudioClip sword_Attack_SoundClip;
    [SerializeField] private GameObject rifle;
    [SerializeField] private GameObject katana;
    [SerializeField] private GameObject katana_Sub;
    

    [Header("Skill_R")]
    [SerializeField] Text skill_R_CoolTime_Text;
    [SerializeField] private ParticleSystem battle_Mode_Change_Effect_First;
    [SerializeField] private ParticleSystem battle_Mode_Change_Effect_Scound;
    [SerializeField] private SkillCooldownUI rSkillCooldownUI; // R ��ų ��Ÿ�� UI �߰�
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
    //[SerializeField] private Transform gun_shoot_Pos;


    [Header("BattleMode_Katana_SKill")]
    [SerializeField] private ParticleSystem skill_Q_Effect;
    [SerializeField] private ParticleSystem skill_W_Effect;
    [SerializeField] private ParticleSystem skill_E_Effect;
    [SerializeField] private ParticleSystem skill_R1_Effect;
    [SerializeField] private ParticleSystem skill_R2_Effect;
    

    //private Vector3 moveDirection;
    //private bool isDashing = false;
    //private float dashTimeLeft;
    //private float lastDashTime = -100f;
    //private CharacterController controller;


    [Header("Common")]
    
    [SerializeField] private AudioSource shoot_Sound;
    [SerializeField] private SoundChanger changer;
    [SerializeField] private Transform attack_Pos;
    [SerializeField] private Transform effect_Pos;
    private PlayerMove playerMove;
    public bool SetbattleMode = false;

    [SerializeField] private AudioSource battleModeSound;
    [SerializeField] private KatanaSoundChanger katanaSoundChanger;

    public float skill_Q_CooldownTime = 5f; // Q ��ų ��Ÿ�� �ð�
    public float skill_W_CooldownTime = 5f; // W ��ų ��Ÿ�� �ð�
    public float skill_E_CooldownTime = 5f; // E ��ų ��Ÿ�� �ð�
    public float skill_R_CooldownTime = 5f; // R ��ų ��Ÿ�� �ð�

    private bool set_Skill_Q_Cooldown = false;
    private bool set_Skill_W_Cooldown = false;
    private bool set_Skill_E_Cooldown = false;
    private bool set_Skill_R_Cooldown = false;

    private float skill_Q_CooldownTimer = 0f;
    private float skill_W_CooldownTimer = 0f;
    private float skill_E_CooldownTimer = 0f;
    private float skill_R_CooldownTimer = 0f;


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
        //controller = GetComponent<CharacterController>();
        skill_Voice = GetComponent<AudioSource>();
    }

    private void Update()
    {
        BattleStyle();
        UpdateCooldowns();
    }

    private void UpdateCooldowns()
    {
        if (set_Skill_Q_Cooldown)
        {
            skill_Q_CooldownTimer -= Time.deltaTime;
            if (skill_Q_CooldownTimer <= 0f)
            {
                set_Skill_Q_Cooldown = false;
                skill_Q_CooldownTimer = 0f;
            }
            //qSkillCooldownUI.cooldownOverlay.fillAmount = skill_Q_CooldownTimer / skill_Q_CooldownTime; // Q ��ų ��Ÿ�� UI ������Ʈ
        }

        if (set_Skill_W_Cooldown)
        {
            skill_W_CooldownTimer -= Time.deltaTime;
            if (skill_W_CooldownTimer <= 0f)
            {
                set_Skill_W_Cooldown = false;
                skill_W_CooldownTimer = 0f;
            }
            //wSkillCooldownUI.cooldownOverlay.fillAmount = skill_W_CooldownTimer / skill_W_CooldownTime; // W ��ų ��Ÿ�� UI ������Ʈ
        }

        if (set_Skill_E_Cooldown)
        {
            skill_E_CooldownTimer -= Time.deltaTime;
            if (skill_E_CooldownTimer <= 0f)
            {
                set_Skill_E_Cooldown = false;
                skill_E_CooldownTimer = 0f;
            }
            //eSkillCooldownUI.cooldownOverlay.fillAmount = skill_E_CooldownTimer / skill_E_CooldownTime; // E ��ų ��Ÿ�� UI ������Ʈ
        }

        if (set_Skill_R_Cooldown)
        {
            skill_R_CooldownTimer -= Time.deltaTime;
            if (skill_R_CooldownTimer <= 0f)
            {
                set_Skill_R_Cooldown = false;
                skill_R_CooldownTimer = 0f;
            }
            //rSkillCooldownUI.cooldownOverlay.fillAmount = skill_R_CooldownTimer / skill_R_CooldownTime; // R ��ų ��Ÿ�� UI ������Ʈ
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
            StartCoroutine(OnDarknessShoot());
            set_Skill_Q_Cooldown = true;
            skill_Q_CooldownTimer = skill_Q_CooldownTime;
            //qSkillCooldownUI.StartCooldown(); // Q ��ų
        }

        if (Input.GetKeyDown(KeyCode.W) && !set_Skill_W_Cooldown)
        {
            StartCoroutine(OnSitShoot());
            set_Skill_W_Cooldown = true;
            skill_W_CooldownTimer = skill_W_CooldownTime;
        }

        if (Input.GetKeyDown(KeyCode.E) && !set_Skill_E_Cooldown)
        {
            StartCoroutine(OnSwordAttack());
            set_Skill_E_Cooldown = true;
            skill_E_CooldownTimer = skill_E_CooldownTime;
        }

        if (Input.GetKeyDown(KeyCode.R) && !set_Skill_R_Cooldown)
        {
            StartCoroutine(Chain_Shoot());
            set_Skill_R_Cooldown = true;
            skill_R_CooldownTimer = skill_R_CooldownTime;
        }

        if (Input.GetKeyDown(KeyCode.F) && !set_Skill_R_Cooldown)
        {
            StartCoroutine(OnBattlModeChange());
            set_Skill_R_Cooldown = true;
            skill_R_CooldownTimer = skill_R_CooldownTime;
        }
    }

    #region BaseBattleMode
    IEnumerator OnDarknessShoot() // ��ų Q
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

    IEnumerator OnSitShoot()//��ų W
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


    IEnumerator OnSwordAttack()//��ų E
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

    IEnumerator Chain_Shoot()//��ų R
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
        //��ų Q(�� ��ų)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(KatanaPowerAttack());
        }

        //��ų W(�ߵ�)
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(KatanaCircleAttack());
        }
        
        //��ų E(�뽬)
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(KatanaDash());  
        }
        

        //��ų R(������)
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(KatanaAwakeAttack());
        }

        //��ų F ��������(�ӽ�)
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(AwakClose());
        }
    }

    #region AwakeMode
    IEnumerator KatanaPowerAttack()// ���� Q��ų
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

    IEnumerator KatanaCircleAttack()// ���� W��ų
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

    IEnumerator KatanaDash()// ���� E��ų
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

    IEnumerator KatanaAwakeAttack()// ���� R��ų(������)
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


    IEnumerator AwakClose()// ���� ����
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
