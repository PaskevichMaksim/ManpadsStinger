using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _welcomeMessage;
    [SerializeField]
    private TextMeshProUGUI _movementMessage;
    [SerializeField]
    private TextMeshProUGUI _restartMessage;
    [SerializeField]
    private Button _nextButton;
    [SerializeField]
    private Button _skipButton;
    [SerializeField]
    private Button _doneButton;

    private int _currentStep;

    private void Awake()
    {
        _skipButton.onClick.AddListener(() => gameObject.SetActive(false));
        _doneButton.onClick.AddListener(() => gameObject.SetActive(false));
        _nextButton.onClick.AddListener(ChangeStep);
    }

    private void ChangeStep()
    {
        _currentStep++;

        switch (_currentStep)
        {
            case 1:
                ShowMovementMessage();
                break;
            case 2:
                ShowRestartMessage();
                break;
            default:
                gameObject.SetActive(false);
                break;
        }
    }

    private void ShowMovementMessage()
    {
        _welcomeMessage.gameObject.SetActive(false);
        _movementMessage.gameObject.SetActive(true);
    }

    private void ShowRestartMessage()
    {
        _movementMessage.gameObject.SetActive(false);
        _restartMessage.gameObject.SetActive(true);

        _doneButton.gameObject.SetActive(true);
        _nextButton.gameObject.SetActive(false);
        _skipButton.gameObject.SetActive(false);
    }
}
