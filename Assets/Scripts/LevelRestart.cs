using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelRestart : MonoBehaviour
{
  [SerializeField]
  private InputActionProperty _rightControllerSecondaryButton;
  [SerializeField]
  private InputActionProperty _leftControllerSecondaryButton;

  private void OnEnable()
  {
    _rightControllerSecondaryButton.action.Enable();
    _leftControllerSecondaryButton.action.Enable();
  }

  private void OnDisable()
  {
    _rightControllerSecondaryButton.action.Disable();
    _leftControllerSecondaryButton.action.Disable();
  }

  private void Update()
  {
    if (_rightControllerSecondaryButton.action.triggered || _leftControllerSecondaryButton.action.triggered)
    {
      RestartLevel();
    }
  }

  private void RestartLevel()
  {
    SceneManager.LoadScene(0);
  }
}