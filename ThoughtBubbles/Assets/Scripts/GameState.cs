using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    // Game setup
    [SerializeField] List<string> CharacterOrder = new() { "Biv" };
    [SerializeField] UIController UIController;

    // Character state
    public int Stress = 0;

    // Game state
    private int _currentOpponent = 0;
    private int _currentOpponentInteraction = 0;
    private int _currentOpponentPositiveInteractions = 0;
    private Dialogue _lastResolvedDialogue;
    private Character _currentCharacter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitCharacter();
    }

    public void GoToNextInteraction()
    {
        CheckScreenShake();
        ResolveGameOver();

        if (!HasNextInteraction())
        {
            Debug.Log("Reached end of character!");
            return;
        }

        UIController.GoToDialogueView();

        var interaction = GetNextInteraction();
        var options = interaction.GetResponses();
        foreach (var dialogue in options)
        {
            UIController.AddDialogueToUI(dialogue);
        }

        UIController.SetPrompt(interaction.Prompt);
    }

    public void ResolvePlayerSelection(Dialogue dialogue)
    {
        _lastResolvedDialogue = dialogue;

        Stress += dialogue.StressToAdd;
        if (dialogue.IsPositiveOption)
        {
            _currentOpponentPositiveInteractions++;
        }

        UIController.ClearDialogueButtons();

        UIController.GoToSideBySideView(dialogue, _currentCharacter);
    }

    public void ResolvePlayerSpeech()
    {
        UIController.GoToOpponentSpeech();
    }

    private void ResolveGameOver()
    {
        if (Stress >= 100)
        {
            UIController.GameOverTransition();
        }
    }

    private void InitCharacter()
    {
        _currentCharacter = GameObject.Find(CharacterOrder[_currentOpponent]).GetComponent<Character>();
        GoToNextInteraction();
    }

    private bool HasNextInteraction() => _currentOpponentInteraction < _currentCharacter.GetInteractions().Count;

    private Interaction GetNextInteraction()
    {
        var currentInteraction = _currentCharacter.GetInteractions()[_currentOpponentInteraction];
        _currentOpponentInteraction++;
        return currentInteraction;
    }

    private void CheckScreenShake()
    {
        if (!_lastResolvedDialogue?.IsPositiveOption ?? false)
        {
            if (_lastResolvedDialogue.StressToAdd > 5)
            {
                UIController.ScreenShakeBig();
            }
            else
            {
                UIController.ScreenShakeSmall();
            }
        }
    }
}
