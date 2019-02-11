using UnityEngine;
using UnityEngine.UI;

public class SettingCommon : BigPanel 
{
    private Button inputPanel;
    private Button videoPanel;
    private Button commonPanel;

    public override void Start()
    {
        base.Start();
        inputPanel = transform.Find("Title").transform.Find("Input").GetComponent<Button>();
        videoPanel = transform.Find("Title").transform.Find("Video").GetComponent<Button>();
        commonPanel = transform.Find("Title").transform.Find("Common").GetComponent<Button>();

        inputPanel.onClick.AddListener(delegate { InputAwake("Input"); });
        videoPanel.onClick.AddListener(delegate { InputAwake("Video"); });
        commonPanel.onClick.AddListener(delegate { InputAwake("Common"); });
    }

    void InputAwake(string panelType)
    {
        transform.parent.SendMessage("AwakePanel", panelType);
    }

    public void ChangeValue(string valueName,float value)
    {
        //TODO 改变对应属性的值
        Debug.Log(valueName + value);
    }
    public void ChangeBool(string valueName, bool value)
    {
        Debug.Log(valueName + value);
    }
}