using Assets.GenericTools.Event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Selection Screen
    [SerializeField] GameObject SelectionScreen;
    [SerializeField] Transform LeftButtonPanel;
    [SerializeField] Transform RightButtonPanel;
    [SerializeField] TextMeshProUGUI PromptField;
    [SerializeField] GameObject ButtonPrefab;

    // Side by side screen
    [SerializeField] GameObject SideBySideScreen;
    [SerializeField] Image PlayerNormalSprite;
    [SerializeField] Image PlayerAltSprite;
    [SerializeField] TextMeshProUGUI PlayerText;
    [SerializeField] Image PlayerTextPlayerSprite;
    [SerializeField] Image PlayerTextOpponentSprite;
    [SerializeField] TextMeshProUGUI OpponentText;
    [SerializeField] Image OpponentTextPlayerSprite;
    [SerializeField] Image OpponentTextOpponentSprite;

    [SerializeField] GameObject GameOverOverlay;
    public float ShakeStrength = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameOverOverlay.SetActive(false);
    }

    public void SetPrompt(string text)
    {
        PromptField.text = text;
    }

    public void GoToSideBySideView(Dialogue dialogue, Character currentOpponent)
    {
        PlayerText.text = dialogue.PlayerText;
        PlayerTextPlayerSprite.sprite = PlayerNormalSprite.sprite;
        PlayerTextOpponentSprite.sprite = currentOpponent.GetMainSprite();
        OpponentText.text = dialogue.OpponentText;
        OpponentTextPlayerSprite.sprite = dialogue.IsPositiveOption ? PlayerNormalSprite.sprite : PlayerAltSprite.sprite;
        OpponentTextOpponentSprite.sprite = dialogue.IsPositiveOption ? currentOpponent.GetMainSprite() : currentOpponent.GetAltSprite();

        SelectionScreen.SetActive(false);
        SideBySideScreen.SetActive(true);
        PlayerText.gameObject.SetActive(true);
        OpponentText.gameObject.SetActive(false);
    }

    public void GoToOpponentSpeech()
    {
        PlayerText.gameObject.SetActive(false);
        OpponentText.gameObject.SetActive(true);
    }

    public void GoToDialogueView()
    {
        SelectionScreen.SetActive(true);
        SideBySideScreen.SetActive(false);
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

    public void ScreenShakeSmall()
    {
        ShakeStrength = 5;
    }

    public void ScreenShakeBig()
    {
        ShakeStrength = 10;
    }

    public void GameOverTransition()
    {
        GameOverOverlay.SetActive(true);
        EventManager.Emit(GameEvent.GameOver);
    }
}
