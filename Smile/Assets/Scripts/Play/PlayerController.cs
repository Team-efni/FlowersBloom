using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private int moveSpeed;

    public IScenePass scenePass;

    // 기회 포인트 관련
    public int notePoint = 2; // 기회 포인트
    public GameObject[] go_notePoints; // 기회 포인트 오브젝트

    // 목숨 포인트 관련
    public int lifePoint = 3; // 목숨 포인트

    // Start is called before the first frame update
    void Start()
    {
        scenePass = GetComponent<IScenePass>();
        scenePass.LoadSceneAsync("InGame-RN");
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + (transform.right * moveSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            // 몬스터에 닿으면 멈추고 RN 씬 호출
            Debug.Log("Go to RN");
            
            //moveSpeed = 0;

            // 기회가 남아있다면 감소하고 씬 이동
            if (notePoint > 0)
            {
                notePoint--;
                go_notePoints[notePoint].gameObject.SetActive(false);

                //씬 바로 이동
                scenePass.SceneLoadStart();
            }

            // 기회가 0이라면 목숨 포인트 감소
            else if (notePoint == 0)
            {
                // 목숨 포인트가 남아있다면 감소
                if(lifePoint > 0)
                {
                    lifePoint--;
                }
                // 남아있지 않다면 게임 오버    
                else if(lifePoint == 0)
                {
                    Debug.Log("GameOver");
                }
            }
                

            //애니메이션 주기

            
        }
    }
}
