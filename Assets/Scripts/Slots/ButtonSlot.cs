using UnityEngine;
using UnityEngine.UI;

public class ButtonSlot : MonoBehaviour
{
    public string ValueName;
    private Button check;
    private bool isTrue = true;
    private Text title;

    private Image checkImage;

    private void Start()
    {
        check = transform.Find("Button").GetComponent<Button>();
        checkImage = check.transform.GetChild(0).GetComponent<Image>();
        title = transform.Find("Text").GetComponent<Text>();

        check.onClick.AddListener(delegate{ OnCheck(); });
    }
    public void OnCheck()
    {
        isTrue = !isTrue;
        checkImage.enabled = isTrue;
        if (transform.parent.GetComponent<SettingCommon>())
        {
            transform.parent.GetComponent<SettingCommon>().ChangeBool(ValueName, isTrue);
        }
        else if (transform.parent.parent.GetComponent<SettingVideo>())
        {
            transform.parent.parent.GetComponent<SettingVideo>().ChangeBool(ValueName, isTrue);
        }
    }
}