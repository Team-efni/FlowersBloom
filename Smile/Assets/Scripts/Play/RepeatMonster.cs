using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatMonster : MonoBehaviour
{
    public GameObject Monster;

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

        // �ٿ����� a�� �ٽ� ���� ����
        Color c = Monster.GetComponent<SpriteRenderer>().color;
        c.a = 225f;
        Monster.GetComponent<SpriteRenderer>().color = c;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NoteManager"))
        {
            MonsterColorOrigin();
        }
    }
}