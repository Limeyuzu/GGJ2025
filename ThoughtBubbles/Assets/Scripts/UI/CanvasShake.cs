using System.Linq;
using UnityEngine;

public class CanvasShake : MonoBehaviour
{
    private UIController _uiController;
    private Vector3 _initialPosition;

    private void Start()
    {
        _uiController = FindObjectsByType<UIController>(FindObjectsSortMode.None).First();
        _initialPosition = transform.position;
    }

    // This feels like a terrible way to do camera shake, but it's the canvas that needs to shake not the game world
    // For now this is all I can do for a game jam
    void Update()
    {
        transform.position = _initialPosition + (Vector3)(Random.insideUnitCircle * _uiController.ShakeStrength);
        if (_uiController.ShakeStrength > 0)
        {
            Mathf.Clamp(_uiController.ShakeStrength -= 10 * Time.deltaTime, 0, 1);
        }
    }
}
