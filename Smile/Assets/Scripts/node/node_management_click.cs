/*
*노드 전체적인 관리를 담당하는 스크립트 (클릭형 노드)
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
using UnityEngine.SceneManagement;

public class node_management_click : MonoBehaviour
{
    public GameObject node_prefab;
    public GameObject ring;

    public UnityEvent<GameObject> onClick;
    public Animator animator;
    public Animator drag;
    //public node_delete nd;

    public static float EASY_FPS = 120f;
    public static float NORMAL_FPS = 96f;
    public static float HARD_FPS = 78f;


    public void node_click_event(GameObject clickObject)
    {
        //클릭 효과음을 부여합니다
        AudioSource se = GameObject.Find("ClickEffect").GetComponent<AudioSource>();
        se.volume = UniteData.Effect;
        se.Play();

        node_click_timing();
        
        delete_node_after_click();
    }

    //클릭 타이밍 판정 함수
    public void node_click_timing()
    {
        // 클릭 시간이 조금 미흡한 타이밍의 시작점보다 빠를 때 [MISS]
        if (ring.transform.localScale.x>0.25f)//(elapsedTime < ENTRANCE_UNSATISFACTORY_TOUCH)
        {
            UniteData.Node_LifePoint -= 1;
            if (Node_Result.Miss_Node_Click())
            {
                node.UnPassed = true;
                UniteData.lifePoint--;
            }
        }
        // 클릭 시간이 조금 미흡한 타이밍의 시작점보다 느릴 때 [FAST]
        else if (ring.transform.localScale.x <= 0.25f && ring.transform.localScale.x > 0.21f)
        {
            Debug.Log("FAST");
        }
        // 클릭 시간이 성공적인 타이밍 [SUCCESS]
        else if (ring.transform.localScale.x <= 0.21f && ring.transform.localScale.x > 0.16f)
        {
            Debug.Log("SUCCESS");
        }
        // 클릭 시간이 성공적인 타이밍의 종점보다 느릴 때 [SLOW]
        else if (ring.transform.localScale.x <= 0.16f && ring.transform.localScale.x > 0.14f)
        {
            Debug.Log("SLOW");
        }
        // 클릭 시간이 조금 미흡한 타이밍의 종점보다 느릴 때 [MISS]
        else if (ring.transform.localScale.x <= 0.14f)
        {
            UniteData.Node_LifePoint -= 1;
            if (Node_Result.Miss_Node_Click())
            {
                node.UnPassed = true;
                UniteData.lifePoint--;
            }
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
    }

    private void Start()
    {
        
    }

    void Update()
    {
        // 게임 진행 도중 마우스 왼쪽 버튼이 클릭되었을 때
        if (Input.GetMouseButtonDown(0) && UniteData.Move_Progress==true)
        {
            // 마우스 위치를 화면 공간에서 월드 공간으로 변환합니다.
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 마우스 위치에서 Ray를 생성합니다.
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Ray가 노드와 충돌했는지 확인
            if (hit.collider != null)
            {
                // 노드가 클릭되었을 때
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    // 노드 클릭 이벤트 실행
                    onClick.Invoke(gameObject);
                }
            }
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

    public void delete_node_after_click()
    {
        //노드의 Prefab을 제거합니다.
        Destroy(node_prefab);

        node.LineIndex = node.LineIndex - 1; //좀 느낌 없는데 급하니까 전역변수로 다른 소스코드에 접근 허용 [HACK]

        UniteData.Node_Click_Counter += 1;
    }
}
