using System.Linq;
using TMPro;
using UnityEngine;

public class DialogueButton : MonoBehaviour
{
    private Dialogue _dialogue;
    private GameState _gameState;

    private void Start()
    {
        _gameState = FindObjectsByType<GameState>(FindObjectsSortMode.None).First();
    }

    public void Init(Dialogue dialogue)
    {
        _dialogue = dialogue;

        var textComponent = GetComponentInChildren<TextMeshProUGUI>();
        textComponent.text = dialogue.PlayerText;
    }

    public void ClickDialogue()
    {
        _gameState.ResolvePlayerSelection(_dialogue);
    }
}
