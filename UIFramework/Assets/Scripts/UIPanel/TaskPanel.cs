using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TaskPanel : BasePanel {

    private CanvasGroup canvasGroup;

    private void Start()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void OnEnter()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = true;

        canvasGroup.DOFade(1, .5f);
    }

    public override void OnExit()
    {
        
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0, .5f).OnComplete(() => canvasGroup.alpha = 0);
    }

    public void OnCloseBtn()
    {
        UIManager.Instance.PopPanel(UIPanelType.Task);
    }
}
