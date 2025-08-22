using UnityEngine;

public class Warehouse :MonoBehaviour {
    [SerializeField] private int _foodStockAdded = 20;
    [SerializeField] private int _woodStockAdded = 30;

    private void Start() {
        StaticData.ChangeFoodStockValue(_foodStockAdded);
        StaticData.ChangeWoodStockValue(_woodStockAdded);
    }

    private void OnDestroy() {
        StaticData.ChangeFoodStockValue(-_foodStockAdded);
        StaticData.ChangeWoodStockValue(-_woodStockAdded);
    }
}