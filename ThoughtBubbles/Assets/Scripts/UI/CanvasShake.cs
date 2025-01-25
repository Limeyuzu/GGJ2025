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

    // Update is called once per frame
    void Update()
    {
        transform.position = _initialPosition + (Vector3)(Random.insideUnitCircle * _uiController.ShakeStrength);
        if (_uiController.ShakeStrength > 0)
        {
            Mathf.Clamp(_uiController.ShakeStrength -= 10 * Time.deltaTime, 0, 1);
        }
    }
}
