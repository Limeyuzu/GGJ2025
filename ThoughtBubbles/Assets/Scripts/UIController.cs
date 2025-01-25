using Assets.GenericTools.Event;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] Transform LeftButtonPanel;
    [SerializeField] Transform RightButtonPanel;
    [SerializeField] TextMeshProUGUI PromptField;
    [SerializeField] GameObject ButtonPrefab;
    [SerializeField] GameObject GameOverOverlay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameOverOverlay.SetActive(false);
    }

    public void SetPrompt(string text)
    {
        PromptField.text = text;
    }

    public void AddDialogueToUI(Dialogue dialogue)
    {
        var panelToUse = LeftButtonPanel.childCount <= RightButtonPanel.childCount ? LeftButtonPanel : RightButtonPanel;

        var newButton = Instantiate(ButtonPrefab, panelToUse);
        newButton.transform.SetParent(panelToUse);
        newButton.transform.SetAsLastSibling();

        newButton.GetComponent<DialogueButton>().Init(dialogue);
    }

    public void ClearDialogueButtons()
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

    public void GameOverTransition()
    {
        GameOverOverlay.SetActive(true);
        EventManager.Emit(GameEvent.GameOver);
    }
}
