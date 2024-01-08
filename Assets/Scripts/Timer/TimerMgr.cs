using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerMgr 
{
    GameTimer timer;

    public TimerMgr()
    {
        timer = new GameTimer();
    }

    public void Register(float time,System.Action callback)
    {
        timer.Register(time, callback);
    }

    public void OnUpdate(float dt)
    {
        timer.OnUpdate(dt);
    }
}
