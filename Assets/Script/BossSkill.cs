using UnityEngine;

public class BossSkill : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public string targetTag = "Player"; // 타겟 오브젝트의 태그
    public float lineLength = 5f;
    public float showDuration = 2f;
    private bool isShowingLine = false;
    private Transform target;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false; // 처음엔 선을 숨김
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
            if (target == null) return; // 타겟이 여전히 없으면 종료
        }

        lineRenderer.enabled = true;
        isShowingLine = true;

        Vector3 startPos = transform.position;
        Vector3 direction = (target.position - startPos).normalized;
        Vector3 endPos = startPos + direction * lineLength;

        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);

        // 일정 시간 후 선 숨기기
        //Invoke("HideSkillDirection", showDuration);
    }

    void HideSkillDirection()
    {
        lineRenderer.enabled = false;
        isShowingLine = false;
    }

    // 이 메서드를 스킬 사용 전에 호출
    public void PrepareSkill()
    {
        ShowSkillDirection();
    }
}
