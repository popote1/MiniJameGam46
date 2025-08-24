using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDContructionInfoPanel : MonoBehaviour
{

    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _txtBuildingName;
    [SerializeField] private TMP_Text _txtGoldCost;
    [SerializeField] private TMP_Text _txtWoodCost;
    [SerializeField] private TMP_Text _txtDescription;
    [SerializeField] private Image _imgIcon;
    [SerializeField] private AudioElementSFX _aesOnOpenPanel;
    [SerializeField] private BuildingInfo[] _buildingInfos;
    [Serializable]
    private struct BuildingInfo
    {
        public Cell.TileType type;
        public Sprite ImgIcon;
        [TextArea] public String Txtdescription;
    }
    private void Start() {
        StaticEvent.OnStartBuilding+= StaticEventOnOnStartBuilding;
    }

    private void OnDestroy()
    {
        StaticEvent.OnStartBuilding-= StaticEventOnOnStartBuilding;
    }

    private void StaticEventOnOnStartBuilding(object sender, Building e) {
        if (e == null) {
            _panel.SetActive(false);
            return;
        }
        _panel.SetActive(true);
        _aesOnOpenPanel.Play();
        _txtBuildingName.text = e.name;
        _txtGoldCost.text = e.goldCost.ToString();
        _txtWoodCost.text = e.woodCost.ToString();
        BuildingInfo info =GetBuildingInfo(e.type);
        _imgIcon.sprite = info.ImgIcon;
        _txtDescription.text = info.Txtdescription;
    }

    private BuildingInfo GetBuildingInfo(Cell.TileType type) {
        foreach (var info in _buildingInfos) {
            if (info.type == type) return info;
        }

        return new BuildingInfo();
    }
}