using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManageMent : MonoBehaviour
{
    [Header("Cursor Settings")]
    [SerializeField] private Texture2D customCursorTexture; // 커서 이미지
    [SerializeField] private Vector2 hotSpot = Vector2.zero; // 커서 핫스팟 (이미지의 중심을 기준으로 커서 위치를 설정)

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
        // 스크립트가 비활성화될 때 기본 커서로 복원
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
