/*
*노드의 전체적인 구조를 담당 및 생성하는 스크립트
*
*구현 목표
*-노드의 일괄적인 생성 담당
*
*위 스크립트의 초기 버전은 김시훈이 작성하였음
*문의사항은 gkfldys1276@yandex.com으로 연락 바랍니다 (카톡도 가능).
*/
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Node_data
{
    public Vector2 vector2 = new Vector2();
    public Sprite procedure;

    public Node_data(Vector2 _vector2, Sprite _procedure)
    {
        this.vector2 = _vector2;

        //TODO: 이미지 적용 안될 때 예외처리 필요
        this.procedure = _procedure;
    }
}

public class node : MonoBehaviour
{
    private List<Node_data> node_location = new List<Node_data>();
    public GameObject nodes_prefab;

    [Header("아래의 항목에다가 노드의 이미지를 넣으면 됩니다")]
    public Sprite Node_image_A;
    public Sprite Node_image_B;
    public Sprite Node_image_C;
    public Sprite Node_image_D;

    //노드를 리스트의 순서에 따라 하나를 차례로 배치하는 함수
    void node_placement()
    {
        if (node_location.Count == 0)
        {
            return;
        }
        else
        {
            SpriteRenderer sr = nodes_prefab.GetComponent<SpriteRenderer>();
            sr.sprite = node_location[0].procedure;

            Instantiate(nodes_prefab, node_location[0].vector2, Quaternion.identity);

            node_location.RemoveAt(0);
        }
    }


    //노드를 생성하는 부분입니다. Coroutine으로 구현
    private IEnumerator D_Coroutine()
    {
        UnityEngine.Debug.Log("좌표 설정 완료 2초 대기...");

        yield return new WaitForSeconds(2.0f);
        node_placement();

        yield return new WaitForSeconds(0.8f);
        node_placement();

        yield return new WaitForSeconds(1.2f);
        node_placement();

        yield return new WaitForSeconds(0.5f);
        node_placement();
    }

    // Start is called before the first frame update
    void Start()
    {
        //노드가 배치될 위치를 지정해 저장한다
        node_location.Add(new Node_data(new Vector2(-980, -280), Node_image_A));
        node_location.Add(new Node_data(new Vector2(-540, 350), Node_image_B));
        node_location.Add(new Node_data(new Vector2(915, -290), Node_image_C));
        node_location.Add(new Node_data(new Vector2(440, 200), Node_image_D));

        StartCoroutine(D_Coroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }
}