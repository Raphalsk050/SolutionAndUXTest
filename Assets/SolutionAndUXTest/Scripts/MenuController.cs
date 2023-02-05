using System.Collections.Generic;
using TMPro;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour

{
    private GameObject _optionsPanel;
    private CanvasGroup _optionsGroup;
    private Resolution[] _screenResolutions;
    private TMP_Dropdown _resolutionDropDown;
    private List<string> _resolutionOptionsStr;
     
    private void Start()
    {
        _resolutionOptionsStr = new List<string>();
        _optionsPanel = GameObject.FindWithTag("OptionsPanel");
        _optionsGroup = _optionsPanel.GetComponent<CanvasGroup>();
        _screenResolutions = Screen.resolutions;
        _resolutionDropDown = GameObject.FindWithTag("ResolutionDropDown").GetComponent<TMP_Dropdown>();
        _resolutionDropDown.ClearOptions();
        FeedDropDownWithScreenResolutions(_screenResolutions);
    }

    public void CloseOptions()
    {
        _optionsGroup.alpha = 0;
        _optionsGroup.interactable = false;
        _optionsGroup.blocksRaycasts = false;
    }

    public void OpenOptions()
    {
        _optionsGroup.alpha = 1;
        _optionsGroup.interactable = true;
        _optionsGroup.blocksRaycasts = true;
    }

    public void SetFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }
    
    public void SetVsync(bool Vsync)
    {
        QualitySettings.vSyncCount = Vsync ? 1 : 0;
    }

    private void FeedDropDownWithScreenResolutions(Resolution[] resolutions)
    {
        _resolutionOptionsStr.Clear();
        foreach (var resolution in resolutions)
        {
            string optionName = resolution.width + "x" + resolution.height;
            _resolutionOptionsStr.Add(optionName);
        }
        
        _resolutionDropDown.AddOptions(_resolutionOptionsStr);
        
    }
}