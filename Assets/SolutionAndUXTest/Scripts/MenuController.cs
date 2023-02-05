using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour

{
    private GameObject _optionsPanel;
    private CanvasGroup _optionsGroup;
    private Resolution[] _screenResolutions;
    private TMP_Dropdown _resolutionDropDown;
    private List<string> _resolutionOptionsStr;
    private bool _fullScreen;
     
    private void Start()
    {
        _resolutionOptionsStr = new List<string>();
        _optionsPanel = GameObject.FindWithTag("OptionsPanel");
        _optionsGroup = _optionsPanel.GetComponent<CanvasGroup>();
        _screenResolutions = Screen.resolutions;
        _resolutionDropDown = GameObject.FindWithTag("ResolutionDropDown").GetComponent<TMP_Dropdown>();
        _resolutionDropDown.ClearOptions();
        FeedDropDownWithScreenResolutions(_screenResolutions);
        FindCurrentResolution();
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
        _fullScreen = fullScreen;
    }
    
    public void SetVsync(bool Vsync)
    {
        QualitySettings.vSyncCount = Vsync ? 1 : 0;
    }

    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync(1);
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

    private void FindCurrentResolution()
    {
        for (int i = 0; i < _screenResolutions.Length; i++)
        {
            if (_screenResolutions[i].height == Screen.currentResolution.height && _screenResolutions[i].width == Screen.currentResolution.width)
            {
                _resolutionDropDown.value = i;
                return;
            }
        }
    }

    public void ApplyResolution(int index)
    {
        Resolution _resolutionToSet = _screenResolutions[index];
        Screen.SetResolution(_resolutionToSet.width,_resolutionToSet.height,_fullScreen);
    }
}