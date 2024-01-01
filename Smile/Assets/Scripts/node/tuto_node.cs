using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Text.RegularExpressions;

using CSVFormat = System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>>;
using CSVDict = System.Collections.Generic.Dictionary<string, object>;
using UnityEditor.Rendering;

public class tuto_node : MonoBehaviour
{
    private const int A = 0;
    private const int B = 1;
    private const int C = 2;
    private const int D = 3;

    private int cas;

    private List<Node_data> node_location = new List<Node_data>();
    public GameObject nodes_prefab;
    public GameObject nodedrag_prefab;
    //public LineRenderer line_renderer;
    public GameObject Highlight_Node;
    public GameObject[] backgrounds;

    [Header("아래의 항목에다가 노트의 이미지를 넣으면 됩니다")]
    public Sprite[] Node_image;

    [Header("아래의 항목에다가 노트의 링을 넣으면 됩니다")]
    public Sprite[] Ring;

    [Header("아래의 항목에다가 발광링을 넣으면 됩니다")]
    public Sprite[] BR_Ring;
    public Sprite[] BR_Big_Ring;

    [Header("노트간 간격을 조절합니다")]
    public float radius_MIN = 420f; //difault value 420f
    public float radius_MAX = 1000f; //difault value 1000f

    private IScenePass sceneLoader;
    public static bool UnPassed; //노트포인트를 임시 저장

    //public static int LineIndex_x = 0; //좀 느낌 없는데 급하니까 전역변수로 다른 소스코드에 접근 허용 [HACK]

    private int initLayerValue = 25;

    //노드를 리스트의 순서에 따라 하나를 차례로 배치하는 함수
    void node_placement(int node_array)
    {
        if (node_location.Count == 0)
        {
            return;
        }
        else
        {
            if (node_location[node_array].mode == 1)
            {
                SpriteRenderer sr = nodes_prefab.GetComponent<SpriteRenderer>();
                SpriteRenderer rn = nodes_prefab.transform.Find("ring").GetComponent<SpriteRenderer>();
                sr.sprite = node_location[node_array].procedure;
                rn.sprite = node_location[node_array].Ring_Color;

                GameObject newObject = Instantiate(nodes_prefab, node_location[node_array].vector2, Quaternion.identity);

                SpriteRenderer spriteRenderer = newObject.GetComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = initLayerValue--; // 숫자가 작을수록 뒤에 그려집니다.
            }
            else if (node_location[node_array].mode == 2)
            {
                SpriteRenderer sr = nodedrag_prefab.GetComponent<SpriteRenderer>();
                SpriteRenderer rn = nodedrag_prefab.transform.Find("ring").GetComponent<SpriteRenderer>();
                SpriteRenderer br = nodedrag_prefab.transform.Find("bright").GetComponent<SpriteRenderer>();
                sr.sprite = node_location[node_array].procedure;
                rn.sprite = node_location[node_array].Ring_Color;
                br.sprite = node_location[node_array].bright_Color;

                GameObject newNode = Instantiate(nodedrag_prefab, node_location[node_array].vector2, Quaternion.identity);
                newNode.name = "nodedrag";
                node_management_drag nmd = newNode.GetComponent<node_management_drag>();
                nmd.ping = node_location[node_array + 1].vector2;

                SpriteRenderer spriteRenderer = newNode.GetComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = initLayerValue--; // 숫자가 작을수록 뒤에 그려집니다.
            }
            else if (node_location[node_array].mode == 3)
            {
                SpriteRenderer sr = nodes_prefab.GetComponent<SpriteRenderer>();
                SpriteRenderer rn = nodes_prefab.transform.Find("ring").GetComponent<SpriteRenderer>();
                sr.sprite = node_location[node_array].procedure;
                rn.sprite = node_location[node_array].Ring_Color;

                //nodes_prefab 내에 속해있는 ring object를 비활성화 한다
                nodes_prefab.transform.Find("ring").gameObject.SetActive(false);

                GameObject sha = Instantiate(nodes_prefab, node_location[node_array].vector2, Quaternion.identity);
                sha.name = "shadow";

                SpriteRenderer spriteRenderer = sha.GetComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = initLayerValue--; // 숫자가 작을수록 뒤에 그려집니다.


                nodes_prefab.transform.Find("ring").gameObject.SetActive(true);
            }
            else
            {
                Instantiate(nodes_prefab, node_location[node_array].vector2, Quaternion.identity);
            }
        }
    }

    /*private void Insert_Line(List<Vector2> v)
    {
        List<Vector2> vector = new List<Vector2>(v);

        //list 안의 원소들을 Reverse 시킨다
        vector.Reverse();

        *//*LineIndex_x = LineIndex_x + 1; //좀 느낌 없는데 급하니까 전역변수로 다른 소스코드에 접근 허용 [HACK]

        //line renderer에 좌표를 넣는다
        for (int i = 0; i < LineIndex_x; i++)
        {
            line_renderer.positionCount = LineIndex_x;
            line_renderer.SetPosition(i, vector[i]);
        }*//*
    }*/

