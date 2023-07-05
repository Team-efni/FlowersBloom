using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLock : MonoBehaviour
{
    [SerializeField] List<GameObject> StageList;
    private Button btn;
    void Start()
    {
        /*    StageList[0].GetComponent<Stage>().LockMap();
              StageList[1].GetComponent<Stage>().LockMap();
              StageList[2].GetComponent<Stage>().LockMap();
        */

        // easy맵은 열려있게
        StageList[0].GetComponent<Stage>().UnlockMap();

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
        //StageList[2].GetComponent<Stage>().LockMap(); // 개발전이라 마지막 단계는 아직 안열리게
    }
}