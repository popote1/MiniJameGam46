using System;
using UnityEngine;
using UnityEngine.UI;

public class UICredit : MonoBehaviour
{
    public event EventHandler OnPanelClose;
    [SerializeField] private Button _bpReturn;
    public void OpenPanel() {
        gameObject.SetActive(true);
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