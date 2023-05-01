using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatMonster : MonoBehaviour
{
    public GameObject Monster;
    [SerializeField] private int monsterCount; // 몬스터 등장 횟수

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
        Monster.SetActive(true);

        // 줄여놨던 a값 다시 원상 복귀
        Color c = Monster.GetComponent<SpriteRenderer>().color;
        c.a = 225f;
        Monster.GetComponent<SpriteRenderer>().color = c;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NoteManager"))
        {
            if (monsterCount > 0)
            {
                monsterCount--;
                MonsterColorOrigin();
            }
            else if(monsterCount == 0)
            {
                Debug.Log("Game Clear");
            }
        }
    }
}
