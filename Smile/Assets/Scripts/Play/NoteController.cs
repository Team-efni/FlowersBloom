using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteController : MonoBehaviour
{
    [Header("µÓ¿Â«œ¥¬ ≥Î∆Æ ¿ÃπÃ¡ˆ")] public Sprite[] noteSprite;
    [Header("ªÁøÎ«“ ªÛ¥‹ ≥Î∆Æ UI ø¿∫Í¡ß∆Æ")] public GameObject[] note;
    private int[] noteNums;
    private bool meetMonster = false;
    private int noteIndex = 0;  // «ˆ¿Á ¥≠∑Øæﬂ«“ ≥Î∆Æ¿« ¿⁄∏Æ

    [Header("fade out«“ ∏ÛΩ∫≈Õ ø¿∫Í¡ß∆Æ")] public GameObject target;

    public bool noteSuccess; // ≥Î∆Æ º∫∞¯

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        Initialized();
=======
        //«ˆ¿Á ∞‘¿”∏µÂ ¡ˆ¡§
        UniteData.GameMode = "Play";

        noteIndex = 0;
        meetMonster = false;
        DoBgShow(false); // Ω√¿€«“ ∂ß¥¬ ªÛ¥‹ ≥Î∆Æ UI ∫Ò»∞º∫»≠
>>>>>>> f0e2791 (ÏÉà Ïù¥ÎØ∏ÏßÄ Ï†ÅÏö©)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialized()
    {
        noteIndex = 0;
        meetMonster = false;
        noteSuccess = false;
        DoBgShow(false); // Ω√¿€«“ ∂ß¥¬ ªÛ¥‹ ≥Î∆Æ UI ∫Ò»∞º∫»≠

    }

    // Show Note
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player Meet");
            NoteSetting();
            DoBgShow(true); // ªÛ¥‹ ≥Î∆Æ UI »∞º∫»≠
            meetMonster = true;
        }
    }

    private void NoteSetting()
    {
        noteSuccess = false;
        noteNums = new int[note.Length];

        // ∑£¥˝¿∏∑Œ ≥Î∆Æ ª˝º∫
        for (int i = 0; i < note.Length; i++)
        {
            noteNums[i] = Random.Range(0, 4);
            note[i].GetComponent<Image>().sprite = noteSprite[noteNums[i]];
        }
    }

    public void NoteDisabled()
    {
        // ≥Î∆Æ »∏ªˆ¿∏∑Œ ∏∏µÈ±‚
        Image image = note[noteIndex].GetComponent<Image>();
        image.color = new Color(128/ 255f, 128/ 255f, 128 / 255f, 255/ 255f);
    }

    public void NoteAbled()
    {
        // ≥Î∆Æ ø¯∑°ªˆ¿∏∑Œ ∏∏µÈ±‚
        for (int i = 0; i < note.Length; i++)
        {
            Image image = note[i].GetComponent<Image>();
            image.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        }
    }

    public void touchClickLeftUp()
    {
        Debug.Log("touchClickLeftUp");
        if (meetMonster)
        {
            if (noteNums[noteIndex] == 0)
                NoteSuccess();
        }
            
    }

    public void touchClickLeftDown()
    {
        Debug.Log("touchClickLeftDown");
        if (meetMonster)
        {
            if (noteNums[noteIndex] == 1)
                NoteSuccess();
        }
            
    }

    public void touchClickRightUp()
    {
        Debug.Log("touchClickRightUp");
        if (meetMonster)
        {
            if (noteNums[noteIndex] == 2)
                NoteSuccess();
        }
            
    }

    public void touchClickRightDown()
    {
        Debug.Log("touchClickRightDown");
        if (meetMonster)
        {
            if (noteNums[noteIndex] == 3)
                NoteSuccess();
        }
    }

    private void NoteSuccess()
    {
        Debug.Log("Note Success");
        NoteDisabled();
        noteIndex++;

        if (noteIndex == note.Length)
        {
            // ∏µŒ º∫∞¯«— ∞ÊøÏ
            Debug.Log("All Success");
            meetMonster = false;
            noteSuccess = true;
            MonsterDie();
            DoBgShow(false); // ªÛ¥‹ ≥Î∆Æ UI ∫Ò»∞º∫»≠
            returnNote();
        }
    }

    private void DoBgShow(bool check)
    {
        GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(check); // Note_Bg
    }

    // ∏ÛΩ∫≈Õ ¡◊±‚
    private void MonsterDie()
    {
        StartCoroutine("MonsterFadeOut");
    }

    // ∏ÛΩ∫≈Õ ∆‰¿ÃµÂ æ∆øÙ √≥∏Æ
    IEnumerator MonsterFadeOut()
    {
        int i = 10;
        while(i > 0)
        {
            i -= 1;
            float f = i / 10.0f;
            Color c = target.GetComponent<SpriteRenderer>().color;
            c.a = f;
            target.GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(0.02f);
        }

        target.gameObject.SetActive(false);
    }

    // ≥Î∆ÆµÈ √≥¿Ω ªÛ≈¬∑Œ µ«µπ∏Æ±‚
    void returnNote()
    {
        noteIndex = 0;
        NoteAbled();
    }
}
