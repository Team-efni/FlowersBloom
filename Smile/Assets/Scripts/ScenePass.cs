/*
*씬과 씬 간 비동기적으로 전환하는 스크립트
*
*구현 목표
*-씬 전환
*-미리 씬 로드
*
*위 스크립트의 초기 버전은 김시훈이 작성하였음
*문의사항은 gkfldys1276@yandex.com으로 연락 바랍니다 (카톡도 가능).
*/
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;


public interface IScenePass
{
    public void LoadSceneAsync(string sceneName);
    public void SceneLoadStart();
}


public class ScenePass : MonoBehaviour, IScenePass
{
    private AsyncOperation asyncLoad;
    private bool enable=false;
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }

    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single); //씬 불러오기
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                enable = true;
            }

            yield return null;
        }
    }

    public void SceneLoadStart()
    {
        while(!enable) 
        {
            
        }
        asyncLoad.allowSceneActivation = true;
        return;
    }
}
