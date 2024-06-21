using UnityEngine;

public class MinMapFlow : MonoBehaviour
{
    public Transform player;
    
    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y; // 카메라의 높이는 고정
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f); // 플레이어의 방향을 따라 회전
    }

}
