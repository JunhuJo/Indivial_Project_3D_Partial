using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMapFlow : MonoBehaviour
{
    public Transform player;
    //public RectTransform icon; // 아이콘 RectTransform
    //public RectTransform minimapRect; // 미니맵 RectTransform

    //private void Update()
    //{
    //    // 플레이어의 월드 위치를 미니맵 좌표로 변환
    //    Vector2 playerPositionOnMinimap = WorldToMinimapPosition(player.position);
    //
    //    // 아이콘 위치 업데이트
    //    icon.anchoredPosition = playerPositionOnMinimap;
    //}

    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y; // 카메라의 높이는 고정
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f); // 플레이어의 방향을 따라 회전
    }

    //private Vector2 WorldToMinimapPosition(Vector3 worldPosition)
    //{
    //    // 월드 좌표를 미니맵 좌표로 변환
    //    // 미니맵 크기와 월드 좌표 크기에 맞게 조정해야 합니다.
    //    // 여기서는 단순히 예제이므로 비율을 미리 정해두고 변환합니다.
    //
    //    float mapWidth = minimapRect.rect.width;
    //    float mapHeight = minimapRect.rect.height;
    //
    //    float mapScaleX = mapWidth / 100f; // 월드 크기 100에 맞춘 비율 예제
    //    float mapScaleY = mapHeight / 100f;
    //
    //    float x = (worldPosition.x * mapScaleX) - (mapWidth / 2);
    //    float y = (worldPosition.z * mapScaleY) - (mapHeight / 2); // y 대신 z 사용
    //
    //    return new Vector2(x, y);
    //}
}
