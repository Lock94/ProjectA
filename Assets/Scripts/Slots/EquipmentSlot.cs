using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : Slot
{
    public Sprite defaultSprite;
    public Sprite equipSprite;
    public Equipment.EquipmentType equipType;
    //下面两个符合任意条件即可
    public Item.ItemType itemType1;
    public Item.ItemType itemType2;



    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (transform.childCount > 0 && InventoryManager.Instance.IsPickedItem == false)
            {
                ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();

                DestroyImmediate(currentItem.gameObject);
                transform.parent.SendMessage("UnEquip", currentItem.Item);
                InventoryManager.Instance.HideToolTip();
            }
        }
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (InventoryManager.Instance.IsPickedItem == true)
                {
                    ItemUI pickedItem = InventoryManager.Instance.PickedItem;
                    if (transform.childCount > 0)
                    {
                        //与手中物品交换
                        ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();
                        if (IsRightItem(pickedItem.Item))
                        {
                            transform.parent.GetComponent<CharacterPanel>().EquipItem(pickedItem.Item, true);
                            InventoryManager.Instance.PickedItem.ExchangeItem(currentItem);
                            transform.GetComponent<Image>().sprite = equipSprite;
 
                        }
                    }
                    else
                    {
                        //直接放下手中的物品
                        if (IsRightItem(pickedItem.Item))
                        {
                            this.SetItem(pickedItem.Item);
                            InventoryManager.Instance.RemoveItem(1);
                            transform.GetComponent<Image>().sprite = equipSprite;
                            transform.parent.GetComponent<CharacterPanel>().EquipItem(pickedItem.Item, true);

                        }

                    }
                }
                else
                {
                    if (transform.childCount > 0)
                    {
                        //拿起框中的物品
                        ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();
                        InventoryManager.Instance.PickUpItem(currentItem.Item, currentItem.Amount);
                        Destroy(currentItem.gameObject);
                        transform.GetComponent<Image>().sprite = defaultSprite;
                        transform.parent.GetComponent<CharacterPanel>().EquipItem(currentItem.Item, false);
                    }
                }
            }
        }
    }
    public void CheckEquipSlot()
    {
        if (transform.childCount > 0)
        {
            transform.GetComponent<Image>().sprite = equipSprite;
        }
        else
        {
            transform.GetComponent<Image>().sprite = defaultSprite;
        }
    }

    public bool IsRightItem(Item item)
    {
        if ((item.Type ==Item.ItemType.Equipment && ((Equipment)item).EquipType == this.equipType )|| (item.Type == this.itemType1 || item.Type == this.itemType2))          
        {
            return true;
        }
        return false;
    }
}
