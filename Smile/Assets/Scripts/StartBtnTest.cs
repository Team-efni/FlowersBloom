using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtnTest : MonoBehaviour
{
    public IScenePass sceneLoader;
    // Start is called before the first frame update
    void Start()
    {
        /*
         * 로딩창 사용을 위해 미리 로드는 없앴습니당!
        //Scene을 비동기적으로 연결해 미리 로드
        sceneLoader=GetComponent<IScenePass>();
        sceneLoader.LoadSceneAsync("Play");
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBtn()
    {
        //SceneManager.LoadScene("Play");

        //게임 관련 전역변수 초기화
        UniteData.lifePoint = 3;
        UniteData.notePoint = 2;
        UniteData.Play_Scene_Time = 0f;
        UniteData.Player_Location_Past = Vector2.zero;

        //로드 불러오기
        //sceneLoader.SceneLoadStart("Play");

        LoadingSceneManager.LoadScene("Play");
    }
}
