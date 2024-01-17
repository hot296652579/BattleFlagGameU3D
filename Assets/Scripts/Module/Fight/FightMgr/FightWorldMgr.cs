using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Idle,
    Enter
}

//战斗管理器 （管理战斗相关实体 敌人英雄地图格子等）
public class FightWorldMgr 
{
    public GameState state = GameState.Idle;
    private FightUnitBase current;//处于战斗单元

    public FightUnitBase Current
    {
        get
        {
            return current;
        }
    }

    public FightWorldMgr()
    {
        ChangeState(GameState.Idle);
    }

    // Update is called once per frame
    public void Update(float dt)
    {
        if (current != null && current.Update(dt) == true)
        {

        }
        else
        {
            current = null;
        }
    }

    public void ChangeState(GameState state)
    {
        FightUnitBase _current = current;
        this.state = state;
        switch (this.state)
        {
            case GameState.Idle:
                _current = new FightIdle();
                break;
            case GameState.Enter:
                _current = new FightEnter();
                break;
        }

        _current.Init();
    }
}
