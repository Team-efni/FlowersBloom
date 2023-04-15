/*
*노드 전체적인 관리를 담당하는 스크립트
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

public class node_management : MonoBehaviour
{
    public GameObject node_prefab;

    public UnityEvent<GameObject> onClick;
    public Animator animator;
    public node_delete nd;

    [Header("노드 판정의 타이밍을 조절하는 섹터 \n(변수에 마우스 가져다 대보세요)")]
    [Tooltip("조금 미흡한 타이밍의 시작점 (sec 단위)")]
    public float ENTRANCE_UNSATISFACTORY_TOUCH = 1.65f;
    [Tooltip("성공적인 타이밍의 시작점 (sec 단위)")]
    public float ENTRANCE_SUCCESSFULL_TOUCH = 1.85f;
    [Tooltip("성공적인 타이밍의 종점 (sec 단위)")]
    public float EXIT_SUCCESSFULL_TOUCH = 2.05f;
    [Tooltip("조금 미흡한 부분의 종점 (sec 단위)")]
    public float EXIT_UNSATISFACTORY_TOUCH = 2.15f;


    public void node_click_event(GameObject clickObject)
    {
        //UnityEngine.Debug.Log("노드 클릭: " + clickObject.name);
        node_click_timing();
        
        nd.delete_node_after_click();
    }

    //클릭 타이밍 판정 함수
    public void node_click_timing()
    {
        // 애니메이션 클립의 경과 시간을 얻습니다.
        float elapsedTime = call_animation_time();

        //Debug.Log("클릭 시간: " + elapsedTime + "s");

        // 클릭 시간이 조금 미흡한 타이밍의 시작점보다 빠를 때 [MISS]
        if (elapsedTime < ENTRANCE_UNSATISFACTORY_TOUCH)
        {
            Debug.Log("MISS");
        }
        // 클릭 시간이 조금 미흡한 타이밍의 시작점보다 느릴 때 [FAST]
        else if (elapsedTime > ENTRANCE_UNSATISFACTORY_TOUCH && elapsedTime < ENTRANCE_SUCCESSFULL_TOUCH)
        {
            Debug.Log("FAST");
        }
        // 클릭 시간이 성공적인 타이밍의 시작점보다 빠를 때 [SUCCESS]
        else if (elapsedTime > ENTRANCE_SUCCESSFULL_TOUCH && elapsedTime < EXIT_SUCCESSFULL_TOUCH)
        {
            Debug.Log("SUCCESS");
        }
        // 클릭 시간이 성공적인 타이밍의 종점보다 느릴 때 [SLOW]
        else if (elapsedTime > EXIT_SUCCESSFULL_TOUCH && elapsedTime < EXIT_UNSATISFACTORY_TOUCH)
        {
            Debug.Log("SLOW");
        }
        // 클릭 시간이 조금 미흡한 타이밍의 종점보다 느릴 때 [MISS]
        else if (elapsedTime > EXIT_UNSATISFACTORY_TOUCH)
        {
            Debug.Log("MISS");
        }
    }

    //애니메이션의 실행 이후의 시간을 확인하는 함수
    public float call_animation_time()
    {
        // 현재 애니메이션 상태를 가져옵니다.
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        return stateInfo.normalizedTime * stateInfo.length;
    }


    void Update()
    {
        // 마우스 왼쪽 버튼이 클릭되었을 때
        if (Input.GetMouseButtonDown(0))
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
    }
}
