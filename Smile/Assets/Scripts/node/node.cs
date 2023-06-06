/*
*노드의 전체적인 구조를 담당 및 생성하는 스크립트
*
*구현 목표
*-노드의 일괄적인 생성 담당
*-노드간 Line Renderer를 통한 연결
*-씬 시작 담당
*
*난이도 변경 시 수정절차
*-Initialize_node_setting() 수정 (난이도에 따른 노드의 개수 변경)
*-node_management의 시간값 수정
*-AnimationClip 노드 감소 타이밍 수정
*-node_delete의 MAX_TIME 수정 
*
*위 스크립트의 초기 버전은 김시훈이 작성하였음
*문의사항은 gkfldys1276@yandex.com으로 연락 바랍니다 (카톡도 가능).
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Node_data
{
    private static float SIDE_V = 500f;
    private static float STR_V = 900f;

    public Vector2 vector2 = new Vector2();
    public Sprite procedure;
    public Sprite Ring_Color;
    public Sprite bright_Color;
    public int mode; //1: 클릭 / 2: 드래그(시작 포인트) / 3: 드래그 (끝 포인트)

    public Node_data(Vector2 _vector2, Sprite _procedure, Sprite _ring_Color, Sprite _bright_Color, int _mode)
    {
        this.vector2 = _vector2;

        //TODO: 이미지 적용 안될 때 예외처리 필요
        this.procedure = _procedure;
        this.Ring_Color = _ring_Color;
        this.bright_Color = _bright_Color;
        this.mode = _mode;
    }

    static public Vector2 resettingShadowNode(Vector2 forwardVector, int direction)
    {
        if (direction == 0) //12시
        {
            Vector2 v = new Vector2(forwardVector.x, forwardVector.y + STR_V);
            if (v.y>500f)
            {
                v.y = 500f;
            }
            return new Vector2(v.x, v.y);
        }
        else if(direction == 1) //1~2시
        {
            Vector2 v = new Vector2(forwardVector.x+ SIDE_V, forwardVector.y + SIDE_V);
            if (forwardVector.y > 500f)
            {
                v.y = 500f;
            }
            if(forwardVector.x>1300f)
            {
                v.x = 1300f;
            }
            return new Vector2(v.x, v.y);
        }
        else if (direction == 2) //3시
        {
            Vector2 v = new Vector2(forwardVector.x + STR_V, forwardVector.y);
            if (forwardVector.x > 1300f)
            {
                v.x = 1300f;
            }
            return new Vector2(v.x, v.y);
        }
        else if (direction == 3) //4~5시
        {
            Vector2 v = new Vector2(forwardVector.x + SIDE_V, forwardVector.y - SIDE_V);
            if (forwardVector.y < -500f)
            {
                v.y = -500f;
            }
            if (forwardVector.x > 1300f)
            {
                v.x = 1300f;
            }
            return new Vector2(v.x, v.y);
        }
        else if(direction==4) //6시
        {
            Vector2 v = new Vector2(forwardVector.x, forwardVector.y - STR_V);
            if (forwardVector.y < -500f)
            {
                v.y = -500f;
            }
            return new Vector2(v.x, v.y);
        }
        else if(direction==5) //7~8시
        {
            Vector2 v = new Vector2(forwardVector.x - SIDE_V, forwardVector.y - SIDE_V);
            if (forwardVector.y < -500f)
            {
                v.y = -500f;
            }
            if (forwardVector.x < -1300f)
            {
                v.x = -1300f;
            }
            return new Vector2(v.x, v.y);
        }
        else if(direction==6) //9시
        {
            Vector2 v = new Vector2(forwardVector.x - STR_V, forwardVector.y);
            if (forwardVector.x < -1300f)
            {
                v.x = -1300f;
            }
            return new Vector2(v.x, v.y);
        }
        else if(direction==7) //10~11시
        {
            Vector2 v = new Vector2(forwardVector.x - SIDE_V, forwardVector.y + SIDE_V);
            if (forwardVector.y > 500f)
            {
                v.y = 500f;
            }
            if (forwardVector.x < -1300f)
            {
                v.x = -1300f;
            }
            return new Vector2(v.x, v.y);
        }
        return forwardVector;
    }
}

public class node : MonoBehaviour
{
    private const int A = 0;
    private const int B = 1;
    private const int C = 2;
    private const int D = 3;

    private List<Node_data> node_location = new List<Node_data>();
    public GameObject nodes_prefab;
    public GameObject nodedrag_prefab;
    public LineRenderer line_renderer;
    public GameObject Highlight_Node;
    public GameObject[] backgrounds;

    [Header("아래의 항목에다가 노트의 이미지를 넣으면 됩니다")]
    public Sprite Node_image_A;
    public Sprite Node_image_B;
    public Sprite Node_image_C;
    public Sprite Node_image_D;
    public Sprite ping_locate_shadow;

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

    public static int LineIndex = 0; //좀 느낌 없는데 급하니까 전역변수로 다른 소스코드에 접근 허용 [HACK]


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

                Instantiate(nodes_prefab, node_location[node_array].vector2, Quaternion.identity);
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
                node_management_drag nmd=newNode.GetComponent<node_management_drag>();
                nmd.ping = node_location[node_array + 1].vector2;
            }
            else if (node_location[node_array].mode == 3)
            {
                SpriteRenderer sr = nodes_prefab.GetComponent<SpriteRenderer>();
                SpriteRenderer rn = nodes_prefab.transform.Find("ring").GetComponent<SpriteRenderer>();
                sr.sprite = node_location[node_array].procedure;
                rn.sprite = node_location[node_array].Ring_Color;

                //nodes_prefab 내에 속해있는 ring object를 비활성화 한다
                nodes_prefab.transform.Find("ring").gameObject.SetActive(false);

                GameObject sha=Instantiate(nodes_prefab, node_location[node_array].vector2, Quaternion.identity);
                sha.name = "shadow";


                nodes_prefab.transform.Find("ring").gameObject.SetActive(true);
            }
            else
            {
                Instantiate(nodes_prefab, node_location[node_array].vector2, Quaternion.identity);
            }
        }
    }

    private void Insert_Line(List<Vector2> v)
    {
        List<Vector2> vector = new List<Vector2>(v);

        //list 안의 원소들을 Reverse 시킨다
        vector.Reverse();

        LineIndex = LineIndex + 1; //좀 느낌 없는데 급하니까 전역변수로 다른 소스코드에 접근 허용 [HACK]

        //line renderer에 좌표를 넣는다
        for (int i = 0; i < LineIndex; i++)
        {
            line_renderer.positionCount = LineIndex;
            line_renderer.SetPosition(i, vector[i]);
        }
    }

    public void Delete_Line() //아직 미사용
    {
        LineIndex = LineIndex - 1; //좀 느낌 없는데 급하니까 전역변수로 다른 소스코드에 접근 허용 [HACK]
        UnityEngine.Debug.Log("진행");
    }

    private float[] easyTime_01 = { 0f, 2f, 1.8f, 1.8f, 2f };
    private float[] easyTime_23 = { 0f, 1.8f, 2f, 2f, 2f };
    private float[] normalTime_01 = { 0f, 1.6f, 1.8f, 1.8f, 1.6f, 1.6f, 1.6f, 1.8f };
    private float[] normalTime_23 = { 0f , 1.8f , 1.8f , 1.6f , 1.6f , 1.8f , 1.6f , 1.6f };
    private float[] hardTime = { 0, 0f, 2.5f, 1.5f, 1.5f, 1.5f, 0f, 2.3f, 1.3f, 1.5f, 1.3f };
    private int cas;

    //노드를 생성하는 부분입니다. Coroutine으로 구현
    private IEnumerator D_Coroutine()
    {
        List<Vector2> vector = new List<Vector2>();

#if true
        for (int i=0; i<node_location.Count; i++)
        {
            vector.Add(node_location[i].vector2);
            if(node_location[i].mode == 1 )
            {
                yield return new WaitForSeconds(set_node_wait());
            }
            else if(node_location[i].mode == 2)
            {
                yield return new WaitForSeconds(1f);
            }
            node_placement(i);
            
            Insert_Line(vector);

            if(node_location[i].mode == 3)
            {
                yield return new WaitForSeconds(2f);
            }
        }
#else
        float[] non;
        if(cas-1<=0)
        {
            if (UniteData.Difficulty == 1)
            {
                non = easyTime_01;
            }
            else if (UniteData.Difficulty == 2)
            {
                non = normalTime_01;
            }
            else
            {
                non = hardTime;
            }
        }
        else
        {
            if(UniteData.Difficulty==1)
            {
                non = easyTime_23;
            }
            else if(UniteData.Difficulty==2)
            {
                non = normalTime_23;
            }
            else { non = hardTime; }
        }

        for(int i = 0; i < node_location.Count; i++)
        {
            vector.Add(node_location[i].vector2);
            if (node_location[i].mode == 1)
            {
                yield return new WaitForSeconds(non[i]);
                node_placement(i);
                Insert_Line(vector);
            }
            else if (node_location[i].mode == 2)
            {
                yield return new WaitForSeconds(non[i]);
                node_placement(i);
                Insert_Line(vector);
                i++;
                vector.Add(node_location[i].vector2);
                node_placement(i);
                Insert_Line(vector);
            }

            /*if (node_location[i].mode == 3)
            {
                yield return new WaitForSeconds(2f);
            }*/
        }
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        //Play 씬을 ScenePass를 통해 비동기적으로 로드한다
        sceneLoader = GetComponent<IScenePass>();
        sceneLoader.LoadSceneAsync("Play");

        //컷씬 초기화
        UniteData.Move_Progress = true;
        UnPassed = false;
        UniteData.Node_LifePoint = 2; //노드 목숨
        UniteData.Node_Click_Counter = 0; //노드 클릭 횟수
        LineIndex = 0;
        line_renderer.material.color = Color.white;

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
                //backgrounds[2].SetActive(true); // 아직 hard 모드가 안나온 상태
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
        line_renderer.positionCount = LineIndex; //좀 느낌 없는데 급하니까 전역변수로 다른 소스코드에 접근 허용 [HACK]

        //만약 컷씬을 클리어 했거나 / 기회포인트가 남아있는 상황에서 실패했을 때
        if ((UniteData.Node_LifePoint >= 0 && UniteData.Node_Click_Counter == node_location.Count) || UnPassed)
        {
            //쬐끔만 대기 [HACK]
            for(int x=0; x<500000000; x++)
            {
                //대기
            }
            //데이터 초기화
            UniteData.Node_LifePoint = 2;
            UniteData.Node_Click_Counter = 0;
            UnPassed = false;
            UniteData.GameMode = "Play";
            //클리어 애니메이션 실행
            sceneLoader.SceneLoadStart("Play");
        }


    }

    private void Initialize_node_setting()
    {
#if true
    cas = call_random() % 4;
#else
    cas = 1;
#endif
        switch (UniteData.Difficulty)
        {
            case 1: //easy
                if( cas == 0) {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[C], BR_Ring[C], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                }
                else if( cas == 1) {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[C], BR_Ring[C], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                }
                else if ( cas == 2) {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[C], BR_Ring[C], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                }
                else {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[C], BR_Ring[C], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                }
                break;

            case 2: //normal
                if (cas == 0) {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                }
                else if(cas == 1) {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                }
                else if (cas == 2) {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                }
                else {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                }
                break;

            case 3: //hard
                if (cas == 0) {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                }
                else if (cas == 1) {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));

                    /*node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[0], 2));
                    node_location.Add(new Node_data(Node_data.resettingShadowNode(node_location[node_location.Count-1].vector2,5), ping_locate_shadow, Ring[1], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[2], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[3], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[0], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[1], 2));
                    node_location.Add(new Node_data(Node_data.resettingShadowNode(node_location[node_location.Count - 1].vector2,2), ping_locate_shadow, Ring[3], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[2], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[0], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[1], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[2], 1));*/
                }
                else if (cas == 2) {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                }
                else {
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 2));
                    node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[A], BR_Ring[A], 3));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                    node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));
                }
                break;

            default: //default
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_A, Ring[A], BR_Ring[A], 1));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_C, Ring[C], BR_Ring[C], 2));
                node_location.Add(new Node_data(set_node_coordinate(), ping_locate_shadow, Ring[C], BR_Ring[C], 3));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_B, Ring[B], BR_Ring[B], 1));
                node_location.Add(new Node_data(set_node_coordinate(), Node_image_D, Ring[D], BR_Ring[D], 1));

                UniteData.Difficulty = 1;
                break;
        }
    }



    private int call_random()
    {
        System.Random r = new System.Random();
        return r.Next(-210000000, 210000000);
    }

    private Vector2 set_node_coordinate()
    {
        System.Random random = new System.Random(unchecked((int)((long)Thread.CurrentThread.ManagedThreadId + (DateTime.UtcNow.Ticks)) - call_random()));

        Vector2 vector = new Vector2(random.Next(-1300, 1301), random.Next(-500, 501));
        
        //노드끼리 일정 거리가 떨어지면 탈출
        while (check_radius_between_nodes(vector))
        {
            vector = new Vector2(random.Next(-1300, 1301), random.Next(-500, 501));
        }


        return vector;
    }

    private float set_node_wait()
    {
        System.Random random = new System.Random(unchecked((int)((long)Thread.CurrentThread.ManagedThreadId + (DateTime.UtcNow.Ticks)) - call_random()));

        switch(UniteData.Difficulty)
        {
            case 1:
                return 0.1f * random.Next(8, 14);
            case 2:
                return 0.1f * random.Next(6, 11);
            case 3:
                return 0.1f * random.Next(4, 9);
            default:
                return 0.1f * random.Next(8, 14);
        }
    }

    private bool check_radius_between_nodes(Vector2 vector)
    {
        int node_set_locate_count=node_location.Count;

        for (int i = node_set_locate_count < 4 ? 0 : node_set_locate_count - 4; i < node_set_locate_count; i++) 
        {
            Vector2 call_vec = node_location[i].vector2;

            if (Vector2.Distance(vector, call_vec) < radius_MIN)
            {
                return true;
            }
            else if (Vector2.Distance(vector, call_vec) > radius_MAX)
            {
                return true;
            }
        }
        return false;
    }


}