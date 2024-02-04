using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMgr
{
    private GameTimer timer;

    private Queue<(ISkill skill, List<ModelBase> targets, System.Action callback)> skills;

    public SkillMgr()
    {
        timer = new GameTimer();
        skills = new Queue<(ISkill skill, List<ModelBase> targets, System.Action callback)>();
    }

    public void AddSkill(ISkill skill,List<ModelBase> targets = null,System.Action callback = null)
    {
        skills.Enqueue((skill, targets, callback));
    }

    public void UseSkill(ISkill skill,List<ModelBase> targets,System.Action callback)
    {
        ModelBase current = (ModelBase)skill;
        if(targets.Count > 0)
        {
            current.LookAtModel(targets[0]);
        }

        current.PlaySound(skill.skillPro.Sound);
        current.PlayAni(skill.skillPro.AniName);

        timer.Register(skill.skillPro.AttackTime, delegate ()
         {
            //技能作用个数
            int atkCount = skill.skillPro.AttackCount >= targets.Count ? targets.Count : skill.skillPro.AttackCount;
             for (int i = 0; i < atkCount; i++)
             {
                 targets[i].GetHit(skill);//受伤
            }
         });

        timer.Register(skill.skillPro.Time, delegate ()
         {
             current.PlayAni("idle");
             callback?.Invoke();
         });
    }

    public void Update(float dt)
    {
        timer.OnUpdate(dt);
        if(timer.Count() == 0 && skills.Count > 0)
        {
            var next = skills.Dequeue();
            if(next.targets != null)
            {
                UseSkill(next.skill, next.targets, next.callback);
            }
        }
    }

    //使用正在跑一个技能
    public bool IsRuningSkill()
    {
        if(timer.Count() == 0 && skills.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Clear()
    {
        timer.Break();
        skills.Clear();
    }
}
