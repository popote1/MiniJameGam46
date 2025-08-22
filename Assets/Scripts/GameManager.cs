using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _tickRate = 0.25f;
    [SerializeField] private bool _doTicks = true;

    private float _timer ;
    private void Update() {
        ManagerTick();
    }

    private void ManagerTick() {
        if (!_doTicks) return;
        _timer += Time.deltaTime;
        if (_timer > _tickRate) {
            StaticEvent.DoGameTick();
            StaticEvent.DoLateGameTick();
            _timer -= _tickRate;
            Debug.Log("Tick");
        }
    }
}