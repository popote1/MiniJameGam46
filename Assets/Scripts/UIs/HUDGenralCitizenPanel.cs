using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDGenralCitizenPanel : MonoBehaviour {
    [SerializeField] private TMP_Text _txtCitizenCount;
    [SerializeField] private Button _bpMorInfo;
    [Space(10)]
    [SerializeField] private GameObject _extendePanel;
    [SerializeField] private TMP_Text _txtSickCitizen;
    [SerializeField] private TMP_Text _txtDeadCitizen;
    [SerializeField] private TMP_Text _txtCurringCitizen;

    private bool _isExtented;

    private void Awake() {
        _bpMorInfo.onClick.AddListener(UIMoreInfo);
    }

    private void UIMoreInfo() {
        if (_isExtented) {
            _extendePanel.SetActive(false);
            _isExtented = false;
        }
        else {
            _extendePanel.SetActive(true);
            _isExtented = true;  
        }
    }

    private void Update() {
        _txtCitizenCount.text = StaticData.GetCitizenCount.ToString();
        if (_isExtented) {
            _txtSickCitizen.text = StaticData.GetSickCitizen().Count.ToString();
            _txtDeadCitizen.text = StaticData.GetDeadCitizen().Count.ToString();
            _txtCurringCitizen.text = StaticData.GetCurringCitizen().Count.ToString();
        }
    }
}