using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILevelSelectionButton : MonoBehaviour
{
    public event EventHandler<UILevelSelection.LevelInfoData> OnLevelSelection;
    
    [SerializeField] private TMP_Text _txtLabel;
    [SerializeField] private Button _button;
    
    
    private UILevelSelection.LevelInfoData _levelInfoData;
    public void SetUpLevelData(UILevelSelection.LevelInfoData info)
    {
        _levelInfoData = info;
        _button.onClick.AddListener(OnLevelSelected);
        _txtLabel.text = _levelInfoData.LevelName;
    }

    private void OnLevelSelected() {
        OnLevelSelection?.Invoke(this, _levelInfoData);
    }
}