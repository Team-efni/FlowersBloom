/*
*노드 전체적인 관리를 담당하는 스크립트 (드래그형 노드)
*
*구현 목표
*-노드의 이벤트 관리
*-노드의 클릭 판정
*
*위 스크립트의 초기 버전은 김시훈이 작성하였음
*문의사항은 gkfldys1276@yandex.com으로 연락 바랍니다 (카톡도 가능).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class node_management_drag : MonoBehaviour
{
    public GameObject node_prefab;
    public GameObject ring;
    public GameObject bright; 
    private GameObject shadow;
    private CircleCollider2D collider;

    public Vector2 ping;

    public UnityEvent<GameObject> onClick;
    public Animator animator;

    private float Drag_fast = 16f;

    private bool move_unlock = false;

    public void node_drag_event(GameObject clickObject)
    {
        shadow = GameObject.Find("shadow");

        //클릭 효과음을 부여합니다
        AudioSource se = GameObject.Find("ClickEffect").GetComponent<AudioSource>();
        se.volume = UniteData.Effect;
        se.Play();

        node_drag_timing();

        //delete_node_after_click();
    }

    //드래그 타이밍 판정 함수
    public void node_drag_timing()
    {
        //드래그 전 클릭 시간이 조금 미흡한 타이밍의 시작점보다 빠를 때 [MISS]
        if (ring.transform.localScale.x > 0.25f)//(elapsedTime < ENTRANCE_UNSATISFACTORY_TOUCH)
        {
            UniteData.Node_LifePoint -= 1;
            if (Node_Result.Miss_Node_Click())
            {
                node.UnPassed = true;
                UniteData.lifePoint--;
            }
            delete_node_after_click();
        }
        //드래그 전 클릭 시간이 조금 미흡한 타이밍의 시작점보다 느릴 때 [FAST]
        else if (ring.transform.localScale.x <= 0.25f && ring.transform.localScale.x > 0.21f)
        {
            //ring 오브젝트 비활성화
            ring.SetActive(false);
            //bright 활성화
            bright.SetActive(true);
            move_unlock = true;
            Debug.Log("FAST");
        }
        //드래그 전 클릭 시간이 성공적인 타이밍 [SUCCESS]
        else if (ring.transform.localScale.x <= 0.21f && ring.transform.localScale.x > 0.16f)
        {
            //ring 오브젝트 비활성화
            ring.SetActive(false);
            //bright 활성화
            bright.SetActive(true);
            move_unlock = true;
            Debug.Log("SUCCESS");
        }
        //드래그 전 클릭 시간이 성공적인 타이밍의 종점보다 느릴 때 [SLOW]
        else if (ring.transform.localScale.x <= 0.16f && ring.transform.localScale.x > 0.14f)
        {
            //ring 오브젝트 비활성화
            ring.SetActive(false);
            //bright 활성화
            bright.SetActive(true);
            move_unlock = true;
            Debug.Log("SLOW");
        }
        //드래그 전 클릭 시간이 조금 미흡한 타이밍의 종점보다 느릴 때 [MISS]
        else if (ring.transform.localScale.x <= 0.14f)
        {
            UniteData.Node_LifePoint -= 1;
            if (Node_Result.Miss_Node_Click())
            {
                node.UnPassed = true;
                UniteData.lifePoint--;
            }
            delete_node_after_click();
        }
    }

    //애니메이션의 실행 이후의 시간을 확인하는 함수
    public float call_animation_time()
    {
        // 현재 애니메이션 상태를 가져옵니다.
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        return stateInfo.normalizedTime * stateInfo.length;
    }

    private void Awake()
    {
        animator.SetInteger("Difficulty", UniteData.Difficulty);

        switch (UniteData.Difficulty)
        {
            case 4:
                animator.SetInteger("Difficulty", 1);
                break;
        }

        //bright 비활성화
        bright.SetActive(false);
    }

    private void Start()
    {
        //ping의 위치를 nodes_prefab의 위치에서 500거리만큼 떨어진 곳에 배치한다.
        //ping.transform.position = new Vector3(node_prefab.transform.position.x, node_prefab.transform.position.y + 500, node_prefab.transform.position.z);
        //Debug.Log(ping);

        collider= node_prefab.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        Vector2 mousePosition = new Vector2(10000, 10000);
        // 게임 진행 도중 마우스 왼쪽 버튼이 클릭되었을 때
        if (Input.GetMouseButtonDown(0) && UniteData.Move_Progress == true)
        {
            // 마우스 위치를 화면 공간에서 월드 공간으로 변환합니다.
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 마우스 위치에서 Ray를 생성합니다.
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Ray가 노드와 충돌했는지 확인
            if (hit.collider != null)
            {
                // 노드가 클릭되었을 때
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    collider.radius = 2.0f;
                    // 노드 클릭 이벤트 실행
                    onClick.Invoke(gameObject);
                }
            }
        }

        //노드 클릭 성공 시 노드 이동 활성화
        if(Input.GetMouseButton(0) && UniteData.Move_Progress == true && move_unlock==true)
        {
            // 마우스 위치를 화면 공간에서 월드 공간으로 변환합니다.
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 마우스 위치에서 Ray를 생성합니다.
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Ray가 노드와 충돌했는지 확인
            if (hit.collider != null)
            {
                // 노드가 클릭되었을 때
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    // 노드 클릭 이벤트 실행
                    //Debug.Log(mousePosition+" 호버중");
                }
            }
            else
            {
                // 노드 클릭 이벤트 실행
                Debug.Log("떨어저서 실패");
                UniteData.Node_LifePoint -= 1;
                if (Node_Result.Miss_Node_Click())
                {
                    node.UnPassed = true;
                    UniteData.lifePoint--;
                }
                delete_node_after_click();
            }
        }

        //노드 드래그 도중 먼저 손을 떼버렸을 때 처리
        if(Input.GetMouseButtonUp(0) && UniteData.Move_Progress == true && move_unlock == true)
        {
            // 노드 클릭 이벤트 실행
            Debug.Log("실패");
            delete_node_after_click();
        }


        if (ring.transform.localScale.x <= 0.135f)
        {
            //목숨 감소
            UniteData.Node_LifePoint -= 1;

            //게임실패처리 -> 기회 포인트1 감소(PlyaerController에서 처리했음) / 캐릭터 목숨 포인트 -1
            if (Node_Result.Miss_Node_Click())
            {
                node.UnPassed = true;
                UniteData.lifePoint--;
            }

            // 노드의 Prefab을 제거합니다.
            delete_node_after_click();
        }
    }

    private void FixedUpdate()
    {
        if(move_unlock)
        {
            //노드가 100frame 동안 node_prefab이 vector2 variation인 ping 변수로 이동 Like Linearity
            Vector2 v2 = Vector2.MoveTowards(node_prefab.transform.position, ping, Drag_fast);
            node_prefab.transform.position = v2;

            if (node_prefab.transform.position.x == ping.x)
            {
                if (node_prefab.transform.position.y == ping.y)
                {
                    //노드가 ping에 도달하면 노드 이동을 멈춘다.
                    move_unlock = false;
                    Debug.Log("성공");
                    delete_node_after_click();
                }
            }
        }
    }

    public void delete_node_after_click()
    {
        shadow = GameObject.Find("shadow");
        //shadow.transform.parent = node_prefab.transform;
        shadow.transform.SetParent(node_prefab.transform, false);

        //노드의 Prefab을 제거합니다.
        Destroy(node_prefab);

        node.LineIndex = node.LineIndex - 2; //좀 느낌 없는데 급하니까 전역변수로 다른 소스코드에 접근 허용 [HACK]

        UniteData.Node_Click_Counter += 2;
    }
}
