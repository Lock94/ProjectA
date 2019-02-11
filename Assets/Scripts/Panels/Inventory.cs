using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private Slot[] slotList;

    private float targetAlpha = 0;
    private float smoothing = 5;

    private CanvasGroup canvasGroup;

    private Button exitButton;
    // Use this for initialization
    public virtual void Start ()
    {
        slotList= GetComponentsInChildren<Slot>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (transform.Find("Exit"))
        {
            exitButton = transform.Find("Exit").GetComponent<Button>();
            exitButton.onClick.AddListener(delegate { Hide(); });
        }
        if (canvasGroup!=null)
        {
            canvasGroup.blocksRaycasts = false;
        }
    
    }
    protected Slot[] SlotList
    {
        get
        {
            if (slotList == null)
            {
                slotList = GetComponentsInChildren<Slot>();
            }
            return slotList;
        }
    }

    void Update()
    {
        if (canvasGroup!=null&&canvasGroup.alpha !=targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothing * Time.deltaTime);
            if (Mathf.Abs(canvasGroup.alpha-targetAlpha)<0.02f)
            {
                canvasGroup.alpha = targetAlpha;
            }
        }
    }

    public bool SetItem(int id)
    {
        Item item = InventoryManager.Instance.GetItemByID(id);
        return SetItem(item);
    }

    public bool SetItem(Item item)
    {
        if (item ==null)
        {
            Debug.LogWarning("存储的物品ID不存在");
            return false;
        }
        if (item.MaxStark==1)
        {
            //TODO 
            Slot slot = FindEmptySlot();
            if (slot ==null)
            {
                Debug.LogWarning("背包已满，没有空的物品槽");
                return false;
            }
            else
            {
                slot.SetItem(item);//存入背包
                return true;
            }
        }
        else
        {
            Slot slot = FindSameItemSlot(item);
            if (slot !=null)
            {
                slot.SetItem(item);//存入背包
                return true;
            }
            else
            {
                Slot emptySlot = FindEmptySlot();
                if (emptySlot == null)
                {
                    Debug.LogWarning("背包已满，没有空的物品槽");
                    return false;
                }
                else
                {
                    emptySlot.SetItem(item);//存入背包
                    return true;
                }
            }
        }
    }
    /// <summary>
    /// 寻找空的物品槽
    /// </summary>
    /// <returns></returns>
    private Slot FindEmptySlot()
    {
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount==0)
            {
                return slot;
            }
        }
        return null;
    }
    private Slot FindSameItemSlot(Item item)
    {
        foreach (Slot  slot in slotList)
        {
            if (slot.transform.childCount>=1&&item ==slot.GetItem()&&slot.IsFilled())
            {
                return slot;
            }
        }
        return null;
    }

    public void Show()
    {
        if (canvasGroup!=null)
        {
            canvasGroup.blocksRaycasts = true;
            targetAlpha = 1;
        }
    }
    public void Hide()
    {
        if (canvasGroup!=null)
        {
            canvasGroup.blocksRaycasts = false;
            targetAlpha = 0;
        }
    }
    public void Toggle()
    {
        if (targetAlpha == 0)
        {
            Show();
        }
        else if (targetAlpha==1)
        {
            Hide();
        }
    }
}
