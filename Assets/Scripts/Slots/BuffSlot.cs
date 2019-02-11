using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuffSlot : CraftSlot
{
    private Image coldown;
    public Image Coldown
    {
        get
        {
            return coldown;
        }
    }

    public override void SetItem(Item item, int amount = 1)
    {
        if (transform.childCount == 0)
        {
            GameObject itemGO = Instantiate(ItemPrefab) as GameObject;
            itemGO.transform.SetParent(this.transform);
            itemGO.transform.localPosition = Vector3.zero;
            itemGO.GetComponent<ItemUI>().SetItemUI(item, amount);
            itemGO.GetComponent<ItemUI>().targetScale = 1;
            itemGO.GetComponent<ItemUI>().animationScale = new Vector3(1, 1, 1);
            itemGO.GetComponent<ItemUI>().useAnimation = false;
            coldown = itemGO.transform.Find("Coldown").GetComponent<Image>();
        }
        else
        {
            transform.GetChild(0).GetComponent<ItemUI>().AddAmount(amount);
        }
    }
    public override void OnPointerDown(PointerEventData eventData)
    {

    }
    public void  DestroyBuff()
    {
        DestroyImmediate(transform.gameObject);
    }
}
