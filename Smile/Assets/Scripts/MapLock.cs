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
        for (int i = 0; i < StageList.Count; i++)
        {
            if (UniteData.GameClear[i])
            {
                StageList[i].GetComponent<Stage>().UnlockMap();
            }
            else
            {
                StageList[i].GetComponent<Stage>().LockMap();
            }
        }
    }
}