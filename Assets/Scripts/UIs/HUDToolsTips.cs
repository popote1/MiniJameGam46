using UnityEngine;
using UnityEngine.InputSystem;

public class HUDToolsTips : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private HUDHouseToolTips _panelHouse;
    [SerializeField] private HUDWorkingPlaceToolTips _panelWorkingBuilding;

    // Update is called once per frame
    void Update() {

        //RaycastHit hit;
        //if (Physics.Raycast(_camera.ScreenPointToRay(Mouse.current.position.value), out hit)) {
        //    if( hit.collider.GetComponent<House>()) {
        //        _panelHouse.transform.position = Mouse.current.position.value;
        //        _panelHouse.DisplayHouseInfo(hit.collider.GetComponent<House>());
        //        Debug.Log("DisplayPanel House");
        //    }
        //    else {
        //        _panelHouse.HidePanel();
        //    }

        //    //if (hit.collider.GetComponent<WorkingBuilding>()) {
        //    //    _panelWorkingBuilding.transform.position = Mouse.current.position.value;
        //    //    _panelWorkingBuilding.DisplayHouseInfo(hit.collider.GetComponent<WorkingBuilding>());
        //    //    Debug.Log("DisplayPanel building");
        //    //}
        //    else {
        //        _panelWorkingBuilding.HidePanel();
        //    }
        //}
        //else
        //{
        //    _panelWorkingBuilding.HidePanel();
        //    _panelHouse.HidePanel();
        //}
    }
}