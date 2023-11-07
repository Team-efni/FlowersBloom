using System.Collections;
using System.Collections.Generic;
using System;
using System.Timers;

public class ScriptTimer
{
    private bool isTimeEnd = false;
    private int timeLimit;
    private int elapsedTime = 0;
    private Timer timer;

    public ScriptTimer(int timeLimitMillisec)
    {
        this.timeLimit = timeLimitMillisec;
        this.timer = new Timer(timeLimit);
        timer.Elapsed += TimerElapsed;
        this.timer.Start();
    }
    ~ScriptTimer() { }

    private void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        elapsedTime += (int)timer.Interval;

        if (elapsedTime >= timeLimit)
        {
            timer.Stop();
            isTimeEnd = true;
        }
    }

    public bool getTimeEnd()
    {
        return isTimeEnd;
    }
}
