using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManageMent : MonoBehaviour
{
    [Header("Cursor Settings")]
    [SerializeField] private Texture2D customCursorTexture; // Ŀ�� �̹���
    [SerializeField] private Vector2 hotSpot = Vector2.zero; // Ŀ�� �ֽ��� (�̹����� �߽��� �������� Ŀ�� ��ġ�� ����)

    [Header("CharacterCreate")]
    [SerializeField] private CinemachineVirtualCamera virtual_Camera;
    [SerializeField] private GameObject Player_Prefap;

    private void Start()
    {
        ChangeCursor(customCursorTexture, hotSpot);
        GameObject Player = Instantiate(Player_Prefap);
        virtual_Camera.Follow = Player.transform;
    }

    private void ChangeCursor(Texture2D cursorTexture, Vector2 hotSpot)
    {
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    private void OnDisable()
    {
        // ��ũ��Ʈ�� ��Ȱ��ȭ�� �� �⺻ Ŀ���� ����
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
