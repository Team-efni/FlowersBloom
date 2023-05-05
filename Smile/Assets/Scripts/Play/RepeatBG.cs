using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBG : MonoBehaviour
{
    [SerializeField]float speed;

    [SerializeField] float posValue;

    private Vector2 startPos;
    private float newPos;

    private bool gameClear;
    private static Game_Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        //6초 전 과거 위치로 회기 (이동 전 애니메이션 2초 + 이동 4초)
        timer = new Game_Timer(UniteData.Play_Scene_Time < 6f ? UniteData.Play_Scene_Time : UniteData.Play_Scene_Time - 6f);

        Initialized();
    }

    public void Initialized()
    {
        startPos = transform.position;
        //Debug.Log("repeatBG_I : " + transform.position.x);
        //transform.position = GameObject.Find("OriginPos").transform.position;

        gameClear = false;
        newPos = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameClear)
        {
            if(UniteData.Move_Progress)
            {
                newPos = Mathf.Repeat(timer.GetTime() * speed, posValue);
            }
            else
            {
                //현재 시간 백업
                UniteData.Play_Scene_Time = timer.GetTime();
            }
        }
        transform.position = (startPos + Vector2.left * newPos);
    }

    public void SetGameClearTrue()
    {
        gameClear = true;
    }
}
