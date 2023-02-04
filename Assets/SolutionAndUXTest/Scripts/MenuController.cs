using UnityEngine;

public class MenuController : MonoBehaviour

{
    private GameObject _optionsPanel;
    private CanvasGroup _optionsGroup;

    private void Start()
    {
        _optionsPanel = GameObject.FindWithTag("OptionsPanel");
        _optionsGroup = _optionsPanel.GetComponent<CanvasGroup>();
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
}