    public void Delete_Line() //아직 미사용
    {
        //LineIndex_x = LineIndex_x - 1; //좀 느낌 없는데 급하니까 전역변수로 다른 소스코드에 접근 허용 [HACK]
        UnityEngine.Debug.Log("진행");
    }

    //노드를 생성하는 부분입니다. Coroutine으로 구현
    private IEnumerator D_Coroutine()
    {
        List<Vector2> vector = new List<Vector2>();

        for (int i = 0; i < node_location.Count; i++)
        {
            vector.Add(node_location[i].vector2);
            yield return new WaitForSeconds(node_location[i].time);

            node_placement(i);

            //Insert_Line(vector);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        //Play 씬을 ScenePass를 통해 비동기적으로 로드한다
        sceneLoader = GetComponent<IScenePass>();
        sceneLoader.LoadSceneAsync("Tutorial");

        //컷씬 초기화
        UniteData.Move_Progress = true;
        UnPassed = false;
        UniteData.Node_LifePoint = 2; //노드 목숨
        UniteData.Node_Click_Counter = 0; //노드 클릭 횟수
        /*LineIndex_x = 0;
        line_renderer.material.color = Color.white;*/

        //배경 세팅
        // 처음에는 모든 배경 초기화
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(false);
        }

        // 배경 지정
        switch (UniteData.Difficulty)
        {
            case 1:
                backgrounds[0].SetActive(true);
                break;
            case 2:
                backgrounds[1].SetActive(true);
                break;
            case 3:
                backgrounds[2].SetActive(true);
                break;
            case 4:
                backgrounds[3].SetActive(true);
                break;
            case 5:
                backgrounds[3].SetActive(true);
                break;
            case 6:
                backgrounds[3].SetActive(true);
                break;
            default:
                backgrounds[0].SetActive(true);
                break;
        }

        //노드의 초기 설정을 지정한다
        Initialize_node_setting();

        StartCoroutine(D_Coroutine());
    }

    private void Update()
    {
        //line_renderer.positionCount = LineIndex_x; //좀 느낌 없는데 급하니까 전역변수로 다른 소스코드에 접근 허용 [HACK]

        //만약 컷씬을 클리어 했거나 / 기회포인트가 남아있는 상황에서 실패했을 때
        if ((UniteData.Node_LifePoint >= 0 && UniteData.Node_Click_Counter == node_location.Count) || UnPassed)
        {
            //쬐끔만 대기 [HACK]
            for (int x = 0; x < 200000000; x++)
            {
                //대기
            }
            //데이터 초기화
            UniteData.Node_LifePoint = 2;
            UniteData.Node_Click_Counter = 0;
            UnPassed = false;

            UniteData.mon_num--;
            UniteData.GameMode = "Play";
            //클리어 애니메이션 실행
            //첫 그거면 cutend로,
            if (TutorialCommentManager.cutsceneScriptPassed == false) //🤔: 이게 맞아??
            {
                StorySepCommand.Instance.setCommandBranch(StorySepCommand.commandNum.CutEnd);
                TutorialCommentManager.cutsceneScriptPassed = true;
            }
            //아니면 play에서 comment 비활성화로
            sceneLoader.SceneLoadStart("Tutorial");
        }

        else if(UniteData.Node_LifePoint < 0)
        {
            //쬐끔만 대기 [HACK]
            for (int x = 0; x < 100000000; x++)
            {
                //대기
            }
            //데이터 초기화
            UniteData.Node_LifePoint = 2;
            UniteData.Node_Click_Counter = 0;
            UnPassed = false;

            UniteData.mon_num--;
            UniteData.lifePoint--;
            UniteData.GameMode = "Play";
            //클리어 애니메이션 실행
            //첫 그거면 cutend로,
            if (TutorialCommentManager.cutsceneScriptPassed == false) //🤔: 이게 맞아??
            {
                StorySepCommand.Instance.setCommandBranch(StorySepCommand.commandNum.CutEnd);
                TutorialCommentManager.cutsceneScriptPassed = true;
            }
            //아니면 play에서 comment 비활성화로
            sceneLoader.SceneLoadStart("Tutorial");
        }

    }



    private int typeToInt(string type)
    {
        switch (type)
        {
            case "A":
                return 0;
            case "B":
                return 1;
            case "C":
                return 2;
            case "D":
                return 3;
            case "NON":
                return 4;
            default:
                return 4;
        }
    }

    private void Initialize_node_setting()
    {
        string fileName = "Cut_Easy_Type1";

        CSVFormat csvFormat = CSVReader.Read(fileName);

        foreach (CSVDict dict in csvFormat)
        {
            string c_type = dict["Color_Type"].ToString();
            int c_mode = Convert.ToInt32(dict["Mode"]);
            float c_wait = Convert.ToSingle(dict["Waiting"]);
            Vector2 c_pos = new Vector2(Convert.ToSingle(dict["PosX"]), Convert.ToSingle(dict["PosY"]));


            Debug.Log(c_type + ": " + typeToInt(c_type));
            node_location.Add(new Node_data(
                c_pos,
                Node_image[typeToInt(c_type)],
                Ring[typeToInt(c_type)],
                BR_Ring[typeToInt(c_type)],
                c_mode,
                c_wait));
        }
    }
}
