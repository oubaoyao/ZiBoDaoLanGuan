using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;

public class ZiBoTask : BaseTask
{
    public ZiBoTask(BaseState state) : base(state)
    {
    }

    public override void Enter()
    {
        base.Enter();
        UIManager.CreatePanel<ZiBoPanel>(WindowTypeEnum.ForegroundScreen);
    }

    public override void Exit()
    {
        base.Exit();
        UIManager.ChangePanelState<ZiBoPanel>(WindowTypeEnum.ForegroundScreen, UIPanelStateEnum.Hide);
    }
}
