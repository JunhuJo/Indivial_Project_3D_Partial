using UnityEngine;

public class BossSkill : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public string targetTag = "Player"; // Ÿ�� ������Ʈ�� �±�
    public float lineLength = 5f;
    public float showDuration = 2f;
    private bool isShowingLine = false;
    private Transform target;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false; // ó���� ���� ����
        FindTarget();
    }

    void Update()
    {
        if (isShowingLine)
        {
            ShowSkillDirection();
        }
    }

    void FindTarget()
    {
        GameObject targetObject = GameObject.FindWithTag(targetTag);
        if (targetObject != null)
        {
            target = targetObject.transform;
        }
        else
        {
            Debug.LogWarning("Target with tag " + targetTag + " not found.");
        }
    }

    public void ShowSkillDirection()
    {
        if (target == null)
        {
            FindTarget();
            if (target == null) return; // Ÿ���� ������ ������ ����
        }

        lineRenderer.enabled = true;
        isShowingLine = true;

        Vector3 startPos = transform.position;
        Vector3 direction = (target.position - startPos).normalized;
        Vector3 endPos = startPos + direction * lineLength;

        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);

        // ���� �ð� �� �� �����
        //Invoke("HideSkillDirection", showDuration);
    }

    void HideSkillDirection()
    {
        lineRenderer.enabled = false;
        isShowingLine = false;
    }

    // �� �޼��带 ��ų ��� ���� ȣ��
    public void PrepareSkill()
    {
        ShowSkillDirection();
    }
}
