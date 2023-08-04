using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Progress;
using static UnityEngine.Rendering.DebugUI.Table;

public class InGameCommentManager : MonoBehaviour
{
    TSV tsv;
    DataTable dataTable;
    DataRowCollection dataRowCollection;
    UI_Comment uiComment;

    public GameObject textGroup;
    public Image speaker;
    public Text text;

    public GameObject selectGroup;
    public GameObject[] buttonInSelectGroup;

    public Sprite[] speakerSprites;
    public Sprite[] branchSprites;

    private Dictionary<string, Sprite> characterSprites = new Dictionary<string, Sprite>();
    private void Awake()
    {
        characterSprites.Add("", speakerSprites[0]);
        characterSprites.Add("???", speakerSprites[1]);
        characterSprites.Add("민들레", speakerSprites[2]);
        characterSprites.Add("튤립", speakerSprites[3]);
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
    private int FPP = 8;
    private bool clickTemp = false;
    private bool printAll = false;


    private string tsv_W1ES="Assets\\Resources\\comment\\NewW01_Easy_Story.tsv";

    // Start is called before the first frame update
    void Start()
    {
        uiComment = GetComponent<UI_Comment>();
        tsv=new TSV(tsv_W1ES);
        dataRowCollection = startScripting("Finish");

        handleSelectGroup(3, false);
        clickTemp = true;
        outputScript(page, printAll);
    }

    private void Update()
    {
        //1차 클릭(다음 텍스트 출력) 
        //2차 클릭(즉시 텍스트 출력)
        if (Input.GetMouseButtonDown(0))
        {
            if(pageEnd==1)
            {
                clickTemp = false;
            }

            if(!clickTemp) //printing next text
            {
                if(dataRowCollection[page][BRANCH].ToString()!="preselect")
                {
                    initAboutTextValues();
                    page = int.Parse(dataRowCollection[page][GOTO].ToString());
                    clickTemp = true;
                }
            }
            else //printing immediately
            {
                printAll = true;
                clickTemp =false;
            }
        }

        outputScript(page, printAll);

        frameTime++;
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
            Destroy(textGroup);
            return;
        }
        DataRow row = dataRowCollection[rowX];

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

        //text.text = row[COMMENT].ToString();
        pageEnd = uiComment.printTextToUI(text, row[COMMENT].ToString(), frameTime, FPP, isPrintingImmadiately);
        speaker.sprite = bannerImage(row[CHARACTER].ToString());
    }

    //분기 구현

    private Sprite bannerImage(string character)
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
