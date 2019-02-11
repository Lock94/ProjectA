using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VendorSlot : Slot
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && InventoryManager.Instance.IsPickedItem == false)
        {
            if (transform.childCount > 0)
            {
                ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();
                //可以购买
                if (VendorPanel.Instance.CanBuyItem(currentItem.Item, 1, true))
                {
                    currentItem.ReduceAmount(1);
                    Item tempItem = currentItem.Item;
                    if (currentItem.Amount <= 0)
                    {
                        DestroyImmediate(currentItem.gameObject);
                        InventoryManager.Instance.HideToolTip();
                    }
                    VendorPanel.Instance.BuyItem(currentItem.Item, true);
                }
            }
        }
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (transform.childCount > 0)     //物品槽不为空
            {
                ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();
                if (InventoryManager.Instance.IsPickedItem == false)
                {
                    if (InventoryManager.Instance.IsControl)
                    {
                        int amountPicked = (currentItem.Amount + 1) / 2;
                        InventoryManager.Instance.PickUpItem(currentItem.Item, amountPicked);
                        InventoryManager.Instance.PickedUpSlot(transform.GetComponent<Slot>());
                        int amounntRemained = currentItem.Amount - amountPicked;
                        if (amounntRemained <= 0)
                        {
                            Destroy(currentItem.gameObject);
                        }
                        else
                        {
                            currentItem.SetAmount(amounntRemained);
                        }
                    }
                    else
                    {
                        InventoryManager.Instance.PickUpItem(currentItem.Item, currentItem.Amount);
                        InventoryManager.Instance.PickedUpSlot(transform.GetComponent<Slot>());
                        Destroy(currentItem.gameObject);
                    }
                }
                else
                {
                    if (currentItem.Item == InventoryManager.Instance.PickedItem.Item)
                    {
                        if (InventoryManager.Instance.IsControl)                    //一个个放
                        {
                            if (currentItem.Amount < currentItem.Item.MaxStark)       //还有容量
                            {
                                currentItem.AddAmount();
                                VendorPanel.Instance.SellItem(currentItem.Item, false);
                                InventoryManager.Instance.RemoveItem();
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            if (currentItem.Item.MaxStark > currentItem.Amount)        //还有容量  
                            {
                                int amounntRemained = currentItem.Item.MaxStark - currentItem.Amount;
                                if (amounntRemained >= InventoryManager.Instance.PickedItem.Amount)   //完全放下
                                {
                                    currentItem.SetAmount(currentItem.Amount + InventoryManager.Instance.PickedItem.Amount);
                                    VendorPanel.Instance.SellItem(currentItem.Item, false, InventoryManager.Instance.PickedItem.Amount);
                                    InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
                                }
                                else
                                {
                                    currentItem.SetAmount(currentItem.Item.MaxStark);
                                    VendorPanel.Instance.SellItem(currentItem.Item, false, amounntRemained);
                                    InventoryManager.Instance.RemoveItem(amounntRemained);
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    //else                                                            //互换
                    //{
                    //    InventoryManager.Instance.PickedItem.ExchangeItem(currentItem);
                    //}
                }
            }
            else                            //物品槽为空
            {
                if (InventoryManager.Instance.IsPickedItem == true)
                {
                    if (InventoryManager.Instance.IsControl)    //一个个放
                    {
                        this.SetItem(InventoryManager.Instance.PickedItem.Item);
                        VendorPanel.Instance.SellItem(InventoryManager.Instance.PickedItem.Item, false);
                        InventoryManager.Instance.RemoveItem();
                    }
                    else                                        //全放
                    {
                        this.SetItem(InventoryManager.Instance.PickedItem.Item, InventoryManager.Instance.PickedItem.Amount);
                        VendorPanel.Instance.SellItem(InventoryManager.Instance.PickedItem.Item, false, InventoryManager.Instance.PickedItem.Amount);
                        InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }
}
