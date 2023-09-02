using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class InGameCommentManager : MonoBehaviour
{
    TSV tsv;
    DataTable dataTable;
    DataRowCollection dataRowCollection;
    UI_Comment uiComment;
    Animator animator;

    public GraphicRaycaster graphicRaycaster;
    public EventSystem eventSystem;

    public GameObject textGroup;
    public GameObject blind;
    public GameObject speaker;
    public Text text;
    public Text speakerText;
    public GameObject stopPanel;

    public GameObject selectGroup;
    public GameObject[] buttonInSelectGroup;

    public Sprite[] speakerSprites;
    public Sprite[] branchSprites;

    public GameObject[] UI_system;
    public GameObject[] gameCharacters;

    private Dictionary<string, GameObject> inGame_characters = new Dictionary<string, GameObject>();
    private Dictionary<string, Sprite> characterSprites = new Dictionary<string, Sprite>();
    private void Awake()
    {
        characterSprites.Add("민들레", speakerSprites[0]);
        characterSprites.Add("튤립", speakerSprites[1]);
        characterSprites.Add("물망초", speakerSprites[2]);

        inGame_characters.Add("Dandelion", gameCharacters[0]);
        inGame_characters.Add("Tulip", gameCharacters[1]);
        inGame_characters.Add("ForgetMeNot", gameCharacters[2]);

        Debug.LogWarning("빌드 전에 !꼭! play/canvas/comment 오브젝트를 비활성화 해주세요!!!!!");

        animator = blind.GetComponent<Animator>();
    }

    private const int COMMAND   = 0;
    private const int NUMBERING = 1;
    private const int GOTO      = 2;
    private const int BRANCH    = 3;
    private const int CHARACTER = 4;
    private const int COMMENT   = 5;

    private int page = 0;
    private int pageEnd = 0; //1:End

    private int frameTime = 0;
    private int FPP = 4;
    private bool clickTemp = false;
    private bool printAll = false;


    private string[] tsv_file = {
        "NewW01_Easy_Story.tsv",
        "NewW01_Easy_Story.tsv",
        "NewW01_Easy_Story.tsv",
        "NewW01_Easy_Story.tsv",
        "NewW01_Easy_Story.tsv",
        "NewW01_Easy_Story.tsv"
    };

    private void OnEnable()
    {
        UniteData.GameMode = "Scripting";
        Debug.Log(UniteData.GameMode);

        //스토리 시작 전 오브젝트 비활성화
        Time.timeScale = 0f; //이거 때문에 페이드 처리가 안됨
        Application.targetFrameRate = 60;
        foreach (GameObject obj in UI_system)
        {
            obj.SetActive(false);
        }
        foreach(GameObject obj in gameCharacters)
        {
            Debug.Log(obj.name);
            obj.SetActive(false);
        }

        uiComment = GetComponent<UI_Comment>();
        tsv = new TSV(tsv_file[UniteData.Difficulty - 1]);

        if (UniteData.StoryClear[UniteData.Difficulty - 1] == 0 && !UniteData.finishGame)
        {
            dataRowCollection = startScripting("Prestart");
        }
        else if (UniteData.StoryClear[UniteData.Difficulty - 1] == 1 && UniteData.finishGame)
        {
            dataRowCollection = startScripting("Finish");
        }
        else
        {
            //스토리 끝
            Time.timeScale = 1f;
            UniteData.GameMode = "Play";
            foreach (GameObject obj in UI_system)
            {
                obj.SetActive(true);
            }
            foreach (var entry in inGame_characters)
            {
                if(entry.Key == UniteData.Selected_Character)
                {
                    entry.Value.SetActive(true);
                    break;
                }
            }

            textGroup.SetActive(false);
            return;
        }

        handleSelectGroup(3, false);
        clickTemp = true;
        outputScript(page, printAll);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭 위치에서 레이캐스트 수행
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEventData, results);

            // 레이캐스트가 특정 UI 오브젝트와 충돌하면
            if (results.Count > 0)
            {
                bool checkingBackBtn = false;
                foreach(var selectedResult in results)
                {
                    GameObject selectedObject= selectedResult.gameObject;
                    if (selectedObject.name == "BtnStop" || selectedObject.name == "No")
                    {
                        checkingBackBtn = true;
                        break;
                    }
                }

                if(!checkingBackBtn && !stopPanel.activeSelf) 
                {
                    if (pageEnd == 1)
                    {
                        clickTemp = false;
                    }

                    if (!clickTemp) //printing next text
                    {
                        if (dataRowCollection[page][BRANCH].ToString() != "preselect")
                        {
                            initAboutTextValues();
                            page = int.Parse(dataRowCollection[page][GOTO].ToString());
                            clickTemp = true;
                        }
                    }
                    else //printing immediately
                    {
                        printAll = true;
                        clickTemp = false;
                    }
                }
            }
        }

        outputScript(page, printAll);
        frameTime ++;
    }

    private void initAboutTextValues()
    {
        frameTime = 0;
        printAll = false;
    }


    public DataRowCollection startScripting(string command)
    {
        dataTable = tsv.limitTSV(command);
        return dataTable.Rows;
    }


    private void outputScript(int rowX, bool isPrintingImmadiately)
    {
        if (rowX + 1 >= dataRowCollection.Count)
        {
            textGroup.SetActive(false);
            //스토리 끝
            Time.timeScale = 1f;
            foreach (GameObject obj in UI_system)
            {
                obj.SetActive(true);
            }
            foreach (var entry in inGame_characters)
            {
                if (entry.Key == UniteData.Selected_Character)
                {
                    entry.Value.SetActive(true);
                    break;
                }
            }

            UniteData.GameMode = "Play";
            UniteData.StoryClear[UniteData.Difficulty - 1] += 1;
            UniteData.SaveUserData();
            return;
        }
        DataRow row = dataRowCollection[rowX];

        if (dataRowCollection[rowX][CHARACTER].ToString() == "System") 
        {
            if(dataRowCollection[rowX][COMMENT].ToString()=="Blind")
            {
                //블라인드 작업 지시
                Debug.Log("Blind");
            }
            return;
        }

        handleSelectGroup(3, false);
        //분기 시작
        if (row[BRANCH].ToString()=="preselect")
        {
            List<DataRow> selRow=new List<DataRow>();
            for(int x=1; ; x++) 
            {
                if (dataRowCollection[rowX+x][BRANCH].ToString()=="select")
                {
                    selRow.Add(dataRowCollection[rowX + x]);
                }
                else
                {
                    break;
                }
            }

            for(int x=0; x<selRow.Count; x++)
            {
                handleSelectGroup(x + 1, true);
                buttonInSelectGroup[x].GetComponentInChildren<Text>().text = selRow[x][COMMENT].ToString();
            }
        }

        pageEnd = uiComment.printTextToUI(text, row[COMMENT].ToString(), frameTime, FPP, isPrintingImmadiately);

        Image speakerImage = speaker.GetComponent<Image>();
        speakerImage.sprite = speakersBannerImage(row[CHARACTER].ToString());
        if (speakerImage.sprite == null)
            speaker.SetActive(false);
        else
            speaker.SetActive(true);
        if (row[CHARACTER].ToString()=="튤립")
            speaker.transform.localPosition = new Vector2(1200f, -90f);
        else
            speaker.transform.localPosition = new Vector2(-1200f, -90f);

        speakerText.text = row[CHARACTER].ToString();
    }

    //분기 구현

    private Sprite speakersBannerImage(string character)
    {
        if (characterSprites.TryGetValue(character, out Sprite sprite))
        {
            return sprite;
        }
        else
        {
            return null;
        }
    }

    private void handleSelectGroup(int buttonAmount, bool isActive)
    {
        selectGroup.SetActive(isActive);
        for(int x=0; x<buttonAmount; x++)
        {
            buttonInSelectGroup[x].SetActive(isActive);
        }
    }

    public void branchInStoryByUsersSelecting(int buttonCode)
    {
        //코드에 맞는 Row를 가져와 
        //Goto를 추출해서 부여
        initAboutTextValues();
        clickTemp = true;
        handleSelectGroup(3, false);
        page = int.Parse(dataRowCollection[page+buttonCode][GOTO].ToString());
    }
}
