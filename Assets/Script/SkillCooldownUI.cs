using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownUI : MonoBehaviour
{
    public Image cooldownOverlay; // ��Ÿ�� �������� �̹���
    public float cooldownTime; // ��Ÿ�� �ð�

    private float cooldownTimer;
    private bool isCooldown;

    void Start()
    {
        cooldownOverlay.fillAmount = 0f; // �ʱ� ����: ��Ÿ�� ����
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
