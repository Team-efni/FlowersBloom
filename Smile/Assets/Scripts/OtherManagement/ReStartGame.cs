using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 재시작 시 초기 상태로 되돌리는 스크립트
public class ReStartGame : MonoBehaviour
{
    private GameClear s_gc;
    private NoteController s_nc;
    private PlayerController s_pc;
    private RepeatBG s_sb;
    private RepeatMonster s_rm;

    // Start is called before the first frame update
    void Start()
    {
        s_gc= FindObjectOfType<GameClear>();
        s_nc = FindObjectOfType<NoteController>();
        s_pc = FindObjectOfType<PlayerController>();
        s_sb = FindObjectOfType<RepeatBG>();
        s_rm = FindObjectOfType<RepeatMonster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public void GameReSettings()
    {
        s_gc.Initialized();
        s_nc.Initialized();
        s_pc.Initialized();
        s_sb.Initialized();
        s_rm.Initialized();
    }
}
