using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PlayerController : MonoBehaviour
{
<<<<<<< HEAD
    //[SerializeField] private int moveSpeed;
=======
    [SerializeField] private int moveSpeed;
    public GameObject Cut_Scene_prefab;
>>>>>>> CutBackup

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


    public static Vector3 CamabsolutePosition = new Vector3(0, 0, 0);


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            // 몬스터에 닿으면 멈추고 RN 씬 호출
            Debug.Log("Go to RN");
            
            //moveSpeed = 0;

<<<<<<< HEAD
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

            
=======
            StartCoroutine(LoadCutScene());
>>>>>>> CutBackup
        }
    }

    private IEnumerator LoadCutScene()
    {
        //게임 오브젝트 중 UI_Touch Tag를 SetActive(false)로 설정한다
        GameObject[] UI_Touch = GameObject.FindGameObjectsWithTag("UI_Touch");
        foreach (GameObject UI in UI_Touch)
        {
            UI.SetActive(false);
        }

        GameObject Player = GameObject.Find("Player");
        GameObject Cam = GameObject.Find("Player/Main Camera");

        //카메라의 절대좌표를 가져온다
        CamabsolutePosition = Player.transform.TransformPoint(Cam.transform.localPosition + new Vector3(0, 0, 10));

        //애니메이션 주기

        //컷씬 판 만들기
        //Instantiate(Cut_Scene_prefab, CamabsolutePosition, Quaternion.identity);
        Cut_Scene_prefab.transform.position = CamabsolutePosition;

        //애니메이션 시작
        Animator anim = Cut_Scene_prefab.GetComponent<Animator>(); 
        anim.SetBool("IsStart", true);

        //컷씬 애니메이션이 끝나면 씬 바로 이동
        yield return new WaitForSeconds(1.15f);
        scenePass.SceneLoadStart();
    }
}
