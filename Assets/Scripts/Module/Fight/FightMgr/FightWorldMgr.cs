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
    public List<Hero> heros;//战斗单元集合
    public List<Enemy> enemys;//战斗敌人集合
    public int RoundCount;//回合数

    public FightUnitBase Current
    {
        get
        {
            return current;
        }
    }

    public FightWorldMgr()
    {
        heros = new List<Hero>();
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

    public void EnterFight()
    {
        RoundCount = 1;
        heros = new List<Hero>();
        enemys = new List<Enemy>();

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");//敌人添加Enemy标签
        Debug.Log("enemy:" + objects.Length);
        for(int i = 0; i < objects.Length; i++)
        {
            enemys.Add(objects[i].GetComponent<Enemy>());
        }
    }

    //添加英雄
    public void AddHero(Block b, Dictionary<string,string> data)
    {
        GameObject obj = Object.Instantiate(Resources.Load($"Model/{data["Model"]}")) as GameObject;
        Debug.Log("添加英雄 pos:", b);
        obj.transform.position = new Vector3(b.transform.position.x, b.transform.position.y, -1);
        Hero hero = obj.AddComponent<Hero>();
        hero.Init(data, b.RowIndex, b.ColIndex);
        //这个位置被占领了 设置方块为障碍物
        b.Type = BlockType.Obstacle;
        heros.Add(hero);
    }
}
