using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FightSelectHeroView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();
        Find<Button>("bottom/startBtn").onClick.AddListener(onFightBtn);
    }

    private void onFightBtn()
    {
        if(GameApp.FightWorldMgr.heros.Count == 0)
        {
            GameApp.ViewMgr.Open(ViewType.TipView, "请选择英雄");
        }
        else
        {
            GameApp.ViewMgr.Close(ViewId);
            GameApp.FightWorldMgr.ChangeState(GameState.Player);
        }
    }

    public override void Open(params object[] args)
    {
        base.Open(args);

        GameObject prefabObj = Find("bottom/grid/item");
        Transform gridTf = Find("bottom/grid").transform;

        for(int i = 0;i < GameApp.GameDataManager.heros.Count; i++)
        {
            Dictionary<string, string> data = GameApp.ConfigMgr.GetConfigDdata("player").GetDataById(GameApp.GameDataManager.heros[i]);
            GameObject obj = Object.Instantiate(prefabObj, gridTf);
            obj.SetActive(true);
            HeroItem item = obj.AddComponent<HeroItem>();
            item.Init(data);
        }
    }
}
