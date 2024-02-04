using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCommand : BaseCommand
{
    ISkill skill;

    public SkillCommand(ModelBase model) :base(model)
    {
        skill = model as ISkill;
    }

    public override void Do()
    {
        base.Do();
        List<ModelBase> results = skill.GetTarget();
        if(results.Count > 0)
        {
            //有目标
            GameApp.SkillMgr.AddSkill(skill, results);
        }
    }

    public override bool Update(float dt)
    {
        if(GameApp.SkillMgr.IsRuningSkill() == false)
        {
            model.IsStop = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}
