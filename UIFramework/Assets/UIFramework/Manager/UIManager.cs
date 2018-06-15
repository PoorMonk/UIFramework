using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager  {
    private Dictionary<UIPanelType, string> panelPathDict; //存储所有面板Prefab的路径
    private Dictionary<UIPanelType, BasePanel> panelDict;  //存储所有实例化的Panel的BasePanel组件
    private Stack<BasePanel> panelStack;
    private static UIManager _instance;
    private Transform canvasTransform;
    public Transform CanvasTransform
    {
        get
        {
            if (canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }  

    private UIManager()
    {
        ParsePanleTypeJson();
    }

    public void PushPanel(UIPanelType panelType)
    {
        if (null == panelStack)
        {
            panelStack = new Stack<BasePanel>();
        }

        if (0 < panelStack.Count)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }

        BasePanel panel = GetPanel(panelType);
        panel.OnEnter();
        panelStack.Push(panel);
    }

    public void PopPanel(UIPanelType panelType)
    {
        if (null == panelStack)
            return;

        if (panelStack.Count < 0)
            return;
        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();

        if (panelStack.Count < 0)
            return;
        topPanel = panelStack.Peek();
        topPanel.OnResume();
    }

    [Serializable]
    public class PanelTypeJsonList
    {
        public List<UIPanelInfo> panelInfoList;
    }

    private void ParsePanleTypeJson()
    {
        panelPathDict = new Dictionary<UIPanelType, string>();
        TextAsset ta = Resources.Load<TextAsset>("UIPanelType");
        PanelTypeJsonList panelList = JsonUtility.FromJson<PanelTypeJsonList>(ta.text);
        foreach (UIPanelInfo info in panelList.panelInfoList)
        {
            //Debug.Log(info.panelType);
            panelPathDict.Add(info.panelType, info.path);
        }
    }

    private BasePanel GetPanel(UIPanelType panelType)
    {       
        if (panelDict == null)
        {
            panelDict = new Dictionary<UIPanelType, BasePanel>();
        }

        BasePanel panel = panelDict.TryGet(panelType);
        if (null == panel)
        {
            string path = panelPathDict.TryGet(panelType);          
            GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            instPanel.transform.SetParent(CanvasTransform, false);
            panel = instPanel.GetComponent<BasePanel>();
            panelDict.Add(panelType, panel);
        }

        return panel;
    }
	
    //just for test
    public void test()
    {
        string path;
        panelPathDict.TryGetValue(UIPanelType.ItemMessage, out path);
        //Debug.Log(path);
    }

}
