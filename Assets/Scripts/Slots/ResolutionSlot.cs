using UnityEngine;
using UnityEngine.UI;

public class ResolutionSlot : MonoBehaviour
{
    private Dropdown resolution;

    private void Start()
    {
        resolution = transform.Find("Dropdown").GetComponent<Dropdown>();
        resolution.onValueChanged.AddListener(delegate { ChangeResolution(resolution); });
    }
    void ChangeResolution(Dropdown dropdown)
    {
        Debug.Log(dropdown.value);
        switch (dropdown.value)
        {
            case 0:
                Screen.SetResolution(1920, 1200, true);
                break;
            case 1:
                Screen.SetResolution(1440, 900, true);
                break;
            case 2:
                Screen.SetResolution(1280, 800, true);
                break;
            case 3:
                Screen.SetResolution(1024, 600, true);
                break;
            case 4:
                Screen.SetResolution(800, 480, true);
                break;
        }
    }
}