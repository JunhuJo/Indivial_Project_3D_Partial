using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownUI : MonoBehaviour
{
    public Image cooldownOverlay; // 쿨타임 오버레이 이미지
    public float cooldownTime; // 쿨타임 시간

    private float cooldownTimer;
    private bool isCooldown;

    void Start()
    {
        cooldownOverlay.fillAmount = 0f; // 초기 상태: 쿨타임 없음
    }

    void Update()
    {
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                cooldownTimer = 0f;
                isCooldown = false;
            }
            cooldownOverlay.fillAmount = cooldownTimer / cooldownTime;
        }
    }

    public void StartCooldown()
    {
        isCooldown = true;
        cooldownTimer = cooldownTime;
        cooldownOverlay.fillAmount = 1f;
    }
}
