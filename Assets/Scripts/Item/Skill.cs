using UnityEngine;

public class Skill : Item
{
    /// <summary>
    /// 技能ID
    /// </summary>
    public int SkillID { get; set; }
    /// <summary>
    /// 技能最大点
    /// </summary>
    public int MaxPoint { get; set; }
    /// <summary>
    /// 技能描述
    /// </summary>
    public string SkillDescription { get; set; }
    /// <summary>
    /// 需要的前置技能ID
    /// </summary>
    public int[] PreSkillID { get; set; }
    /// <summary>
    /// 每点需要的前置ID数目
    /// </summary>
    public int[] EachSkillNeed { get; set; }

    public Skill(int id, string name, string description, ItemType type, ItemTechnology technology, int value, int maxStark, string sprite,int skillID,int maxPoint,string skillDescription,int[] preSkillID,int[] eachSkillNeed) : base(id, name, description, type, technology, value, maxStark, sprite)
    {
        this.SkillID = skillID;
        this.MaxPoint = maxPoint;
        this.SkillDescription = skillDescription;
        this.PreSkillID = preSkillID;
        this.EachSkillNeed = eachSkillNeed;
    }
    public override string GetToolTipText()
    {
        string text = string.Format("<size=18><color=white>{0}</color></size>\n<size=10>最大点数： {3}</size>\n" +
                                    "------------------------------\n" +
                                    "<size=13>{1}</size>\n" +
                                    "------------------------------\n" +
                                    "<size=13>{2}</size>\n",
                                    Name, Description, SkillDescription,MaxPoint);
        text = System.Text.RegularExpressions.Regex.Unescape(text);
        return text;
    }
}
