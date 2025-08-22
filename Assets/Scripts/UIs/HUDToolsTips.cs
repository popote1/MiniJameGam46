using UnityEngine;
using UnityEngine.InputSystem;

public class HUDToolsTips : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _panel;

    // Update is called once per frame
    void Update() {
        _panel.transform.position = Mouse.current.position.value;

        RaycastHit hit;
        if (Physics.Raycast(_camera.ScreenPointToRay(Mouse.current.position.value), out hit)) {
            if( hit.collider.GetComponent<House>())
            {
                
            }

            if (hit.collider.GetComponent<WorkingBuilding>())
            {
                
            }
        }
    }
}
