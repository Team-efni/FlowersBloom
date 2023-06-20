using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteController : MonoBehaviour
{
    [Header("등장하는 노트 이미지")] public Sprite[] noteSprite;
    [Header("사용할 상단 노트 UI 오브젝트")] public GameObject[] note;
    private int noteLength; // 난이도에 따른 노트 등장 개수
    private int[] noteNums;
    private bool meetMonster = false;
    //private int noteIndex = 0;  // 현재 눌러야할 노트의 자리

    private float clickTime; // 클릭 중인 시간
    public float minClickTime = 1; // 최소 클릭 시간
    private bool[] isClick = {false, false, false, false}; // 클릭중인지 판단

    [Header("fade out할 몬스터 오브젝트")] public GameObject target;
    [Header("등장할 노트 배경")] public GameObject Note_Bg;

    //private List<Dictionary<string, object>> data;

    // Start is called before the first frame update
    void Start()
    {
        Initialized();
        //현재 게임모드 지정
        UniteData.GameMode = "Play";
    }

    // Update is called once per frame
    void Update()
    {
        MoveImage(UniteData.noteIndex);
    }

    public void Initialized()
    {
        UniteData.noteIndex = 0;
        meetMonster = false;
        UniteData.NoteSuccess = false;
        DoBgShow(false); // 시작할 때는 상단 노트 UI 비활성화
    }

    // Show Note
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
//#if true
            Debug.Log("Player Meet");
            Set_Note_Count(); // 노트 개수 확인
            NoteSetting();
            DoBgShow(true); // 상단 노트 UI 활성화

            meetMonster = true;
//#endif
        }
    }

    private void NoteSetting()
    {
        UniteData.NoteSuccess = false;
        noteNums = new int[noteLength];

        // 랜덤으로 노트 생성
        for (int i = 0; i < noteLength; i++)
        {
            //noteNums[i] = Random.Range(0, 4);
            string columnName = $"noteNums{i}";
            noteNums[i] = int.Parse(UniteData.data[UniteData.mon_num][columnName].ToString());
            Debug.Log("noteNums["+i+"] : " + noteNums[i]);
            note[i].GetComponent<Image>().sprite = noteSprite[noteNums[i]];
            isClick[i] = false;
        }

        Set_Note();
    }

    public void NoteDisabled()
    {
        // 노트 회색으로 만들기
        Image image = note[UniteData.noteIndex].GetComponent<Image>();
        image.color = new Color(128/ 255f, 128/ 255f, 128 / 255f, 255/ 255f);
    }

    public void NoteAbled()
    {
        // 노트 원래색으로 만들기
        for (int i = 0; i < noteLength; i++)
        {
            Image image = note[i].GetComponent<Image>();
            image.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        }
    }

    public void touchClick(int i)
    {
        Debug.Log("touchClick : " + i);
        if (Input.touchCount > 1) return; // 멀티 터치 안되게
        if (meetMonster)
        {
            if (noteNums[UniteData.noteIndex] == i)
                NoteSuccess();
        }
    }

    
    public void LongTouchDown(int i)
    {
        Debug.Log("TouchDown : ");
        // 받는 변수는 입력되는 버튼
        if(noteNums[UniteData.noteIndex] == i)
            isClick[i] = true;
    }

    public void LongTouchUp(int i)
    {
        Debug.Log("TouchUp");
        isClick[i] = false;
    }


    private void MoveImage(int i)
    {
        if (isClick[i])
        {
            note[noteNums[UniteData.noteIndex]].transform.Translate(Vector2.right * Time.deltaTime);
        }
    }
    

    /*
    public void touchClickLeftUp()
    {
        Debug.Log("touchClickLeftUp : ");
        if (Input.touchCount > 1) return; // 멀티 터치 안되게
        if (meetMonster)
        {
            if (noteNums[noteIndex] == 0)
                NoteSuccess();
        }
            
    }

    public void touchClickLeftDown()
    {
        Debug.Log("touchClickLeftDown");
        if (Input.touchCount > 1) return; // 멀티 터치 안되게
        if (meetMonster)
        {
            if (noteNums[noteIndex] == 1)
                NoteSuccess();
        }
            
    }

    public void touchClickRightUp()
    {
        Debug.Log("touchClickRightUp");
        if (Input.touchCount > 1) return; // 멀티 터치 안되게
        if (meetMonster)
        {
            if (noteNums[noteIndex] == 2)
                NoteSuccess();
        }
            
    }

    public void touchClickRightDown()
    {
        Debug.Log("touchClickRightDown");
        if (Input.touchCount > 1) return; // 멀티 터치 안되게
        if (meetMonster)
        {
            if (noteNums[noteIndex] == 3)
                NoteSuccess();
        }
    }*/

    private void NoteSuccess()
    {
        Debug.Log("Note Success");
        NoteDisabled();
        UniteData.noteIndex++;

        if (UniteData.noteIndex == noteLength)
        {
            // 모두 성공한 경우
            Debug.Log("All Success");
            meetMonster = false;
            UniteData.NoteSuccess = true;
            MonsterDie();
            DoBgShow(false); // 상단 노트 UI 비활성화
            returnNote();
        }
    }

    public void DoBgShow(bool check)
    {
        Note_Bg.SetActive(check); // Note_Bg
    }

    // 몬스터 죽기
    private void MonsterDie()
    {
        StartCoroutine("MonsterFadeOut");
    }

    // 몬스터 페이드 아웃 처리
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

    // 노트들 처음 상태로 되돌리기
    public void returnNote()
    {
        UniteData.noteIndex = 0;
        NoteAbled();
    }

    private void Set_Note_Count()
    {
        /*
        switch(UniteData.Closed_Monster)
        {
            case "Rose":
                noteLength = 3;
                break;
            case "Cosmos":
                noteLength = 5;
                break;
            case "MorningGlory":
                noteLength = 6;
                break;
            default:
                noteLength = 3;
                break;
        }
        */

        noteLength = int.Parse(UniteData.data[UniteData.mon_num]["noteLength"].ToString());

        Debug.Log("noteLength : " + noteLength);
    }

    private void Set_Note()
    {
        // 자식 노트들 모두 비활성화
        for (int i = 0; i < 6; i++)
        {
            Note_Bg.transform.GetChild(i).gameObject.SetActive(false);
        }

        // 몬스터에 해당하는 수만큼 활성화
        for (int i = 0; i < noteLength; i++)
        {
            Note_Bg.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
