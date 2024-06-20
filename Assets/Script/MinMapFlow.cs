using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMapFlow : MonoBehaviour
{
    public Transform player;
    //public RectTransform icon; // ������ RectTransform
    //public RectTransform minimapRect; // �̴ϸ� RectTransform

    //private void Update()
    //{
    //    // �÷��̾��� ���� ��ġ�� �̴ϸ� ��ǥ�� ��ȯ
    //    Vector2 playerPositionOnMinimap = WorldToMinimapPosition(player.position);
    //
    //    // ������ ��ġ ������Ʈ
    //    icon.anchoredPosition = playerPositionOnMinimap;
    //}

    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y; // ī�޶��� ���̴� ����
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f); // �÷��̾��� ������ ���� ȸ��
    }

    //private Vector2 WorldToMinimapPosition(Vector3 worldPosition)
    //{
    //    // ���� ��ǥ�� �̴ϸ� ��ǥ�� ��ȯ
    //    // �̴ϸ� ũ��� ���� ��ǥ ũ�⿡ �°� �����ؾ� �մϴ�.
    //    // ���⼭�� �ܼ��� �����̹Ƿ� ������ �̸� ���صΰ� ��ȯ�մϴ�.
    //
    //    float mapWidth = minimapRect.rect.width;
    //    float mapHeight = minimapRect.rect.height;
    //
    //    float mapScaleX = mapWidth / 100f; // ���� ũ�� 100�� ���� ���� ����
    //    float mapScaleY = mapHeight / 100f;
    //
    //    float x = (worldPosition.x * mapScaleX) - (mapWidth / 2);
    //    float y = (worldPosition.z * mapScaleY) - (mapHeight / 2); // y ��� z ���
    //
    //    return new Vector2(x, y);
    //}
}
