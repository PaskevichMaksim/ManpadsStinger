using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractorSwitcher : MonoBehaviour
{
  [SerializeField]
  private XRDirectInteractor _rightDirectInteractor;
  [SerializeField]
  private XRDirectInteractor _leftDirectInteractor;
  [SerializeField]
  private XRRayInteractor _rightRayInteractor;
  [SerializeField]
  private XRRayInteractor _leftRayInteractor;
  [SerializeField]
  private InputActionProperty _rightControllerPrimaryButton;
  [SerializeField]
  private InputActionProperty _leftControllerPrimaryButton;

  private void OnEnable()
  {
    _rightControllerPrimaryButton.action.Enable();
    _leftControllerPrimaryButton.action.Enable();
  }

  private void OnDisable()
  {
    _rightControllerPrimaryButton.action.Disable();
    _leftControllerPrimaryButton.action.Disable();
  }

  private void Update()
  {
    if (_rightControllerPrimaryButton.action.triggered)
    {
      ToggleInteractor(_rightDirectInteractor, _rightRayInteractor);
    }

    if (_leftControllerPrimaryButton.action.triggered)
    {
      ToggleInteractor(_leftDirectInteractor, _leftRayInteractor);
    }
  }

  private void ToggleInteractor(XRDirectInteractor directInteractor, XRRayInteractor rayInteractor)
  {
    directInteractor.gameObject.SetActive(!directInteractor.gameObject.activeSelf);
    rayInteractor.gameObject.SetActive(!rayInteractor.gameObject.activeSelf);
  }
}