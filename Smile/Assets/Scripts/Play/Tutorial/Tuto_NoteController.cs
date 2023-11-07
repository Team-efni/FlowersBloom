using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  가이드용 UI + 튜토리얼용 노트 컨트롤러
// 기본 NoteController에서 튜토리얼만 구분할까 했지만 안쓰이는 함수가 너무 많아서 새로 작성함
public class Tuto_NoteController : MonoBehaviour
{
    [Header("등장하는 노트 이미지")] public Sprite[] noteSprite;
    [Header("사용할 상단 노트 UI 오브젝트")] public GameObject[] note;
    private int noteLength; // 노트 등장 개수
    private int[] noteNums;
    private bool meetMonster = false;

    [Header("fade out할 몬스터 오브젝트")] public GameObject target;
    [Header("등장할 상단 노트 UI")] public GameObject Note_Bg;

    [Header("가이드 텍스트 박스 UI")] public GameObject guideTextBox;
    [Header("가이드 텍스트 UI")] public Text guideText;
    [Header("가이드 손가락 UI")] public GameObject guideFinger;

    // 노트 버튼 위치
    [SerializeField] private Transform[] noteBtn;
    Transform transFinger;

    // Start is called before the first frame update
    void Start()
    {
        Initialized();
    }

    public void Initialized()
    {
        UniteData.noteIndex = 0;
        meetMonster = false;
        UniteData.NoteSuccess = false;
        UniteData.oneNoteSuccess = false;
        DoBgShow(false); // 시작할 때는 상단 노트 UI 비활성화

        // 가이드 손가락, 텍스트박스 비활성화
        guideTextBox.SetActive(false);
        guideFinger.SetActive(false);

        transFinger = guideFinger.GetComponent<Transform>();
    }

    public void DoBgShow(bool check)
    {
        Note_Bg.SetActive(check); // Note_Bg
    }

    private void Set_Note()
    {
        // 자식 노트들 모두 비활성화
        for (int i = 0; i < Note_Bg.transform.childCount; i++)
        {
            Note_Bg.transform.GetChild(i).gameObject.SetActive(false);
        }

        // 몬스터에 해당하는 수만큼 활성화
        for (int i = 0; i < noteLength; i++)
        {
            Note_Bg.transform.GetChild(i).gameObject.SetActive(true);
        }
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

            if (UniteData.mon_num == 1)
                GuideTextSet();
            //#endif
        }
    }

    public void GuideTextSet()
    {
        // 텍스트 박스 표시
        guideTextBox.SetActive(true);
        guideText.text = "노트박스에 표시되는 색상 순서대로\r\n좌, 우의 노트를 터치하세요.";

        // 가이드 손가락 표시
        GuideFingerSet();

        // 이동 멈추기
        UniteData.Move_Progress = false;
    }


    private void GuideTextOff()
    {
        guideTextBox.SetActive(false);
    }

    private void GuideFingerSet()
    {
        guideFinger.SetActive(true);
        transFinger.position = noteBtn[noteNums[UniteData.noteIndex]-1].position;
    }

    private void GuideFingerOff()
    {
        guideFinger.SetActive(false);
    }


    private void NoteSetting()
    {
        UniteData.NoteSuccess = false;
        noteNums = new int[noteLength];

        UniteData.oneNoteSuccess = false;

        // 노트 생성
        for (int i = 0; i < noteLength; i++)
        {
            string columnName = $"noteNums{i}";
            noteNums[i] = int.Parse(UniteData.data[UniteData.mon_num][columnName].ToString());
            Debug.Log("noteNums[" + i + "] : " + noteNums[i]);
            note[i].GetComponent<Image>().sprite = noteSprite[noteNums[i]-1];
        }
        UniteData.noteNums = noteNums;
        UniteData.NoteSet = true;
    }

    // 노트들 처음 상태로 되돌리기
    public void returnNote()
    {
        UniteData.noteIndex = 0;
        NoteAbled();
    }

    private void NoteSuccess()
    {
        Debug.Log("Note Success");
        NoteDisabled();
        UniteData.oneNoteSuccess = true;
        UniteData.lastNoteIndex = UniteData.noteIndex;
        UniteData.noteIndex++;

        Debug.Log("noteindex : " + UniteData.noteIndex);
        if (UniteData.noteIndex == noteLength)
        {
            // 모두 성공한 경우
            Debug.Log("All Success");
            meetMonster = false;
            UniteData.NoteSuccess = true;
            UniteData.Move_Progress = true;
            MonsterDie();
            DoBgShow(false); // 상단 노트 UI 비활성화
            returnNote();

            target.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

            // 튜토리얼 가이드, 손가락 off
            GuideTextOff();
            GuideFingerOff();

            UniteData.tuto_meetMonster = false;
        }

        else
        {
            // 몬스터 깜빡임
            StartCoroutine(MonsterBlink());

            if (UniteData.mon_num <= 1 || UniteData.tuto_meetMonster)
            {
                // 손가락 가이드 움직이기
                GuideFingerSet();
            }
        }
    }

    public void touchClick(int i)
    {
        if (Input.touchCount > 1) return; // 멀티 터치 안되게
        if (meetMonster)
        {
            if (noteNums[UniteData.noteIndex] == i + 1)
                NoteSuccess();
        }
        Debug.Log("touchClick : " + i);
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

    private void Set_Note_Count()
    {
        noteLength = int.Parse(UniteData.data[UniteData.mon_num]["noteLength"].ToString());
        Debug.Log("noteLength : " + noteLength);
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
}
