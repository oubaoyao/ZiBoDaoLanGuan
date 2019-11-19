using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public ScrollRect scrollRect;
    public CanvasGroup text;
    public bool IsOpen = true;
    // Start is called before the first frame update
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        if(scrollRect.verticalNormalizedPosition > 1)
        {
            Debug.Log("到头了！！");
            
        }

        if(scrollRect.verticalNormalizedPosition < 0)
        {
            Debug.Log("到地了！！");
            Anima();
        }
    }

    private void Anima()
    {
        if(IsOpen)
        {
            IsOpen = false;
            text.DOFillAlpha(1, 1.0f, TweenMode.NoUnityTimeLineImpact).OnComplete(() => {
                text.DOFillAlpha(0, 1.0f, TweenMode.NoUnityTimeLineImpact).OnComplete(() => {
                    if(scrollRect.verticalNormalizedPosition < 0)
                    {
                        scrollRect.verticalNormalizedPosition = 0;
                    }
                    IsOpen = true;
                });
            });
        }
    }
}
