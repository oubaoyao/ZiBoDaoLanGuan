using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;
using DG;
using UnityEngine.EventSystems;

public class ConTrolPanel : BasePanel
{
    public RectTransform ConTrolGroup;

    public override void InitFind()
    {
        base.InitFind();
        ConTrolGroup = FindTool.FindChildComponent<RectTransform>(transform, "ConTrolGroup");
    }

    public override void InitEvent()
    {
        base.InitEvent();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 ScreenPosition = Input.mousePosition;
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = ScreenPosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            if(results.Count > 0)
            {
                string TagName = results[0].gameObject.tag;
                if (TagName.Contains("Untagged"))
                {
                    ConTrolGroup.DOMove(ScreenPosition, 0.5f, TweenMode.NoUnityTimeLineImpact);
                }
                else
                {
                    Debug.Log("TAG===" + TagName);
                }
            }
        }
    }

    public void OnMouseDrag()
    {
        ConTrolGroup.gameObject.transform.position = Input.mousePosition;
    }

}
