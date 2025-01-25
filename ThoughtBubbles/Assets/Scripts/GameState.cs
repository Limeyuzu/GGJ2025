using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    // Game setup
    [SerializeField] List<string> CharacterOrder = new() { "Biv" };
    [SerializeField] UIController UIController;

    // Game state
    public int CurrentOpponent = 0;
    public int CurrentOpponentInteraction = 0;
    public int CurrentOpponentPositiveInteractions = 0;
    public Character CurrentCharacter;
    public string PreviousDialogueResponse = "";

    // Character state
    public int Stress = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitCharacter();
    }

    public void GoToNextInteraction()
    {
        ResolveGameOver();

        if (!HasNextInteraction())
        {
            PreviousDialogueResponse = "";
            Debug.Log("Reached end of character!");
            return;
        }

        var interaction = GetNextInteraction();
        var options = interaction.GetResponses();
        foreach (var dialogue in options)
        {
            UIController.AddDialogueToUI(dialogue);
        }

        UIController.SetPrompt(PreviousDialogueResponse + "\n" + interaction.Prompt);
    }

    public void ResolveInteraction(Dialogue dialogue)
    {
        Stress += dialogue.StressToAdd;
        if (dialogue.IsPositiveOption)
        {
            CurrentOpponentPositiveInteractions++;
        }

        PreviousDialogueResponse = dialogue.OpponentText;

        UIController.ClearDialogueButtons();

        GoToNextInteraction();
    }

    private void ResolveGameOver()
    {
        if (Stress >= 100)
        {
            // todo game over
        }
    }

    private void InitCharacter()
    {
        CurrentCharacter = GameObject.Find(CharacterOrder[CurrentOpponent]).GetComponent<Character>();
        GoToNextInteraction();
    }

    private bool HasNextInteraction() => CurrentOpponentInteraction < CurrentCharacter.GetInteractions().Count;

    private Interaction GetNextInteraction()
    {
        var currentInteraction = CurrentCharacter.GetInteractions()[CurrentOpponentInteraction];
        CurrentOpponentInteraction++;
        return currentInteraction;
    }
}
