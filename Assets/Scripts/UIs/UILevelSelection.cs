using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevelSelection :MonoBehaviour
{

    public event EventHandler OnPanelClose;
    [Serializable]
    public struct LevelInfoData {
        public string LevelName;
        [TextArea] public string Description;
        public string SceneName;
    }

    [SerializeField] private Transform _buttonHolder;
    [SerializeField] private UILevelSelectionButton _prfUILevelSelectionButton;
    
    [Space(10), Header("LevelInfo;")] 
    [SerializeField] private TMP_Text _txtLevelName;
    [SerializeField] private TMP_Text _txtLeveldescription;
    [SerializeField] private Button _bpPlay;
    [SerializeField] private Button _bpReturn;

    [SerializeField] private LevelInfoData[] _levelInfoDatas;

    private List<UILevelSelectionButton> _buttons = new List<UILevelSelectionButton>();
    private LevelInfoData _selectedData;


    private void Start() {
        _bpPlay.onClick.AddListener(UIPLay);
        _bpReturn.onClick.AddListener(UIReturn);
    }

    public void OpenPanel() {
        gameObject.SetActive(true);
        PopulateButtons();
        _txtLevelName.text = "";
        _txtLeveldescription.text = "";
        _bpPlay.interactable = false;
        _selectedData = new LevelInfoData();
    }

    private void PopulateButtons() {
        foreach (LevelInfoData data in _levelInfoDatas) {
            UILevelSelectionButton button  =Instantiate(_prfUILevelSelectionButton, _buttonHolder);
            button.SetUpLevelData(data);
            button.OnLevelSelection+= ButtonOnOnLevelSelection;
            _buttons.Add(button);
        }
    }

    private void ButtonOnOnLevelSelection(object sender, LevelInfoData e) {
        _selectedData = e;
        _txtLevelName.text = _selectedData.LevelName;
        _txtLeveldescription.text = _selectedData.Description;
        _bpPlay.interactable = true;
    }

    private void ClearButtons() {
        for (int i = _buttons.Count-1; i < 0; i++) {
            _buttons[i].OnLevelSelection -= ButtonOnOnLevelSelection;
            Destroy(_buttons[i].gameObject);
        }
        _buttons.Clear();
        
    }

    
    private void UIPLay() {
        if (string.IsNullOrEmpty(_selectedData.SceneName)) return;
        SceneManager.LoadScene(_selectedData.SceneName);
    }

    private void UIReturn() {
        OnPanelClose?.Invoke(this , EventArgs.Empty);
        ClearButtons();
        gameObject.SetActive(false);
    }

}