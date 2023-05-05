using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatMonster : MonoBehaviour
{
    public GameObject[] monsters;
    static private GameObject monster;

    public Sprite[] monster_images;
    static private SpriteRenderer monster_image;

    private string[] monster_name = { "Rose", "Cosmos", "MorningGlory" };

    static public int monsterCount; // 몬스터 등장 횟수 (난이도에 따라 설정)

    public GameClear s_gameclear;

    // Start is called before the first frame update
    void Start()
    {
        if (UniteData.ReStart)
            Initialized();

        switch (UniteData.Difficulty)
        {
            case 1:
                monster = monsters[0];
                break;

            case 2:
                monster = monsters[1];
                break;

            case 3:
                //monster = monsters[2];
                break;

            default:
                monster = monsters[0];
                break;
        }

        monster_image = monster.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialized()
    {
        Debug.Log("몬스터 마리수 초기화");
        switch (UniteData.Difficulty)
        {
            case 1 :
                monsterCount = 6;
                break;

            case 2 :
                monsterCount = 7;
                break;

            case 3 :
                monsterCount = 10;
                break;

            default : 
                monsterCount = 6;
                break;
        }
        UniteData.ReStart = false;
    }

    public void MonsterColorOrigin()
    {
        Debug.Log("monsterCount" + monsterCount);
        Debug.Log("MonsterColorOrigin");
        monster.SetActive(true);

        // 몬스터 랜덤 배치
        int ran_mon = Random.Range(0, 2);

        Debug.Log("ran_mon : " + ran_mon);
        if(monsterCount == 2 && UniteData.Difficulty == 2)
        {
            ran_mon = 1;
        }

        UniteData.Closed_Monster = monster_name[ran_mon];
        monster_image.sprite = monster_images[ran_mon];
        Debug.Log("monster_image" + monster_image.sprite);

        // 줄여놨던 a값 다시 원상 복귀
        Color c = monster.GetComponent<SpriteRenderer>().color;
        c.a = 225f;
        monster.GetComponent<SpriteRenderer>().color = c;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NoteManager"))
        {
            if (monsterCount > 1)
            {
                monsterCount--;
                MonsterColorOrigin();
            }
            else if (monsterCount == 1)
            {
                Debug.Log("Game Clear");
                s_gameclear.ClearGame();
            }
        }
    }

}
