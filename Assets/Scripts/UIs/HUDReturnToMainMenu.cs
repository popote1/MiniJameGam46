using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDReturnToMainMenu : MonoBehaviour {
    public event EventHandler OnPanelClose;
    [SerializeField] private Button _bpReturn;
    [SerializeField] private Button _bpReturnToMainMenu;

    private void Start() {
        _bpReturnToMainMenu.onClick.AddListener(UIRestart);
        _bpReturn.onClick.AddListener(UIReturn);
    }

    public void OpenPanel() {
        gameObject.SetActive(true);
        _bpReturn.Select();
    }

    private void UIRestart() {
        SceneManager.LoadScene(0);
    }

    private void UIReturn() {
        OnPanelClose?.Invoke(this , EventArgs.Empty);
        gameObject.SetActive(false);
    }
    
}