using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlot : CraftSlot
{
    private Text m_Point;
    public Text Point
    {
        get
        {
            if (m_Point == null)
            {
                m_Point = transform.Find("Point").Find("Text").GetComponent<Text>();
            }
            return m_Point;
        }
    }
    public int SkillID { get; set; }

    public void SetPoint(int point)
    {
        point = CheckPoints(point);
        Point.text = point.ToString();
    }

    public void AddPoint(int point=1)
    {
        Point.text = (int.Parse(Point.text) + point).ToString();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        //TODO JIA DIAN
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (CanAddPoint())
            {
                AddPoint();
            }
            else
            {
                Debug.Log("已到达上限");
            }
        }
    }
    public override void SetItem(Item item, int amount = 1)
    {
        if (item.Type!=Item.ItemType.Skill)
        {
            Debug.LogWarning("赋值的物品不是Skill类");
            return;
        }
        if (transform.childCount==1)
        {
            GameObject itemGO = Instantiate(ItemPrefab) as GameObject;
            itemGO.transform.SetParent(this.transform);
            itemGO.transform.localPosition = Vector3.zero;
            itemGO.GetComponent<ItemUI>().SetItemUI(item, amount);
            itemGO.GetComponent<ItemUI>().targetScale = 1.45f;
            itemGO.GetComponent<ItemUI>().useAnimation = false;
            itemGO.transform.SetSiblingIndex(0);
        }
    }
    private bool CanAddPoint()
    {
        Item item = InventoryManager.Instance.GetItemByID(SkillID + 343);
        if (!(item is Skill))
        {
            Debug.LogWarning("非技能类的物品");
            return false;
        }
        Skill skill = (Skill)item;
        if (int.Parse(Point.text)>= skill.MaxPoint)
        {
            //加不了点了
            return false;
        }
        for (int i = 0; i < skill.PreSkillID.Length; i++)
        {
            int pointID = skill.PreSkillID[i] - 343;
            int eachPoint = skill.EachSkillNeed[i];

            int maxPoint = SkillPanel.Instance.GetPointByID(pointID) / eachPoint;
            if (int.Parse(Point.text)>= maxPoint)
            {
                return false;
            }
        }
        if (!SkillPanel.Instance.CheckPoints(SkillID))
        {
            return false;
        }
        return true;
    }
    private int CheckPoints(int points)
    {
        Skill skill = (Skill)InventoryManager.Instance.GetItemByID(SkillID + 343);

        if (points>= skill.MaxPoint)
        {
            return skill.MaxPoint;
        }
        for (int i = 0; i < skill.PreSkillID.Length; i++)
        {
            int pointID = skill.PreSkillID[i] - 343;
            int eachPoint = skill.EachSkillNeed[i];

            int maxPoint = SkillPanel.Instance.GetPointByID(pointID) / eachPoint;
            if (int.Parse(Point.text) > maxPoint)
            {
                int checkPoint = int.Parse(Point.text) - maxPoint;
                SkillPanel.Instance.CorrectPoint(checkPoint);
                return maxPoint;
            }
        }
        return points;
    }
}