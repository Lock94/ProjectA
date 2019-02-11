using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputSlot : MonoBehaviour
{
    public string InputName;
    private Text title;
    private Button inputButton;
    private Text inputText;

    public KeyCode InputKey;
    private KeyCode currentKey;
    private KeyCode newKey;
    bool isWaitingForKey = false;
    bool isPlaying = true;
    string functionName = string.Empty;

    public void Start()
    {
        title = transform.Find("Text").GetComponent<Text>();
        inputButton = transform.Find("Button").GetComponent<Button>();
        inputText = inputButton.transform.GetChild(0).GetComponent<Text>();

        inputButton.onClick.AddListener(InputBtnClick);
    }

    void InputBtnClick()
    {
        isPlaying = false;
        isWaitingForKey = true;
        inputText.text = "<color=yellow>请输入</color>";
    }

    void OnGUI()
    {
        if (isWaitingForKey)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                newKey = e.keyCode;
                inputText.text = string.Format("<color=white>{0}</color>",newKey.ToString());
                PlayerPrefs.SetString(functionName, newKey.ToString());
                isWaitingForKey = false;
                currentKey = newKey;
                Debug.Log(currentKey);
                StartCoroutine(WaitUpdate());
            }
        }
    }
    IEnumerator WaitUpdate()
    {
        yield return new WaitForEndOfFrame();
        isPlaying = true;
    }
}