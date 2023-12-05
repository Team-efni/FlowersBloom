using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto_RestartGame : MonoBehaviour
{
    private Tuto_GameManager t_gm; // GameClear
    private Tuto_NoteController t_nc;
    private Tuto_PlayerController t_pc;
    private Tuto_RepeatBG t_rb;
    private Tuto_MonsterManager t_mm; // RepeatMonster
    private BtnStop s_bs;


    // Start is called before the first frame update
    void Start()
    {
        t_gm = FindAnyObjectByType<Tuto_GameManager>();
        t_nc = FindAnyObjectByType<Tuto_NoteController>();
        t_pc = FindObjectOfType<Tuto_PlayerController>();
        t_rb = FindObjectOfType<Tuto_RepeatBG>();
        t_mm = FindAnyObjectByType<Tuto_MonsterManager>();
        s_bs = FindObjectOfType<BtnStop>();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public void GameReSettings()
    {
        if (UniteData.ReStart)
        {
            Debug.Log("GameSetting");
            UniteData.Difficulty = 0; // 추후에 맵에서 선택하면 이거 나오게 설정

            t_gm.Initialized();
            t_nc.Initialized();
            t_pc.Initialized();
            s_bs.Initialized();
            t_mm.Initialized();

            UniteData.ReStart = false;
        }

        t_rb.Initialized(); // 컷씬에서 돌아오는 경우에도 호출
    }
}
