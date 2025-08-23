using UnityEngine;
using UnityEngine.UI;

public class HUDTestBuilding : MonoBehaviour {
    [SerializeField] private Button _bpAddStorage;
    [SerializeField] private Button _bpAddSawMill;
    [SerializeField] private Button _bpAddFarm;
    [SerializeField] private Button _bpAddHouse;

    [SerializeField]private Vector2 _minpos =Vector2.zero;
    [SerializeField]private Vector2 _maxpos = new Vector2(10, 10);

    [SerializeField]private GameObject _prfStorage;
    [SerializeField]private GameObject _prfSawMill;
    [SerializeField]private GameObject _prfFarm;
    [SerializeField] private GameObject _prfHouse;

    private void Start() {
        _bpAddStorage.onClick.AddListener(BuildStorage);
        _bpAddFarm.onClick.AddListener(BuildFarm);
        _bpAddSawMill.onClick.AddListener(BuildingSawMill);
        _bpAddHouse.onClick.AddListener(BuildHouse);
    }

    private void BuildFarm() {
        Instantiate(_prfFarm, GetRandomPos(), Quaternion.identity);
    }

    private void BuildingSawMill() {
        Instantiate(_prfSawMill, GetRandomPos(), Quaternion.identity);
    }

    private void BuildStorage() {
        Instantiate(_prfStorage, GetRandomPos(), Quaternion.identity);
    }
    private void BuildHouse() {
        Instantiate(_prfHouse, GetRandomPos(), Quaternion.identity);
    }
    

    private Vector2 GetRandomPos() {
        return new Vector2(Random.Range(_minpos.x, _maxpos.x), Random.Range(_minpos.y, _maxpos.y));
    }
}