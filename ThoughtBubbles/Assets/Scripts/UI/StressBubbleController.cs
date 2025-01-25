using System.Linq;
using UnityEngine;

public class StressBubbleController : MonoBehaviour
{
    [SerializeField] float StressScaleFactor = 0.15f;

    private GameState _gameState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameState = FindObjectsByType<GameState>(FindObjectsSortMode.None).First();
    }

    // Update is called once per frame
    void Update()
    {
        var bubbleSize = StressScaleFactor * (_gameState.Stress + 1);
        transform.localScale = new Vector3(bubbleSize, bubbleSize, 1);
    }
}
