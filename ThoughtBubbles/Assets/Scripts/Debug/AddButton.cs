using System.Linq;
using UnityEngine;

public class DebugButton : MonoBehaviour
{
    [SerializeField] Transform LeftButtonPanel;
    [SerializeField] Transform RightButtonPanel;
    [SerializeField] GameObject ButtonPrefab;
    [SerializeField] int AmountOfStressToAdd = 5;

    private GameState _gameState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameState = FindObjectsByType<GameState>(FindObjectsSortMode.None).First();
    }

    public void AddButtonToPanel()
    {
        var panelToUse = LeftButtonPanel.childCount <= RightButtonPanel.childCount ? LeftButtonPanel : RightButtonPanel;

        var newButton = Instantiate(ButtonPrefab, panelToUse);
        newButton.transform.SetParent(panelToUse);
        newButton.transform.SetAsLastSibling();
    }

    public void AddStress()
    {
        _gameState.Stress += AmountOfStressToAdd;
    }
}
