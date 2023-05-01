using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    public GameObject bgGroup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearGame()
    {
        for (int i = 0; i < bgGroup.transform.childCount; i++)
        {
            bgGroup.transform.GetChild(i).GetComponent<RepeatBG>().setGameClearTrue();
        }
    }
}
