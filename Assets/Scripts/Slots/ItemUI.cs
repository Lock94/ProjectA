using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {

    public Item Item { get; set; }
    public int Amount { get; private set; }

    #region UI Component
    private Image icon;
    private Text amountText;

    private Image Icon
    {
        get
        {
            if (icon == null)
            {
                icon = GetComponent<Image>();
            }
            return icon;
        }
    }

    private Text AmountText
    {
        get
        {
            if (amountText == null)
            {
                amountText = GetComponentInChildren<Text>();
            }
            return amountText;
        }
    }
    #endregion

    public float targetScale = 1.7f;

    public Vector3 animationScale = new Vector3(2.5f, 2.5f, 2.5f);

    public bool useAnimation = true;

    private float smoothing = 4;

    private void Update()
    {
        if (targetScale != transform.localScale.x&& useAnimation)
        {
            float scale = Mathf.Lerp(transform.localScale.x, targetScale, smoothing*Time.deltaTime);
            transform.localScale = new Vector3(scale, scale, scale);
            if (Mathf.Abs (targetScale-transform.localScale.x)<=0.02f)
            {
                transform.localScale = new Vector3(targetScale, targetScale, targetScale);
            }
        }
        if (!useAnimation && transform.localScale.x != targetScale)
        {
            transform.localScale = new Vector3(targetScale, targetScale, targetScale);
        }
    }

    public void SetItemUI(Item item,int amount=1)
    {
        if (useAnimation)
            transform.localScale = animationScale;
        this.Item = item;
        this.Amount = amount;
        Icon.sprite = Resources.Load<Sprite>(item.Sprite);
        if (Item.MaxStark>1)
            AmountText.text = Amount.ToString();
        else
            AmountText.text = " ";
    }
    public void AddAmount(int amount =1)
    {
        if (useAnimation)
            transform.localScale = animationScale;
        this.Amount += amount;
        AmountText.text = Amount.ToString();
        if (Item.MaxStark > 1)
            AmountText.text = Amount.ToString();
        else
            AmountText.text = " ";
    }
    public void SetAmount(int amount)
    {
        if (useAnimation)
            transform.localScale = animationScale;
        this.Amount = amount;
        if (Item.MaxStark > 1)
            AmountText.text = Amount.ToString();
        else
            AmountText.text = " ";
    }
    public void ReduceAmount(int amount=1)
    {
        if (useAnimation)
            transform.localScale = animationScale;
        this.Amount -= amount;
        if (Item.MaxStark > 1)
            AmountText.text = Amount.ToString();
        else
            AmountText.text = " ";
    }
    public void ExchangeItem(ItemUI itemUI)
    {
        Item tempItem = itemUI.Item;
        int tempAmount = itemUI.Amount;
        itemUI.SetItemUI(this.Item, this.Amount);
        this.SetItemUI(tempItem, tempAmount);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

}
