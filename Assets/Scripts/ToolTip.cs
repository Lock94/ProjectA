using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{

    private float targetAlpha = 0;
    public float smoothing = 4;

    #region UI Component
    private Text toolTipText;
    private Text contentText;
    private CanvasGroup canvasGroup;
    private Text ToolTipText
    {
        get
        {
            if (toolTipText==null)
            {
                toolTipText = GetComponent<Text>();
            }
            return toolTipText;
        }
    }
    private Text ContentText
    {
        get
        {
            if (contentText ==null)
            {
                contentText = transform.Find("Content").GetComponent<Text>();
            }
            return contentText;
        }
    }
    private CanvasGroup CanvasGroup
    {
        get
        {
            if (canvasGroup==null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
            return canvasGroup;
        }
    }
    #endregion

    private void Update()
    {
        if (CanvasGroup.alpha !=targetAlpha )
        {
            CanvasGroup.alpha = Mathf.Lerp(CanvasGroup.alpha, targetAlpha, smoothing * Time.deltaTime);
            if (Mathf.Abs(targetAlpha- CanvasGroup.alpha)<0.01F)
            {
                CanvasGroup.alpha = targetAlpha;
            }
        }     
    }

    public void Show(string text)
    {
        ToolTipText.text = text;
        ContentText.text = text;
        targetAlpha = 1;
    }
    public void Hide()
    {
        targetAlpha = 0;
    }
    public void SetLocalPostion(Vector3 position)
    {
        transform.localPosition = position;
    }
}
