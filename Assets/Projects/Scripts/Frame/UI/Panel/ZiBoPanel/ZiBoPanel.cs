using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;

public class ZiBoPanel : BasePanel
{
    public ConTrolPanel controlPanel;
    private Transform ConTrolTransform;

    public override void InitFind()
    {
        base.InitFind();
        controlPanel = FindTool.FindChildComponent<ConTrolPanel>(transform, "ConTrolPanel");
        ConTrolTransform = controlPanel.gameObject.transform;
    }

    public override void InitEvent()
    {
        base.InitEvent();
    }

    //public override void Open()
    //{
    //    base.Open();
    //    controlPanel.BG_FullScreenButton();
    //}
}
