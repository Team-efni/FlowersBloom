using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tuto_nodeControl : Singleton<tuto_nodeControl>
{
    public void turnOnNodeManager(GameObject nodeManager)
    {
        Time.timeScale = 1.0f;
        nodeManager.SetActive(true);
    }
}
