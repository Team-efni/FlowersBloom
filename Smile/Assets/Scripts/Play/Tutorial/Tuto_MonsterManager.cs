using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Tuto_MonsterManager : MonoBehaviour
{
    public GameObject monster;

    public Sprite[] monster_images;
    static private SpriteRenderer monster_image;

    private string[] monster_name = { "Rose", "Bose" };

    static public int monsterCount;
    static private int monsterOriginCount; // 몬스터 등장 횟수 (난이도에 따라 설정)

    public Tuto_GameManager s_gameclear;

    public Tuto_NoteController noteController;

    // 어떤 몬스터가 나올지 csv를 통해 읽어옴
    private static int choice_mon = 0;

    // Start is called before the first frame update
    void Start()
    {
        UniteData.data = CSVReader.Read("World_tuto");
        choice_mon = 0; // 장미

        monster_image = monster.GetComponent<SpriteRenderer>();

        if (UniteData.ReStart)
            Initialized();
        monster_image.sprite = monster_images[choice_mon];
    }

    public void Initialized()
    {
        // CSV를 읽어서 나오는 마리수 설정
        monsterOriginCount = int.Parse(UniteData.data[0]["num"].ToString());
        monsterCount = monsterOriginCount;
        UniteData.mon_num = 1;
        Debug.Log("monsterCountNew : " + monsterCount);
        MonsterColorOrigin();
        UniteData.ReStart = false;
    }

    public void MonsterColorOrigin()
    {
        Debug.Log("monsterCount" + monsterCount);
        Debug.Log("MonsterColorOrigin");
        monster.SetActive(true);

        // 노트 초기화
        noteController.returnNote();

#if true
        Debug.Log("mon_num : " + UniteData.mon_num);
        if (UniteData.data[UniteData.mon_num]["Monster"].ToString().Equals("Rose"))
            choice_mon = 0;
        else if (UniteData.data[UniteData.mon_num]["Monster"].ToString().Equals("Bose"))
            choice_mon = 1;

        UniteData.Closed_Monster = monster_name[choice_mon];
        monster_image.sprite = monster_images[choice_mon];
        Debug.Log("monster_image" + monster_image.sprite);

        // 줄여놨던 a값 다시 원상 복귀
        Color c = monster.GetComponent<SpriteRenderer>().color;
        c.a = 225f;
        monster.GetComponent<SpriteRenderer>().color = c;
#endif
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MonsterSpwan"))
        {
            if (monsterCount > 0)
            {
                if (monsterCount != monsterOriginCount) // 맨처음에는 안나오게
                {
                    UniteData.mon_num++;
                    MonsterColorOrigin();
                }
                monsterCount--;
            }
            else if (monsterCount == 0)
            {
                Debug.Log("Game Clear");
                s_gameclear.ClearGame();
            }
        }
    }
}
