using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftPanel : Inventory
{
    #region 单例模式
    private static CraftPanel _instance;
    public static CraftPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("CraftPanel").GetComponent<CraftPanel>();
            }
            return _instance;
        }
    }
    #endregion

    public KeyCode button = KeyCode.O;

    public GameObject craftSlot;

    private Dropdown craftType;

    private Transform needContent;

    //private List<List<CraftRecipe>> craftRecipos;

    private Text needTitle;
    private Text needItems;

    private Button craftButton;
    private CraftRecipe tempCraft;

    public override void Start()
    {
        base.Start();
        craftType = transform.Find("Type").GetComponent<Dropdown>();
        needContent = transform.Find("GetPanel").Find("GetView").GetComponent<ScrollRect>().content;
        needTitle = transform.Find("NeedPanel").Find("Title").GetComponent<Text>();
        needItems = transform.Find("NeedPanel").Find("NeedText").GetComponent<Text>();
        craftButton = transform.Find("CraftButton").GetComponent<Button>();
        SetCraftType();
        craftType.onValueChanged.AddListener(delegate { CraftChangeMethod(); });
        craftButton.onClick.AddListener(delegate { CraftItems(); });
        SetGetItem(0);
        SetCraftText();
    }

    public void CraftItems()
    {
        //Debug.Log("Crafting");
        for (int i = 0; i < tempCraft.NeedItemIDs.Length; i++)
        {
            BackPack.Instance.RemoveItem(tempCraft.NeedItemIDs[i], tempCraft.NeedItemCount[i]);
        }
        for (int i = 0; i < tempCraft.GetItemIDs.Length; i++)
        {
            BackPack.Instance.SetItem(tempCraft.GetItemIDs[i]);
        }   
        SetCraftText(tempCraft);
    }
    /// <summary>
    /// 当改变craftType时候的方法
    /// </summary>
    public void CraftChangeMethod()
    {
        //Debug.Log("CraftType value" + craftType.value);
        SetGetItem(craftType.value);
        SetCraftText();
    }

    void SetGetItem(int typeValue)
    {
        for (int i = 0; i < needContent.childCount; i++)
        {
            Destroy(needContent.GetChild(i).gameObject);
        }
        foreach (CraftRecipe craft in InventoryManager.Instance.GetCraftsOfType(typeValue))
        {
            GameObject craftGo = Instantiate(craftSlot) as GameObject;
            craftGo.transform.SetParent(needContent);
            Item setItem = typeValue == 4 ? InventoryManager.Instance.GetItemByID(craft.NeedItemIDs[0]) : InventoryManager.Instance.GetItemByID(craft.GetItemIDs[0]);
            craftGo.GetComponent<CraftSlot>().SetItem(setItem);
            craftGo.GetComponent<CraftSlot>().craftItem = craft;
        }
    }

    void SetCraftType()
    {
        string[] typeNames = System.Enum.GetNames(typeof(CraftRecipe.CraftType));
        craftType.options = new List<Dropdown.OptionData>();

        for (int i = 0; i < typeNames.Length; i++)
        {
            Dropdown.OptionData name = new Dropdown.OptionData { text = typeNames[i] };
            craftType.options.Add(name);
        }
        craftType.value = 1;
        craftType.value = 0;  
    }

    public void SetCraftText(CraftRecipe craft)
    {
        needTitle.text = craft.Type != CraftRecipe.CraftType.拆解 ? string.Format("合成{0}需要：", craft.Name) : string.Format("{0}需要：", craft.Name);
        tempCraft = craft;
        string needText = "";
        bool canCraft = true;
        for (int i = 0; i < craft.NeedItemIDs.Length; i++)
        {
            needText += string.Format("{0} ({1}/{2})\n", InventoryManager.Instance.GetItemByID(craft.NeedItemIDs[i]).Name, BackPack.Instance.GetCountByID(craft.NeedItemIDs[i]), craft.NeedItemCount[i]);
            if (BackPack.Instance.GetCountByID(craft.NeedItemIDs[i]) < craft.NeedItemCount[i])
            {
                canCraft = false;
            }
        }
        if (craft.Type == CraftRecipe.CraftType.拆解)
        {
            needText += string.Format("\n获得以下物品：\n");
            for (int i = 0; i < craft.GetItemIDs.Length; i++)
            {
                needText += string.Format("{0}:{1}\n", InventoryManager.Instance.GetItemByID(craft.GetItemIDs[i]).Name, craft.GetItemCount[i]);
            }
        }
        craftButton.interactable = canCraft;
        needItems.text = needText;
    }

    public void SetCraftText()
    {
        needTitle.text = " ";
        needItems.text = " ";
    }
}
