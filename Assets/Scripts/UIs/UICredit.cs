using System;
using UnityEngine;
using UnityEngine.UI;

public class UICredit : MonoBehaviour
{
    public event EventHandler OnPanelClose;
    [SerializeField] private Button _bpReturn;
    [SerializeField] private AudioElementSFX _OnOpenSFX;
    public void OpenPanel() {
        gameObject.SetActive(true);
        _OnOpenSFX.Play();
    }
    private void Start()
    {
        _bpReturn.onClick.AddListener(UIReturn);
    }

    private void UIReturn() {
        OnPanelClose?.Invoke(this, EventArgs.Empty);
        gameObject.SetActive(false);
    }
}