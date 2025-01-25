using System.Linq;
using UnityEngine;

public class SideBySideScreenButton : MonoBehaviour
{
    private GameState _gameState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameState = FindObjectsByType<GameState>(FindObjectsSortMode.None).First();
    }

    public void OnClick()
    {
        _gameState.GoToNextInteraction();
    }
}
