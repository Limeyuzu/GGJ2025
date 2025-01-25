using Assets.GenericTools.Event;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var images = GetComponentsInChildren<Image>();
        var gameOverText = GetComponentInChildren<TextMeshProUGUI>();
        var resetButton = GetComponentInChildren<Button>();
        foreach (var image in images)
        {
            image.canvasRenderer.SetAlpha(0);
            image.color = new Color(image.color.r, image.color.b, image.color.g, 0);
        }
        gameOverText.canvasRenderer.SetAlpha(0);
        resetButton.enabled = false;

        EventManager.Subscribe(GameEvent.GameOver, _ =>
        {
            foreach (var image in images)
            {
                image.CrossFadeAlpha(1, 3, false);
                image.color = new Color(image.color.r, image.color.b, image.color.g, 1);
            }
            gameOverText.CrossFadeAlpha(1, 8, false);
            resetButton.enabled = true;
        });
    }

    public void OnButtonPress()
    {
        // reset scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
