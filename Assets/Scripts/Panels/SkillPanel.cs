using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillPanel : Inventory
{
    #region 单例模式
    private static SkillPanel _instance;
    public static SkillPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Canvas").transform.Find("SkillPanel").GetComponent<SkillPanel>();
            }
            return _instance;
        }
    }
    #endregion

    private Button reset;
    private Button save;
    private Text sxPoint;
    private Text SxPoint
    {
        get
        {
            if (sxPoint == null)
            {
                sxPoint = transform.Find("SxPoint").Find("Text").GetComponent<Text>();
            }
            return sxPoint;
        }
    }
    private Text tzPoint;
    private Text TzPoint
    {
        get
        {
            if (tzPoint==null)
            {
                tzPoint = transform.Find("TzPoint").Find("Text").GetComponent<Text>();
            }
            return tzPoint;
        }
    }
    private SkillSlot[] skillSlots;
    public SkillSlot[] SkillSlots
    {
        get
        {
            if (skillSlots ==null)
            {
                skillSlots = new SkillSlot[SlotList.Length];

                for (int i = 0; i < SlotList.Length; i++)
                {
                    skillSlots[i] = (SkillSlot)SlotList[i];
                }
            }
            return skillSlots;
        }
    }

    private int[] tempPoints;
    private int tempSxPoint;
    private int tempTzPoint;



    void InitSkill()
    {
        if (reset == null)
        {
            reset = transform.Find("Reset").GetComponent<Button>();
            save = transform.Find("Save").GetComponent<Button>();

            reset.onClick.AddListener(delegate { ResetPoint(); });
            save.onClick.AddListener(delegate { SavePoint(); });
        }
        if (SlotList.Length!=25)
        {
            Debug.LogWarning("缺少技能槽");
            return;
        }
        if (SlotList[0].transform.childCount<2)
        {
            SlotList[0].SetItem(InventoryManager.Instance.GetItemByID(343)); 
            SlotList[1].SetItem(InventoryManager.Instance.GetItemByID(344));
            SlotList[2].SetItem(InventoryManager.Instance.GetItemByID(345));
            SlotList[3].SetItem(InventoryManager.Instance.GetItemByID(346));
            SlotList[4].SetItem(InventoryManager.Instance.GetItemByID(347));

            SlotList[5].SetItem(InventoryManager.Instance.GetItemByID(348));
            SlotList[6].SetItem(InventoryManager.Instance.GetItemByID(349));
            SlotList[7].SetItem(InventoryManager.Instance.GetItemByID(350));
            SlotList[8].SetItem(InventoryManager.Instance.GetItemByID(351));
            SlotList[9].SetItem(InventoryManager.Instance.GetItemByID(352));
            SlotList[10].SetItem(InventoryManager.Instance.GetItemByID(353));
            SlotList[11].SetItem(InventoryManager.Instance.GetItemByID(354));
            SlotList[12].SetItem(InventoryManager.Instance.GetItemByID(355));
            SlotList[13].SetItem(InventoryManager.Instance.GetItemByID(356));
            SlotList[14].SetItem(InventoryManager.Instance.GetItemByID(357));
            SlotList[15].SetItem(InventoryManager.Instance.GetItemByID(358));
            SlotList[16].SetItem(InventoryManager.Instance.GetItemByID(359));
            SlotList[17].SetItem(InventoryManager.Instance.GetItemByID(360));
            SlotList[18].SetItem(InventoryManager.Instance.GetItemByID(361));
            SlotList[19].SetItem(InventoryManager.Instance.GetItemByID(362));
            SlotList[20].SetItem(InventoryManager.Instance.GetItemByID(363));
            SlotList[21].SetItem(InventoryManager.Instance.GetItemByID(364));
            SlotList[22].SetItem(InventoryManager.Instance.GetItemByID(365));
            SlotList[23].SetItem(InventoryManager.Instance.GetItemByID(366));
            SlotList[24].SetItem(InventoryManager.Instance.GetItemByID(367));
        }

        for (int i = 0; i < SkillSlots.Length; i++)
        {
            SkillSlots[i].SkillID = i;
        }
    }

    public void SetSkillPoints(int[] skillPoints,int _sxPoint,int _tzPoint)
    {
        InitSkill();
        SxPoint.text = _sxPoint.ToString();
        TzPoint.text = _tzPoint.ToString();
        tempPoints = new int[skillPoints.Length];
        for (int i = 0; i < skillPoints.Length; i++)
        {
            SkillSlots[i].SetPoint(skillPoints[i]);
            tempPoints[i] = int.Parse(SkillSlots[i].Point.text);
        }
        if (_tzPoint!= int.Parse(TzPoint.text))
        {
            SavePoint();
        }
        else
        {
            tempSxPoint = int.Parse(SxPoint.text);
            tempTzPoint = int.Parse(TzPoint.text);
        }
    }
    public int GetPointByID(int id)
    {
        return int.Parse(SkillSlots[id].Point.text);
    }
    public bool CheckPoints(int id)
    {
        if (id >= 0 && id < 5)
        {
            int sxPoint = int.Parse(SxPoint.text);
            if (sxPoint <= 0)
            {
                return false;
            }
            sxPoint--;
            SxPoint.text = sxPoint.ToString();
        }
        if (id >= 5 && id < 25)
        {
            int tzPoint = int.Parse(TzPoint.text);
            if (tzPoint <= 0)
            {
                return false;
            }
            tzPoint--;
            TzPoint.text = tzPoint.ToString();
        }
        return true;
    }

    public void ResetPoint()
    {
        if (tempPoints==null)
        {
            Debug.LogWarning("未发现缓存技能点");
            return;
        }
        SetSkillPoints(tempPoints, tempSxPoint, tempTzPoint);
    }
    public void SavePoint()
    {
        int[] points = new int[25];
        for (int i = 0; i < SkillSlots.Length; i++)
        {
            points[i] = int.Parse(SkillSlots[i].Point.text);
        }
        PropertyManager.Instance.SaveSkillPoint(points);
    }
    public void CorrectPoint(int checkPoint)
    {
        if (checkPoint!=0 )
        {
            TzPoint.text = (int.Parse(TzPoint.text) + checkPoint).ToString();
        }    
    }
}