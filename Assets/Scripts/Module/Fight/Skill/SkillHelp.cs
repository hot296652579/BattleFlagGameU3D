using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillHelp 
{
    public static bool IsModelInSkillArea(this ISkill skill,ModelBase target)
    {
        ModelBase current = (ModelBase)skill;
        if(current.GetDis(target) <= skill.skillPro.AttackRange)
        {
            return true;
        }
        return false;
    }

    //获得技能作用目标
    public static List<ModelBase> GetTarget(this ISkill skill)
    {
        //0：以鼠标指向的目标为目标
        //1：在攻击范围内的所有目标
        //2：在攻击范围内的英雄目标
        switch (skill.skillPro.Target)
        {
            case 0:
                return GetTarget_0(skill);
            case 1:
                return GetTarget_1(skill);
            case 2:
                return GetTarget_2(skill);
        }
        return null;
    }

    public static List<ModelBase> GetTarget_0(ISkill skill)
    {
        List<ModelBase> results = new List<ModelBase>();
        Collider2D col = Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition);
        if(col != null)
        {
            ModelBase target = col.GetComponent<ModelBase>();
            if(target != null)
            {
                if(skill.skillPro.TargetType == target.Type)
                {
                    results.Add(target);
                }
            }
        }
        return results;
    }

    public static List<ModelBase> GetTarget_1(ISkill skill)
    {
        List<ModelBase> results = new List<ModelBase>();
        for(int i = 0; i < GameApp.FightWorldMgr.heros.Count; i++)
        {
            //找到技能范围内的目标
            if (skill.IsModelInSkillArea(GameApp.FightWorldMgr.heros[i]))
            {
                results.Add(GameApp.FightWorldMgr.heros[i]);
            }
        }

        for(int i = 0;i < GameApp.FightWorldMgr.enemys.Count; i++)
        {
            if (skill.IsModelInSkillArea(GameApp.FightWorldMgr.enemys[i]))
            {
                results.Add(GameApp.FightWorldMgr.enemys[i]);
            }
        }

        return results;
    }

    public static List<ModelBase> GetTarget_2(ISkill skill)
    {
        List<ModelBase> results = new List<ModelBase>();
        for (int i = 0; i < GameApp.FightWorldMgr.heros.Count; i++)
        {
            //找到技能范围内的目标
            if (skill.IsModelInSkillArea(GameApp.FightWorldMgr.heros[i]))
            {
                results.Add(GameApp.FightWorldMgr.heros[i]);
            }
        }

        return results;
    }
}
