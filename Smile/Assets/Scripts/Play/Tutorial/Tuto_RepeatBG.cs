using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto_RepeatBG : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] float posValue;

    private Vector2 startPos;
    private float newPos;

    private bool gameClear;
    private static Game_Timer timer;

    private bool stop_game_once = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = new Game_Timer(UniteData.Play_Scene_Time);
        Initialized();
    }

    public void Initialized()
    {
        startPos = transform.position;

        gameClear = false;
        newPos = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameClear)
        {
            // 기본 상태
            if (UniteData.Move_Progress && !stop_game_once)
            {
                UniteData.Play_Scene_Time = timer.GetTime();
                newPos = Mathf.Repeat(timer.GetTime() * speed, posValue);
                
            }

            // 가이드로 인해 정지된 상태
            else if(!UniteData.Move_Progress && !stop_game_once)
            {
                //현재 시간 백업
                UniteData.Play_Scene_Time = timer.GetTime();

                stop_game_once = true;

            }

            // 정지되었다가 움직이게 되는 직후
            else if(UniteData.Move_Progress && stop_game_once)
            {
                timer = new Game_Timer(UniteData.Play_Scene_Time);
                stop_game_once = false;

            }
        }

        transform.position = (startPos + Vector2.left * newPos);
    }

    public void SetGameClearTrue()
    {
        gameClear = true;
    }
}
