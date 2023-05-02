using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatMonster : MonoBehaviour
{
    public GameObject monster;
    [SerializeField] private int monsterCount; // 몬스터 등장 횟수

    public GameClear s_gameclear;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MonsterColorOrigin()
    {
        Debug.Log("MonsterColorOrigin");
        monster.SetActive(true);

        // 줄여놨던 a값 다시 원상 복귀
        Color c = monster.GetComponent<SpriteRenderer>().color;
        c.a = 225f;
        monster.GetComponent<SpriteRenderer>().color = c;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NoteManager"))
        {
            /*if (monsterCount > 0)
            {
                monsterCount--;
                MonsterColorOrigin();
            }*/
             if(monsterCount == 5)
            {
                Debug.Log("Game Clear");
                s_gameclear.ClearGame();
            }
        }
    }

}
