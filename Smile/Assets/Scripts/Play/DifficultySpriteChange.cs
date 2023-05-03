using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 난이도에 따른 몬스터 이미지 변경 스크립트

public class DifficultySpriteChange : MonoBehaviour
{
    [Header("몬스터 이미지 추가")] public Sprite[] Monster_Image;
    public GameObject monster;

    // Start is called before the first frame update
    void Start()
    {
        MonsterSpriteChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MonsterSpriteChange()
    {
        SpriteRenderer spriteRenderer = monster.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Monster_Image[UniteData.Difficulty - 1];
    }

    private void BackgroundChange()
    {
        
    }
}
