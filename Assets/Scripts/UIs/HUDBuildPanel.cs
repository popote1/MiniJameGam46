using UnityEngine;
using UnityEngine.UI;

public class HUDBuildPanel : MonoBehaviour{
    [SerializeField] private Button _bpSmallHouse;
    [SerializeField] private Button _bpBigHouse;
    [SerializeField] private Button _bpFarm;
    [SerializeField] private Button _bpSawMill;
    [SerializeField] private Button _bpWareHouse;
    [SerializeField] private Button _bpFishingDock;
    [SerializeField] private Button _bpMerchantDock;
    [SerializeField] private Button _bpInfermery;
    [SerializeField] private Button _bpDestruc;

    [Space(10)]
    [SerializeField] private BuildingCost _costSmallHouse;
    [SerializeField] private BuildingCost _costBigHouse;
    [SerializeField] private BuildingCost _costFarm;
    [SerializeField] private BuildingCost _costSawMill;
    [SerializeField] private BuildingCost _costWareHouse;
    [SerializeField] private BuildingCost _costFishingDocks;
    [SerializeField] private BuildingCost _costMerchantDocks;
    [SerializeField] private BuildingCost _costInfermery;

    private void Start() {
        _bpSmallHouse.onClick.AddListener(UIBuildSmallBuilding);
        _bpBigHouse.onClick.AddListener(UIBuildBigBuilding);
        _bpFarm.onClick.AddListener(UIBuildFarmlBuilding);
        _bpSawMill.onClick.AddListener(UIBuildSawMillBuilding);
        _bpWareHouse.onClick.AddListener(UIBuildWarehouseBuilding);
        _bpFishingDock.onClick.AddListener(UIBuildFishingDockBuilding);
        _bpMerchantDock.onClick.AddListener(UIBuildMerchantDockBuilding);
        _bpInfermery.onClick.AddListener(UIBuildInfiermeryBuilding);
        _bpDestruc.onClick.AddListener(UIBuildDestructionBuilding);
    }

    private void Update() {
        if (StaticData.CurrentGameStat == StaticData.GameStat.Playing) {
            _bpSmallHouse.interactable = _costSmallHouse.CanBeBuild();
            _bpBigHouse.interactable = _costBigHouse.CanBeBuild();
            _bpFarm.interactable = _costFarm.CanBeBuild();
            _bpSawMill.interactable = _costSawMill.CanBeBuild();
            _bpWareHouse.interactable = _costWareHouse.CanBeBuild();
            _bpFishingDock.interactable = _costFishingDocks.CanBeBuild();
            _bpMerchantDock.interactable = _costMerchantDocks.CanBeBuild();
            _bpInfermery.interactable = _costInfermery.CanBeBuild();
            _bpDestruc.interactable = true;
        }
        else {
            _bpSmallHouse.interactable = false;
            _bpBigHouse.interactable = false;
            _bpFarm.interactable = false;
            _bpSawMill.interactable = false;
            _bpWareHouse.interactable = false;
            _bpFishingDock.interactable = false;
            _bpMerchantDock.interactable = false;
            _bpInfermery.interactable = false;
            _bpDestruc.interactable = false;
        }
    }

    private void UIBuildSmallBuilding() { }
    private void UIBuildBigBuilding() { }
    private void UIBuildFarmlBuilding() { }
    private void UIBuildSawMillBuilding() { }
    private void UIBuildWarehouseBuilding() { }
    private void UIBuildFishingDockBuilding() { }
    private void UIBuildMerchantDockBuilding() { }
    private void UIBuildInfiermeryBuilding() { }
    private void UIBuildDestructionBuilding() { }
}