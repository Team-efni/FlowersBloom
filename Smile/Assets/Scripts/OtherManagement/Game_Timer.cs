/*
*모든 게임 내 시간을 다루는 스크립트
*
*구현 목표
*-플레이 도중의 시간값을 기록
*-시간을 측정, 일시정지, 다시 재생 기능
*
*위 스크립트의 초기 버전은 김시훈이 작성하였음
*문의사항은 gkfldys1276@yandex.com으로 연락 바랍니다 (카톡도 가능).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game_Timer
{
    private float _startTime;
    private float _pausedTime;
    //private bool _isRunning = false;

    public Game_Timer(float backupTime)
    {
        _startTime = Time.time - backupTime;
    }

    public float GetTime()
    {
        return Time.time - _pausedTime - _startTime;
    }

    /*public void StartTimer()
    {
        _isRunning = true;
    }

    public void PauseTimer()
    {
        _pausedTime += Time.time - _startTime;
        _isRunning = false;
    }

    public void ResumeTimer()
    {
        _startTime = Time.time;
        _isRunning = true;
    }

    public void ResetTimer()
    {
        _startTime = Time.time;
        _pausedTime = 0f;
        _isRunning = false;
    }*/
}
