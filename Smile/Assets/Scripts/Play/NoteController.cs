using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteController : MonoBehaviour
{
    public Sprite[] noteSprite;
    public GameObject[] note;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Show Note
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player Meet");
            NoteSetting();
            GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true); // leftPad
            GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true); // rightPad
            GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true); // Note_Bg
        }
    }

    private void NoteSetting()
    {
        int[] noteNum = new int[note.Length];

        // 랜덤으로 노트 생성
        for (int i = 0; i < note.Length; i++)
        {
            noteNum[i] = Random.Range(0, 4);
            note[i].GetComponent<Image>().sprite = noteSprite[noteNum[i]];
        }    

    }
}
