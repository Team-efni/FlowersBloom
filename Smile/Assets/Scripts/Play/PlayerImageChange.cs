using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PlayerImageChange : MonoBehaviour
{
    public GameObject[] playerPrefab;
    static public GameObject player;

    [Header("변경할 캐릭터 이미지 들어갈 오브젝트")] public GameObject changePlayer;
    [Header("변경할 캐릭터 이미지")] public Sprite[] changePlayerImg;

    private SpriteRenderer[] spriteRenderers;
    private SpriteRenderer sr_changePlayer;

    private void Awake()
    {
        //캐릭터 지정
        if (UniteData.Selected_Character == "Dandelion")
        {
            player = playerPrefab[0];
        }
        else if (UniteData.Selected_Character == "Tulip")
        {
            player = playerPrefab[1];
        }
        else
        {
            Debug.LogError("GameClear.cs 파일에서 캐릭터 선택 오류가 발생했습니다 \n 아마도 UniteData.Selected_Character 지정 문제이거나 해당 스크립트의 조건문에서 오류를 수정하세요.");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderers = player.GetComponentsInChildren<SpriteRenderer>()
            .Where(spriteRenderer => spriteRenderer.CompareTag("PlayerSprite"))
            .ToArray();
        sr_changePlayer = changePlayer.GetComponent<SpriteRenderer>();

        Color color_changePlayer = sr_changePlayer.color;
        color_changePlayer.a = 0f;
        sr_changePlayer.color = color_changePlayer;
    }

    // Update is called once per frame
    void Update()
    {
        // 노트 성공했으면
        if (UniteData.oneNoteSuccess)
        {
            StartCoroutine(CharacterImageChange());
        }
    }

    IEnumerator CharacterImageChange()
    {
        UniteData.oneNoteSuccess = false;

        Color color_changePlayer = sr_changePlayer.color;
        color_changePlayer.a = 255f;
        sr_changePlayer.color = color_changePlayer;

        sr_changePlayer.sprite = changePlayerImg[UniteData.noteIndex];

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            Color color = spriteRenderer.color;
            color.a = 0f;
            spriteRenderer.color = color;
        }

        yield return new WaitForSeconds(0.3f); // 사라지는 시간

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            Color color = spriteRenderer.color;
            color.a = 255f;
            spriteRenderer.color = color;
        }

        color_changePlayer.a = 0f;
        sr_changePlayer.color = color_changePlayer;
    }
}
