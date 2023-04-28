using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    public GameObject Cut_Scene_prefab;

    public IScenePass scenePass;
    // Start is called before the first frame update
    void Start()
    {
        scenePass = GetComponent<IScenePass>();
        scenePass.LoadSceneAsync("InGame-RN");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (transform.right * moveSpeed * Time.deltaTime);
    }


    public static Vector3 CamabsolutePosition = new Vector3(0, 0, 0);


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            // 몬스터에 닿으면 멈추고 RN 씬 호출
            moveSpeed = 0;
            Debug.Log("Go to RN");

            StartCoroutine(LoadCutScene());
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
