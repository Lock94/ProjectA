using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 物品槽
/// </summary>
public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public GameObject ItemPrefab;


    public virtual void SetItem(Item item, int amount = 1)
    {
        if (transform.childCount == 0)
        {
            GameObject itemGO = Instantiate(ItemPrefab) as GameObject;
            itemGO.transform.SetParent(this.transform);
            itemGO.transform.localPosition = Vector3.zero;
            itemGO.GetComponent<ItemUI>().SetItemUI(item, amount);
        }
        else
        {
            transform.GetChild(0).GetComponent<ItemUI>().AddAmount(amount);
        }
    }
    public bool HasItem()
    {
        if (transform.childCount == 0)
        {
            return false;
        }
        return true;
    }


    public void RemoveItem(int amount = 1)
    {
        ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();
        currentItem.ReduceAmount(amount);
        if (currentItem.Amount <= 0)
        {
            DestroyImmediate(currentItem.gameObject);
            InventoryManager.Instance.HideToolTip();
        }
    }

    public Item GetItem()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item;
    }

    public int GetAmount()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Amount;
    }

    public bool IsFilled()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Amount < transform.GetChild(0).GetComponent<ItemUI>().Item.MaxStark;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.childCount > 0)
            InventoryManager.Instance.HideToolTip();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            string toolTipText = transform.GetChild(0).GetComponent<ItemUI>().Item.GetToolTipText();
            InventoryManager.Instance.ShowToolTip(toolTipText);
        }
    }
    /// <summary>
    /// 右键物品栏且物品栏有物品
    /// </summary>
    public virtual void OnButtonRight()
    {
        ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();
        if (currentItem.Item.Type == Item.ItemType.Equipment || currentItem.Item.Type == Item.ItemType.ShootWeapon || currentItem.Item.Type == Item.ItemType.MeleeWeapon)
        {
            currentItem.ReduceAmount(1);
            Item tempItem = currentItem.Item;
            if (currentItem.Amount <= 0)
            {
                DestroyImmediate(currentItem.gameObject);
                InventoryManager.Instance.HideToolTip();
            }
            CharacterPanel.Instance.Equip(tempItem);
        }
        if (currentItem.Item.Type == Item.ItemType.Consumable)
        {
            //TODO 消耗品
            //TODO 普通消耗品（无持续性）
            //TODO 滋补品（有持续时间）
            currentItem.ReduceAmount(1);
            Consumable tempItem = (Consumable)currentItem.Item;
            if (currentItem.Amount <= 0)
            {
                DestroyImmediate(currentItem.gameObject);
                InventoryManager.Instance.HideToolTip();
            }
            if (tempItem.ContinuedTime != 0)
            {
                //使用了滋补品
                //Debug.Log("使用了"+tempItem.Name);
                CharacterPanel.Instance.AddBuff(tempItem.BuffID);
            }
            else
            {

            }
        }
        if (currentItem.Item.Type == Item.ItemType.Package)
        {
            //TODO 打开包裹
            currentItem.ReduceAmount(1);
            Package tempItem = (Package)currentItem.Item;
            if (currentItem.Amount <= 0)
            {
                DestroyImmediate(currentItem.gameObject);
                InventoryManager.Instance.HideToolTip();
            }
            if (tempItem.ItemIDs != null)
            {
                int getItemID = 0;
                int getItemAmount = 1;
                float randomProb = Random.value;
                for (int i = 0; i < tempItem.ItemProbabilities.Length; i++)
                {
                    float minProb = 0;
                    float maxProb = 0;
                    for (int j = 0; j < i; j++)
                    {
                        minProb += tempItem.ItemProbabilities[j];
                    }
                    for (int k = -1; k < i; k++)
                    {
                        maxProb += tempItem.ItemProbabilities[k + 1];
                    }
                    //string text= string.Format("第{0}组：最小概率：{1} 最大概率：{2} 本次概率：{3}", i, minProb, maxProb, randomProb);
                    if (randomProb >= minProb && randomProb <= maxProb)
                    {
                        getItemID = tempItem.ItemIDs[i];
                        getItemAmount = tempItem.ItemCount[i];
                    }
                    //Debug.Log(text);
                }
                for (int i = 0; i < getItemAmount; i++)
                {
                    BackPack.Instance.SetItem(getItemID);
                }
            }
            else if (tempItem.ItemTypes != null)
            {
                if (tempItem.ItemTechnologies != null && tempItem.ItemProbabilities[0] == 1)
                {
                    for (int i = 0; i < tempItem.ItemTypes.Length; i++)
                    {
                        List<Item> items = InventoryManager.Instance.FindItemsWithCondition(tempItem.ItemTypes[i], tempItem.ItemTechnologies[i]);
                        int randomID = Random.Range(0, items.Count);

                        Item getItem = items[randomID];
                        int itemCount = tempItem.ItemCount[i] > getItem.MaxStark ? getItem.MaxStark : tempItem.ItemCount[i];
                        for (int j = 0; j < itemCount; j++)
                        {
                            BackPack.Instance.SetItem(getItem);
                        }
                    }
                }
                if (tempItem.ItemTechnologies != null && tempItem.ItemProbabilities[0] != 1)
                {
                    float randomProb = Random.value;
                    Item.ItemTechnology randomTech = Item.ItemTechnology.T1;
                    for (int i = 0; i < tempItem.ItemProbabilities.Length; i++)
                    {
                        float minProb = 0;
                        float maxProb = 0;
                        for (int j = 0; j < i; j++)
                        {
                            minProb += tempItem.ItemProbabilities[j];
                        }
                        for (int k = -1; k < i; k++)
                        {
                            maxProb += tempItem.ItemProbabilities[k + 1];
                        }
                        //string text = string.Format("第{0}组：最小概率：{1} 最大概率：{2} 本次概率：{3}", i, minProb, maxProb, randomProb);
                        //Debug.Log(text);
                        if (randomProb >= minProb && randomProb <= maxProb)
                        {
                            randomTech = (Item.ItemTechnology)(i + 1);
                        }
                    }
                    for (int i = 0; i < tempItem.ItemTypes.Length; i++)
                    {
                        List<Item> items = InventoryManager.Instance.FindItemsWithCondition(tempItem.ItemTypes[i], randomTech);
                        int randomID = Random.Range(0, items.Count);
                        Item getItem = items[randomID];
                        int itemCount = tempItem.ItemCount[i] > getItem.MaxStark ? getItem.MaxStark : tempItem.ItemCount[i];
                        for (int j = 0; j < itemCount; j++)
                        {
                            BackPack.Instance.SetItem(getItem);
                        }
                    }
                }
            }
        }
        if (currentItem.Item.Type == Item.ItemType.Blueprint)
        {
            //TODO 学习蓝图
            currentItem.ReduceAmount(1);
            Blueprint tempItem = (Blueprint)currentItem.Item;
            if (currentItem.Amount <= 0)
            {
                DestroyImmediate(currentItem.gameObject);
                InventoryManager.Instance.HideToolTip();
            }
            foreach (int craftID in tempItem.CraftIDs)
            {
                InventoryManager.Instance.LearnCraft(craftID);
            }
        }
    }
    /// <summary>
    /// 左键物品且手里没拿东西
    /// </summary>
    public virtual void OnButtonLeftAndUnPickedItem()
    {
        ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();
        if (InventoryManager.Instance.IsControl)
        {
            int amountPicked = (currentItem.Amount + 1) / 2;
            InventoryManager.Instance.PickUpItem(currentItem.Item, amountPicked);
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
            Destroy(currentItem.gameObject);
        }
    }
    /// <summary>
    /// 手里有东西的情况下按左键且物品栏内物品和手上物品相同
    /// </summary>
    public virtual void OnButtonLeftAndPickedItemIsSame()
    {
        ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();
        if (InventoryManager.Instance.IsControl)                    //一个个放
        {
            if (currentItem.Amount < currentItem.Item.MaxStark)       //还有容量
            {
                currentItem.AddAmount();
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
                    InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
                }
                else
                {
                    currentItem.SetAmount(currentItem.Item.MaxStark);
                    InventoryManager.Instance.RemoveItem(amounntRemained);
                }
            }
            else
            {
                return;
            }
        }
    }
    /// <summary>
    /// 手里有东西的情况下按左键且物品栏内物品和手上物品不同
    /// </summary>
    public virtual void OnButtonLeftAndPickedItemIsNotSame()
    {
        ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();
        InventoryManager.Instance.PickedItem.ExchangeItem(currentItem);
    }
    /// <summary>
    /// 手里那东西的情况下左键且物品栏内物品为空
    /// </summary>
    public virtual void OnButtonLeftAndPickedItemNull()
    {
        if (InventoryManager.Instance.IsControl)    //一个个放
        {
            this.SetItem(InventoryManager.Instance.PickedItem.Item);
            InventoryManager.Instance.RemoveItem();
        }
        else                                        //全放
        {
            this.SetItem(InventoryManager.Instance.PickedItem.Item, InventoryManager.Instance.PickedItem.Amount);
            InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (transform.childCount > 0 && InventoryManager.Instance.IsPickedItem == false)
            {
                OnButtonRight();
            }
        }
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (transform.childCount > 0)     //物品槽不为空
            {
                ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();
                if (InventoryManager.Instance.IsPickedItem == false)
                {
                    OnButtonLeftAndUnPickedItem();
                }
                else
                {
                    if (currentItem.Item == InventoryManager.Instance.PickedItem.Item)
                    {
                        OnButtonLeftAndPickedItemIsSame();
                    }
                    else                                                            //互换
                    {
                        OnButtonLeftAndPickedItemIsNotSame();
                    }
                }
            }
            else                            //物品槽为空
            {
                if (InventoryManager.Instance.IsPickedItem == true)
                {
                    OnButtonLeftAndPickedItemNull();
                }
                else
                {
                    return;
                }
            }
        }
    }
}
