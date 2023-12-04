using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLock : MonoBehaviour
{
    [SerializeField] List<GameObject> StageList;
    [SerializeField] Sprite ping2;
    private Button btn;
    void Start()
    {
        for (int i = 0; i < StageList.Count - 1; i++)
        {
            if (UniteData.GameClear[i]==1)
            {
                StageList[i+1].GetComponent<Stage>().UnlockMap();
            }
            else
            {
                StageList[i+1].GetComponent<Stage>().LockMap();
            }
        }
        // easy맵은 열려있게
        StageList[0].GetComponent<Stage>().UnlockMap();

        //모든 맵 클리어 시 첫번째 맵 핑 색상 변경
        if(UniteData.GameClear[StageList.Count - 1] == 1)
        {
            GameObject ping = StageList[0].transform.GetChild(0).gameObject;
            ping.GetComponent<Image>().sprite = ping2;
        }
    }
}