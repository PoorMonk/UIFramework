using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KnapsackPanel : BasePanel {

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

        Vector3 tempPos = transform.localPosition;
        tempPos.x = 1000;
        transform.localPosition = tempPos;
        transform.DOLocalMoveX(0, .5f);
    }

    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
    }

    public override void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }

    public override void OnExit()
    {
        //canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;

        transform.DOLocalMoveX(1000, .5f).OnComplete(() => canvasGroup.alpha = 0);
    }

    public void OnCloseBtn()
    {
        UIManager.Instance.PopPanel(UIPanelType.Task);
    }

    public void OnItemClickedBtn()
    {
        UIManager.Instance.PushPanel(UIPanelType.ItemMessage);
    }
}
