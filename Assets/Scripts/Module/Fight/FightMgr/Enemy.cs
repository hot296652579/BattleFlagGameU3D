using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : ModelBase,ISkill
{
    public int newSortingOrder = 99;

    public SkillProperty skillPro { get; set; }
    private Slider hpSlider;

    protected override void Start()
    {
        base.Start();

        hpSlider = transform.Find("hp/bg").GetComponent<Slider>();
        data = GameApp.ConfigMgr.GetConfigDdata("enemy").GetDataById(Id);

        Type = int.Parse(this.data["Type"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;
        skillPro = new SkillProperty(int.Parse(data["Skill"]));
    }

    protected override void OnSelectCallBack(object arg)
    {
        if(GameApp.CommandMgr.isRunningCommand == true)
        {
            return;
        }

        base.OnSelectCallBack(arg);
        GameApp.ViewMgr.Open(ViewType.EnemyDesView, this);
    }

    protected override void OnUnSelectCallBack(object arg)
    {
        base.OnUnSelectCallBack(arg);
        GameApp.ViewMgr.Close((int)ViewType.EnemyDesView);
    }

    public void ShowSkillArea()
    {
        throw new System.NotImplementedException();
    }

    public void HideSkillArea()
    {
        throw new System.NotImplementedException();
    }

    public override void GetHit(ISkill skill)
    {
        GameApp.SoundManager.PlayEffect("hit", transform.position);

        CurHp -= skill.skillPro.Attack;

        GameApp.ViewMgr.ShowHitNum($"-{skill.skillPro.Attack}", Color.red, transform.position);

        PlayEffect(skill.skillPro.AttackEffect);

        //是否死亡
        if(CurHp <= 0)
        {
            CurHp = 0;
            PlayAni("die");
            Destroy(gameObject, 1.2f);
            GameApp.FightWorldMgr.RemoveEnemy(this);
        }

        StopAllCoroutines();
        StartCoroutine(ChangeColor());
        StartCoroutine(UpdateHpSlider());
    }

    private IEnumerator ChangeColor()
    {
        bodySp.material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(0.25f);
        bodySp.material.SetFloat("_FlashAmount", 0);
    }

    private IEnumerator UpdateHpSlider()
    {
        hpSlider.gameObject.SetActive(true);
        hpSlider.DOValue((float)CurHp / (float)MaxHp, 0.25f);
        yield return new WaitForSeconds(0.75f);
        hpSlider.gameObject.SetActive(false);
    }
}
