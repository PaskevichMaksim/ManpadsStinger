using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportActivator : MonoBehaviour
{
  private const float DEVIATION_VALUE = .5f;
  
   [SerializeField]
   private XRRayInteractor _rightRay;
   [SerializeField]
   private XRRayInteractor _leftRay;
   [SerializeField]
   private InputActionProperty _rightAction;
   [SerializeField]
   private InputActionProperty _leftAction;

   private void Update()
   {
      _rightRay.gameObject.SetActive(ToggleRay(_rightAction));
      _leftRay.gameObject.SetActive(ToggleRay(_leftAction));
   }

   private bool ToggleRay (InputActionProperty inputAction)
   {
     return inputAction.action.ReadValue<Vector2>().y > DEVIATION_VALUE;
   }
   
}
