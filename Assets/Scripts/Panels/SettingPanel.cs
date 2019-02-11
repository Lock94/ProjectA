using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BigPanel
{
    #region 单例模式
    private static SettingPanel _instance;
    public static SettingPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("SettingPanel").GetComponent<SettingPanel>();
            }
            return _instance;
        }
    }
    #endregion

    private Button backBtn;

    private SettingCommon settingCommon;
    private SettingInput settingInput;
    private SettingVideo settingVideo;


    public override void Start()
    {
        backBtn = transform.Find("Back").GetComponent<Button>();

        settingCommon = transform.Find("SettingCommon").GetComponent<SettingCommon>();
        settingInput = transform.Find("SettingInput").GetComponent<SettingInput>();
        settingVideo = transform.Find("SettingVideo").GetComponent<SettingVideo>();

        backBtn.onClick.AddListener(Hide);
        base.Start();
    }

    public override void Show()
    {
        base.Show();
        AwakePanel("Common");
    }

    public void AwakePanel(string panelType)
    {
        switch (panelType)
        {
            case "Input":
                settingInput.Show();
                settingVideo.Hide();
                settingCommon.Hide();
                break;
            case "Common":
                settingInput.Hide();
                settingVideo.Hide();
                settingCommon.Show();
                break;
            case "Video":
                settingInput.Hide();
                settingVideo.Show();
                settingCommon.Hide();
                break;
            default:
                break;
        }
    }
}