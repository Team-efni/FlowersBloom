using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private int moveSpeed;

    public IScenePass scenePass;
    public GameObject Cut_Scene_prefab;
    public NoteController s_noteController;

    // 기회 포인트 관련
    //public int notePoint = 2; // 기회 포인트 -> Data/UniteData.cs로 이동
    public GameObject[] go_notePoints; // 기회 포인트 오브젝트

    // 목숨 포인트 관련
    //public int lifePoint = 3; // 목숨 포인트 -> Data/UniteData.cs로 이동

    // Start is called before the first frame update
    void Start()
    {
        scenePass = GetComponent<IScenePass>();
        scenePass.LoadSceneAsync("InGame-RN");
        UniteData.Move_Progress = true;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + (transform.right * moveSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!s_noteController.noteSuccess && collision.CompareTag("Monster"))
        {
            // 몬스터에 닿으면 움직임을 멈춤
            UniteData.Move_Progress = false;

            

            //moveSpeed = 0;

            // 기회가 남아있다면 감소하고 씬 이동
            if (UniteData.notePoint > 0)
            {
                UniteData.notePoint--;
                go_notePoints[UniteData.notePoint].gameObject.SetActive(false);

                //씬 바로 이동
                //scenePass.SceneLoadStart();

                //씬 애니메이션을 본 뒤 이동
                StartCoroutine(LoadCutScene());
            }

            // 기회가 0이라면 목숨 포인트 감소
            else if (UniteData.notePoint == 0)
            {
                // 목숨 포인트가 남아있다면 감소
                if(UniteData.lifePoint > 0)
                {
                    UniteData.lifePoint--;
                }
                // 남아있지 않다면 게임 오버    
                else if(UniteData.lifePoint == 0)
                {
                    Make_Invisible_UI();

                    Animator fadeAnimator = GameObject.Find("FadeOut").GetComponent<Animator>();
                    // 페이드 아웃 애니메이션 이후 게임 오버 씬을 전환합니다.
                    fadeAnimator.SetBool("IsStartFade", true);
                }
            }
        }
    }


    public static Vector3 CamabsolutePosition = new Vector3(0, 0, 0);
    private IEnumerator LoadCutScene()
    {
        Make_Invisible_UI();

        GameObject Cam = GameObject.Find("Main Camera");

        //카메라의 절대좌표를 가져온다
        CamabsolutePosition = Cam.transform.localPosition + new Vector3(0, 0, 10);

        //애니메이션 주기

        //컷씬 판 만들기
        //Instantiate(Cut_Scene_prefab, CamabsolutePosition, Quaternion.identity);
        Cut_Scene_prefab.transform.position = CamabsolutePosition;

        //애니메이션 시작
        Animator anim = Cut_Scene_prefab.GetComponent<Animator>();
        anim.SetBool("IsStart", true);

        //컷씬 애니메이션이 끝나면 씬 바로 이동
        yield return new WaitForSeconds(1.17f);
        scenePass.SceneLoadStart();
    }

    private void Make_Invisible_UI()
    {
        //게임 오브젝트 중 UI_Touch Tag를 SetActive(false)로 설정한다
        GameObject[] UI_Touch = GameObject.FindGameObjectsWithTag("PlayScene_UI");
        foreach (GameObject UI in UI_Touch)
        {
            UI.SetActive(false);
        }

        GameObject Cam = GameObject.Find("Main Camera");
    }
}
