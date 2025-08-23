using System;
using UnityEngine;
using UnityEngine.UI;

public class HUDPauseMenu : MonoBehaviour
{
    public event EventHandler OnPanelCLose; 
    [SerializeField] private Button _bpReturn;
    [SerializeField] private Button _bpOption;
    [SerializeField] private Button _bpRestart;
    [SerializeField] private Button _bpMainMenu;

    [SerializeField] private UIOption _uiOption;
    [SerializeField] private HUDRestart _hudRestart;
    [SerializeField] private HUDReturnToMainMenu _hudReturnToMainMenu;


    public void OpenPanel()
    {
        gameObject.SetActive(true);
        _bpReturn.Select();
    }
    private void Start() {
        _bpReturn.onClick.AddListener(UIReturn);
        _bpOption.onClick.AddListener(UIOption);
        _bpRestart.onClick.AddListener(UIRestart);
        _bpMainMenu.onClick.AddListener(UIReturnToMainMenu);
        
        _uiOption.OnPanelClose+= UiOptionOnOnPanelClose;
        _hudRestart.OnPanelClose+= HudRestartOnOnPanelClose;
        _hudReturnToMainMenu.OnPanelClose += HudReturnToMainMenuOnOnPanelClose;
    }

    private void HudReturnToMainMenuOnOnPanelClose(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        _bpMainMenu.Select();
        
    }

    private void HudRestartOnOnPanelClose(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        _bpRestart.Select();
    }

    private void UiOptionOnOnPanelClose(object sender, EventArgs e) {
        gameObject.SetActive(true);
        _bpOption.Select();
    }

    private void UIReturnToMainMenu() {
        _hudReturnToMainMenu.OpenPanel();
        gameObject.SetActive(false);
    }

    private void UIOption() {
        _uiOption.OpenPanel();
        gameObject.SetActive(false);
    }

    private void UIRestart() {
        _hudRestart.OpenPanel();
        gameObject.SetActive(false);
    }

    private void UIReturn() {
        OnPanelCLose?.Invoke(this, EventArgs.Empty);
        gameObject.SetActive(false);
    }
    
}