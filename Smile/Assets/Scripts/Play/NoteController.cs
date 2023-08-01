using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private float minClickTime = 0.28f; // 최소 클릭 시간
    private float maxClickTime = 0.4f; // 최대 클릭 시간
    private bool[] isClick = { false, false, false, false, false, false, false }; // 클릭중인지 판단

    private int longNotePos = -1;
    private bool canlongClick = false;
    private bool stopNote = false;
    public GameObject longclickImage;
    public GameObject movingNote;

    [SerializeField] private float longClickSpeed;

    RectTransform trans_mn;

    [Header("fade out할 몬스터 오브젝트")] public GameObject target;
    [Header("등장할 노트 배경")] public GameObject Note_Bg;

    [SerializeField] private Transform[] NotePosition;

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
        if (isClick[UniteData.noteIndex])
        {
            clickTime += Time.deltaTime;
            if (trans_mn.position.x >= NotePosition[UniteData.noteIndex + 2].position.x + 10)
            {
                //Debug.Log("noteIndex : " + UniteData.noteIndex);
                stopNote = true;
            }

            if (!stopNote)
            {
                MoveImage();
            }
        }
        else
        {
            clickTime = 0;
        }


        if (longNotePos == UniteData.noteIndex + 1)
            canlongClick = true;
        else canlongClick = false;
    }

    public void Initialized()
    {
        UniteData.noteIndex = 0;
        meetMonster = false;
        UniteData.NoteSuccess = false;
        UniteData.oneNoteSuccess = false;
        DoBgShow(false); // 시작할 때는 상단 노트 UI 비활성화
        movingNote.SetActive(false);
        longclickImage.SetActive(false);
        stopNote = false;
        clickTime = 0;
    }

    // Show Note
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !UniteData.ReStart)
        {
            //#if true
            Debug.Log("Player Meet");
            Set_Note_Count(); // 노트 개수 확인
            Set_Note();
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
        longNotePos = int.Parse(UniteData.data[UniteData.mon_num]["num"].ToString());
        canlongClick = false;
        UniteData.oneNoteSuccess = false;

        // (랜덤으로) 노트 생성
        for (int i = 0; i < noteLength; i++)
        {
            //noteNums[i] = Random.Range(0, 4);
            string columnName = $"noteNums{i}";
            noteNums[i] = int.Parse(UniteData.data[UniteData.mon_num][columnName].ToString());
            Debug.Log("noteNums[" + i + "] : " + noteNums[i]);
            note[i].GetComponent<Image>().sprite = noteSprite[noteNums[i]];
            isClick[i] = false;

            // 롱클릭인 노트인 경우
            if (i == longNotePos - 1)
            {
                movingNote.SetActive(true);
                longclickImage.SetActive(true);

                Image image = note[i].GetComponent<Image>();
                image.color = new Color(128 / 255f, 128 / 255f, 128 / 255f, 0 / 255f);

                Image image_mn = movingNote.GetComponent<Image>();
                image_mn.sprite = note[i].GetComponent<Image>().sprite;
                //RectTransform childTransform = Note_Bg.transform.GetChild(i) as RectTransform;
                //Vector3 position = childTransform.position;

                trans_mn = movingNote.GetComponent<RectTransform>();
                RectTransform trans_lc = longclickImage.GetComponent<RectTransform>();
                //trans_mn.localPosition = position;
                //trans_lc.localPosition = position;
                if (i < 5)
                {
                    trans_mn.position = NotePosition[i + 1].position;
                    trans_lc.position = NotePosition[i + 1].position;
                }
                //trans_mn.position = position;
                //trans_lc.position = position;
            }
        }

    }

    public void NoteDisabled()
    {
        // 노트 회색으로 만들기
        Image image = note[UniteData.noteIndex].GetComponent<Image>();
        image.color = new Color(128 / 255f, 128 / 255f, 128 / 255f, 255 / 255f);
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
        if (isClick[UniteData.noteIndex]) return;
        if (Input.touchCount > 1) return; // 멀티 터치 안되게
        if (meetMonster && !canlongClick)
        {
            if (noteNums[UniteData.noteIndex] == i + 1)
                NoteSuccess();
        }
        Debug.Log("touchClick : " + i);
    }


    public void LongTouchDown(int i)
    {

        // 받는 변수는 입력되는 버튼
        if (meetMonster && canlongClick)
        {
            if (noteNums[UniteData.noteIndex] == i + 1)
            {
                Debug.Log("TouchDown : " + i);
                isClick[UniteData.noteIndex] = true;
            }
        }
        Debug.Log("TouchDown : " + i);
    }

    public void LongTouchUp(int i)
    {
        Debug.Log("TouchUp");
        isClick[UniteData.noteIndex] = false;

        float clicktime = clickTime;

        if (meetMonster && canlongClick)
        {
            stopNote = false;
            longNotePos = 0;
            UniteData.noteIndex++;
            Debug.Log("clicktime : " + clicktime + "");

            // 성공했다면 + 적절한 위치
            if (noteNums[UniteData.noteIndex] == 0)
            {
                trans_mn = movingNote.GetComponent<RectTransform>();
                Debug.Log("noteIndex : " + UniteData.noteIndex);
                if (trans_mn.position.x >= NotePosition[UniteData.noteIndex + 1].position.x - 10 && trans_mn.position.x <= NotePosition[UniteData.noteIndex + 1].position.x + 10)
                {
                    NoteSuccess();
                }
                else
                {
                    // 실패한 경우
                    Image image = note[UniteData.noteIndex].GetComponent<Image>();
                    image.color = new Color(100 / 255f, 100 / 255f, 100 / 255f, 255 / 255f);
                    if (UniteData.noteIndex != noteLength - 1)
                    {
                        UniteData.noteIndex++; // 다음 인덱스 노트 하러감
                    }
                    else
                    {
                        NoteSuccess(); // 모두 성공한 경우로 취급(몬스터 없어지고 초기화)
                    }
                    GameClear.player.GetComponent<PlayerController>().MeetMonsterFail(); // 목숨 줄어듬
                }
            }
        }
    }


    private void MoveImage()
    {
        Debug.Log("롱클릭 움직임");
        movingNote.transform.Translate(Vector2.right * longClickSpeed * Time.deltaTime);
        //note[noteNums[UniteData.noteIndex]].transform.Translate(Vector2.right * Time.deltaTime);
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
        stopNote = false;
        UniteData.oneNoteSuccess = true;
        Debug.Log("noteindex : " + UniteData.noteIndex);

        if (UniteData.noteIndex == noteLength)
        {
            // 모두 성공한 경우
            Debug.Log("All Success");
            meetMonster = false;
            UniteData.NoteSuccess = true;
            MonsterDie();
            DoBgShow(false); // 상단 노트 UI 비활성화
            returnNote();
            movingNote.SetActive(false);
            longclickImage.SetActive(false);

            target.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }

        else
        {
            // 몬스터 깜빡임
            StartCoroutine(MonsterBlink());
        }
    }

    public void DoBgShow(bool check)
    {
        Note_Bg.SetActive(check); // Note_Bg
    }

    IEnumerator MonsterBlink()
    {
        int i = 0;
        while (i < 4) // 총 몬스터 깜빡이는 횟수 * 2
        {
            if (i % 2 == 0)
                target.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 120);
            else target.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

            yield return new WaitForSeconds(0.1f); // 깜빡이는 주기
            i++;
        }
        target.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        yield return null;

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
        while (i > 0)
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
