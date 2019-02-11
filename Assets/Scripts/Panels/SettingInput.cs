using UnityEngine;
using UnityEngine.UI;

public class SettingInput : BigPanel
{
    private GameObject CommonSetting;
    private GameObject TeamSetting;

    private Button TeamBtn;
    private Button CommonBtn;

    private Button inputPanel;
    private Button videoPanel;
    private Button commonPanel;

    public override void Start()
    {
        base.Start();
        inputPanel = transform.Find("Title").transform.Find("Input").GetComponent<Button>();
        videoPanel = transform.Find("Title").transform.Find("Video").GetComponent<Button>();
        commonPanel = transform.Find("Title").transform.Find("Common").GetComponent<Button>();

        CommonSetting = transform.Find("CommonSetting").gameObject;
        TeamSetting = transform.Find("TeamSetting").gameObject;

        TeamBtn = CommonSetting.transform.Find("TeamBtn").GetComponent<Button>();
        CommonBtn = TeamSetting.transform.Find("CommonBtn").GetComponent<Button>();

        TeamBtn.onClick.AddListener(TeamAwake);
        CommonBtn.onClick.AddListener(CommonAwake);

        inputPanel.onClick.AddListener(delegate { InputAwake("Input"); });
        videoPanel.onClick.AddListener(delegate { InputAwake("Video"); });
        commonPanel.onClick.AddListener(delegate { InputAwake("Common"); });

        CommonAwake();
    }

    void InputAwake(string panelType)
    {
        transform.parent.SendMessage("AwakePanel", panelType);
    }

    private void CommonAwake()
    {
        CommonSetting.SetActive(true);
        TeamSetting.SetActive(false);
    }

    private void TeamAwake()
    {
        CommonSetting.SetActive(false);
        TeamSetting.SetActive(true);
    }

    public void ChangeValue(string valueName, float value)
    {
        //TODO 改变对应属性的值
        Debug.Log(valueName + value);
    }
}