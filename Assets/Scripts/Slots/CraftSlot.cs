using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 合成物品槽，不与其他Panel交互
/// </summary>
public class CraftSlot : Slot
{
    public CraftRecipe craftItem;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (transform.childCount > 0 && InventoryManager.Instance.IsPickedItem == false)
        {
           // ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();
            transform.parent.parent.parent.parent.parent.SendMessage("SetCraftText", craftItem);
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
            itemGO.GetComponent<ItemUI>().useAnimation = false;
        }
        else
        {
            transform.GetChild(0).GetComponent<ItemUI>().AddAmount(amount);
        }
    }

}
