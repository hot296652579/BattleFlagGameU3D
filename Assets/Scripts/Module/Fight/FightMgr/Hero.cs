using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : ModelBase,ISkill
{
    public SkillProperty skillPro { get ; set; }

    public void Init(Dictionary<string,string> data, int row,int col)
    {
        this.data = data;
        this.RowIndex = row;
        this.ColIndex = col;
        Id = int.Parse(this.data["Id"]);
        Type = int.Parse(this.data["Type"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;
        skillPro = new SkillProperty(int.Parse(this.data["Skill"]));
    }

    protected override void OnSelectCallBack(object arg)
    {
        if(GameApp.FightWorldMgr.state == GameState.Player)
        {
            if(GameApp.CommandMgr.isRunningCommand == true)
            {
                return;
            }

            GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);

            if(IsStop == false)
            {
                //显示路径
                GameApp.MapMgr.ShowStepGrid(this, Step);
                //添加显示路径指令
                GameApp.CommandMgr.AddComand(new ShowPathCommand(this));

                addOptionEvents();
            }
            
            GameApp.ViewMgr.Open(ViewType.HeroDesView, this);
        }
    }

    private void addOptionEvents()
    {
        GameApp.MessageCenter.AddTempEvent(Defines.OnAttackEvent, onAttackCallBack);
        GameApp.MessageCenter.AddTempEvent(Defines.OnIdleEvent, onIdleCallBack);
        GameApp.MessageCenter.AddTempEvent(Defines.OnCancelEvent, onCanCelCallBack);
    }

    //攻击
    private void onAttackCallBack(System.Object arg)
    {
        GameApp.CommandMgr.AddComand(new ShowSkillAreaCommand(this));
    } 
    //待机
    private void onIdleCallBack(System.Object arg)
    {
        IsStop = true;
    }
    //取消移动
    private void onCanCelCallBack(System.Object arg)
    {
        GameApp.CommandMgr.UnDo();
    }

    protected override void OnUnSelectCallBack(object arg)
    {
        base.OnUnSelectCallBack(arg);
        GameApp.ViewMgr.Close((int)ViewType.HeroDesView);
    }

    public void ShowSkillArea()
    {
        GameApp.MapMgr.ShowAttackStep(this, skillPro.AttackRange, Color.red);
    }

    public void HideSkillArea()
    {
        GameApp.MapMgr.HideAttackStep(this, skillPro.AttackRange);
    }
}
