using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour

{
    public BoardConfig BoardConfig;
    private GameObject _optionsPanel;
    private CanvasGroup _optionsGroup;
    private Resolution[] _screenResolutions;
    private TMP_Dropdown _resolutionDropDown;
    private List<string> _resolutionOptionsStr;
    private List<string> _tileTypeDropDownStr;
    private List<ImportantTypes.TileType> _tileTypeDropDownDefault;
    private bool _fullScreen;
    private TMP_Dropdown _tileTypeDropDown;
    private TMP_InputField _boardXSizeInputField;
    private TMP_InputField _boardYSizeInputField;

    private void Start()
    {
        _resolutionOptionsStr = new List<string>();
        _tileTypeDropDownStr = new List<string>();
        _tileTypeDropDownDefault = new List<ImportantTypes.TileType>();
        _optionsPanel = GameObject.FindWithTag("OptionsPanel");
        _optionsGroup = _optionsPanel.GetComponent<CanvasGroup>();
        _screenResolutions = Screen.resolutions;
        _resolutionDropDown = GameObject.FindWithTag("ResolutionDropDown").GetComponent<TMP_Dropdown>();
        _tileTypeDropDown = GameObject.FindWithTag("TileTypeDropDown").GetComponent<TMP_Dropdown>();
        _boardXSizeInputField = GameObject.FindWithTag("XBoardSizeInputField").GetComponent<TMP_InputField>();
        _boardYSizeInputField = GameObject.FindWithTag("YBoardSizeInputField").GetComponent<TMP_InputField>();
        _tileTypeDropDown.ClearOptions();
        _resolutionDropDown.ClearOptions();
        FeedDropDownWithScreenResolutions(_screenResolutions);
        FeedDropDownWithTileType();
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

    private void FeedDropDownWithTileType()
    {
        _tileTypeDropDownStr.Clear();
        
        _tileTypeDropDownStr.Add(ImportantTypes.TileType.Hexagon.ToString());
        _tileTypeDropDownStr.Add(ImportantTypes.TileType.Square.ToString());
        _tileTypeDropDownDefault.Add(ImportantTypes.TileType.Hexagon);
        _tileTypeDropDownDefault.Add(ImportantTypes.TileType.Square);
        _tileTypeDropDown.AddOptions(_tileTypeDropDownStr);
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

    public void ApplyTileType(int index)
    {
        ImportantTypes.TileType tileTypeToSet = _tileTypeDropDownDefault[index];
        BoardConfig.TileType = tileTypeToSet;
    }

    public void ApplyBoardXSize(string xSize)
    {
        BoardConfig.BoardSize.x = int.Parse(xSize);
    }

    public void ApplyBoardYSize(string ySize)
    {
        BoardConfig.BoardSize.y = int.Parse(ySize);
    }
    
    public void ApplyResolution(int index)
    {
        Resolution _resolutionToSet = _screenResolutions[index];
        Screen.SetResolution(_resolutionToSet.width,_resolutionToSet.height,_fullScreen);
    }
}