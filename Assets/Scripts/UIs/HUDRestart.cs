using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDRestart : MonoBehaviour {
    public event EventHandler OnPanelClose;
    [SerializeField] private Button _bpReturn;
    [SerializeField] private Button _bpRestart;

    private void Start() {
        _bpRestart.onClick.AddListener(UIRestart);
        _bpReturn.onClick.AddListener(UIReturn);
    }

    public void OpenPanel() {
        gameObject.SetActive(true);
        _bpReturn.Select();
    }

    private void UIRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UIReturn() {
        OnPanelClose?.Invoke(this , EventArgs.Empty);
        gameObject.SetActive(false);
    }
}