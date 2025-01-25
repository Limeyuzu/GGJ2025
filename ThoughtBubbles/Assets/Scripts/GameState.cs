using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameState : MonoBehaviour
{
    // Game setup
    [SerializeField] List<string> CharacterOrder = new() { "Biv" };
    [SerializeField] Transform LeftButtonPanel;
    [SerializeField] Transform RightButtonPanel;
    [SerializeField] GameObject ButtonPrefab;
    [SerializeField] TextMeshProUGUI PromptField;

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

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToNextInteraction()
    {
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
            AddDialogueToUI(dialogue);
        }

        PromptField.text = PreviousDialogueResponse + "\n" + interaction.Prompt;
    }

    public void ResolveInteraction(Dialogue dialogue)
    {
        Stress += dialogue.StressToAdd;
        if (dialogue.IsPositiveOption)
        {
            CurrentOpponentPositiveInteractions++;
        }

        PreviousDialogueResponse = dialogue.OpponentText;

        ClearDialogueButtons();

        GoToNextInteraction();
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

    private void AddDialogueToUI(Dialogue dialogue)
    {
        var panelToUse = LeftButtonPanel.childCount <= RightButtonPanel.childCount ? LeftButtonPanel : RightButtonPanel;

        var newButton = Instantiate(ButtonPrefab, panelToUse);
        newButton.transform.SetParent(panelToUse);
        newButton.transform.SetAsLastSibling();

        newButton.GetComponent<DialogueButton>().Init(dialogue);
    }

    private void ClearDialogueButtons()
    {
        foreach (Transform child in LeftButtonPanel.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in RightButtonPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
