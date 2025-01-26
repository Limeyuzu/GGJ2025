using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StressBubbleOverlayController : MonoBehaviour
{
    [SerializeField] Image Image;
    [SerializeField] float MaxAlpha;

    private GameState _gameState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameState = FindObjectsByType<GameState>(FindObjectsSortMode.None).First();
    }

    // Update is called once per frame
    void Update()
    {
        Image.color = new Color(1, 1, 1, MaxAlpha * _gameState.Stress / 100f);
    }
}
