using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemMessagePanel : BasePanel {

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
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        transform.localScale = Vector3.zero;
        transform.DOScale(1, .5f);
    }

    public override void OnExit()
    {
        
        canvasGroup.blocksRaycasts = false;
        transform.DOScale(0, .5f).OnComplete(() => canvasGroup.alpha = 0);
    }

    public void OnCloseBtn()
    {
        UIManager.Instance.PopPanel(UIPanelType.Task);
    }
}
