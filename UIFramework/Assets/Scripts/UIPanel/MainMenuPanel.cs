using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : BasePanel {
    private CanvasGroup canvasGroup;

    private void Start()
    {
        if (null == canvasGroup)
            canvasGroup = GetComponent<CanvasGroup>();
    }
     
    public void OnPushPanel(string panelTypeString)
    {
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIManager.Instance.PushPanel(panelType);
    }

    //public override void OnEnter()
    //{
    //    if (null == canvasGroup)
    //        canvasGroup = GetComponent<CanvasGroup>();
    //    canvasGroup.alpha = 1;
    //    canvasGroup.blocksRaycasts = true;
    //}

    public override void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }

    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
    }
}
