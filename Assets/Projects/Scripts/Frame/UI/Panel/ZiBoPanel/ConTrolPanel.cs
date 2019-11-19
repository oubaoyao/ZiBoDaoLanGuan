using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;
using DG;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConTrolPanel : BasePanel
{
    public RectTransform ConTrolGroup;

    private bool IsMove = true;

    public Button[] ButtonGroups;
    public Button NarrowButton;

    public RectTransform BG;

    public Transform Other;

    private Vector3 ConTrolGroup_Initial_Position;
    private Vector3 BG_Initial_sizeDelta;

    public CanvasGroup NarrowCanvasGroup;
    public CanvasGroup[] MenuGroup;

    public GameObject MenuButtonPrefab;

    private string[] A_area = {
        "中陶刻瓷绘画艺术研究院",
        "武汉科技大学淄博耐火材料工程研究",
        "上海交通大学淄博高新区先进材料联合研究中心",
        "中澳（淄博）国际协同创新研究院",
        "淄博高新区钢研金属陶瓷研究所",
        "北京科技大学山东金属材料研究院",
        "武汉理工大学淄博先进陶瓷研究院",
        "中国电子科技集团公司陶瓷山东研究院",
        "哈尔滨工业大学淄博机器人研发中心",
        "淄博高新区中乌等离子技术研究院"
    };

    private string[] B_area = {
        "山东齐鲁金融研究院",    
        "中铝中央研究院山东分技术中心"
    };

    private string[] C1_area = {
        "无机非金属材料工程化研发中心",
        "无机非金属公共技术服务平台",
        "淄博市陶瓷艺术设计中心"
    };

    private string[] C2_area = {
        "清华艺术与科学创新研究院淄博研究中心",
        "景德镇陶瓷大学淄博设计研发中心",
        "上海视觉艺术学院淄博设计研发中心"
    };

    private string[] C3_area = {
        "淄博市知识产权公共服务平台",
        "山东博思琉璃制品有限公司设计研发中心",
        "清华大学美术学院（淄博）琉璃设计研发中心",
        "岳孝清陶琉艺术设计中心"
    };

    protected override void Start()
    {
        base.Start();
        AddMenuButton(MenuGroup[0].gameObject.transform, A_area);
        AddMenuButton(MenuGroup[1].gameObject.transform, B_area);
        AddMenuButton(MenuGroup[2].gameObject.transform, C1_area);
        AddMenuButton(MenuGroup[3].gameObject.transform, C2_area);
        AddMenuButton(MenuGroup[4].gameObject.transform, C3_area);
    }

    public override void InitFind()
    {
        base.InitFind();
        ConTrolGroup = FindTool.FindChildComponent<RectTransform>(transform, "ConTrolGroup");

        ButtonGroups = FindTool.FindChildNode(transform, "ConTrolGroup/Other/ButtonGroup").GetComponentsInChildren<Button>();
        NarrowButton = FindTool.FindChildComponent<Button>(transform, "ConTrolGroup/NarrowGroup/NarrowButton");

        BG = FindTool.FindChildComponent<RectTransform>(transform, "ConTrolGroup/BG");

        Other = FindTool.FindChildNode(transform, "Other");
        MenuGroup = FindTool.FindChildNode(transform, "ConTrolGroup/Other/MenuGroup").GetComponentsInChildren<CanvasGroup>();

        NarrowCanvasGroup = FindTool.FindChildComponent<CanvasGroup>(transform, "ConTrolGroup/NarrowGroup");

        MenuButtonPrefab = Resources.Load("MenuButton")as GameObject;

        ConTrolGroup_Initial_Position = ConTrolGroup.anchoredPosition3D;
        
        BG_Initial_sizeDelta = BG.sizeDelta;

    }

    public override void InitEvent()
    {
        base.InitEvent();
        ButtonGroups[0].onClick.AddListener(() => {
            BG_FullScreenButton();
        });

        NarrowButton.onClick.AddListener(() => {
            BG_NarrowButton();
        });

        for (int i = 0; i < ButtonGroups.Length; i++)
        {
            if(i!=0)
            {
               DisplayMenu(ButtonGroups[i], i-1);
            }
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0)&&IsMove)
        {
            foreach (CanvasGroup item in MenuGroup)
            {
                item.alpha = 0;
                item.blocksRaycasts = false;
            }
            Vector3 ScreenPosition = Input.mousePosition;
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = ScreenPosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            if(results.Count > 0)
            {
                string TagName = results[0].gameObject.tag;
                Debug.Log("Name===" + results[0].gameObject.name);
                if (TagName.Contains("Untagged"))
                {
                    ConTrolGroup.DOMove(ScreenPosition, 0.5f, TweenMode.NoUnityTimeLineImpact);
                    ConTrolGroup_Initial_Position = ScreenPosition;
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
        if(IsMove)
        {
            Vector3 ScreenPosition = Input.mousePosition;
            ConTrolGroup.gameObject.transform.position = ScreenPosition;
            ConTrolGroup_Initial_Position = ScreenPosition;
        }
       
    }

    public void BG_FullScreenButton()
    {
        IsMove = false;
        Other.gameObject.SetActive(false);
        BG.sizeDelta = new Vector2(Screen.width, Screen.height);
        ConTrolGroup.DOMove(new Vector3(Screen.width/2, Screen.height/2), 0.5f, TweenMode.NoUnityTimeLineImpact).OnComplete(()=> { NarrowGroup_Open(); });
    }

    private void BG_NarrowButton()
    {
        NarrowGroup_Hide();
        BG.sizeDelta = BG_Initial_sizeDelta;
        ConTrolGroup.DOMove(ConTrolGroup_Initial_Position, 0.5f, TweenMode.NoUnityTimeLineImpact).OnComplete(()=> { Other.gameObject.SetActive(true); IsMove = true; });
    }

    private void NarrowGroup_Open()
    {
        NarrowCanvasGroup.alpha = 1;
        NarrowCanvasGroup.blocksRaycasts = true;
    }

    private void NarrowGroup_Hide()
    {
        NarrowCanvasGroup.alpha = 0;
        NarrowCanvasGroup.blocksRaycasts = false;
    }

    private int num = 0;
    private void AddMenuButton(Transform ParenttTransform, string[] vs)
    {
        for (int i = 0; i < vs.Length; i++)
        {
            GameObject Button =  Instantiate(MenuButtonPrefab);
            Button.GetComponentInChildren<Text>().text = " " + vs[i];
            Button.transform.SetParent(ParenttTransform);
            AddButtonListen(Button.GetComponentInChildren<Button>(), num);
            num++;
        }
    }

    private void AddButtonListen(Button button,int num)
    {
        button.onClick.AddListener(() => {

        });
    }

    private void DisplayMenu(Button button,int num)
    {
        button.onClick.AddListener(() => {
            foreach (CanvasGroup item in MenuGroup)
            {
                item.alpha = 0;
                item.blocksRaycasts = false;
            }
            MenuGroup[num].DOFillAlpha(1, 0.5f);
            MenuGroup[num].blocksRaycasts = true;
        });
    }

}
