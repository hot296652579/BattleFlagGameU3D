using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightOptionDesView : BaseView
{
    protected override void OnStart()
    {
        base.OnStart();
        Find<Button>("bg/turnBtn").onClick.AddListener(onChangeEnemyTurnBtn);
        Find<Button>("bg/gameOverBtn").onClick.AddListener(onGameOverBtn);
        Find<Button>("bg/cancelBtn").onClick.AddListener(onCancelBtn);
    }

    private void onGameOverBtn()
    {
        GameApp.ViewMgr.Close((int)ViewType.FightOptionDesView);
    }

    private void onChangeEnemyTurnBtn()
    {
        GameApp.ViewMgr.Close((int)ViewType.FightOptionDesView);
    }

    private void onCancelBtn()
    {
        GameApp.ViewMgr.Close((int)ViewType.FightOptionDesView);
    }
}
