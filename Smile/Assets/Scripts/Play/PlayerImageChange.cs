using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PlayerImageChange : MonoBehaviour
{
    public GameObject[] playerPrefab;
    static public GameObject player;

    [Header("������ ĳ���� �̹��� �� ������Ʈ")] public GameObject changePlayer;
    [Header("������ ĳ���� �̹���")] public Sprite[] changePlayerImg;

    private SpriteRenderer[] spriteRenderers;
    private SpriteRenderer sr_changePlayer;

    private void Awake()
    {
        //ĳ���� ����
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
            Debug.LogError("GameClear.cs ���Ͽ��� ĳ���� ���� ������ �߻��߽��ϴ� \n �Ƹ��� UniteData.Selected_Character ���� �����̰ų� �ش� ��ũ��Ʈ�� ���ǹ����� ������ �����ϼ���.");
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
        // ��Ʈ ����������
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

        yield return new WaitForSeconds(0.3f); // ������� �ð�

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