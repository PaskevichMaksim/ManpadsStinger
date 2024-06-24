using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHand : MonoBehaviour
{
    [SerializeField] private InputActionProperty pinchAction;
    [SerializeField] private InputActionProperty gripAction;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float triggerValue = pinchAction.action.ReadValue<float>();
        animator.SetFloat("Trigger", triggerValue);

        float gripValue = gripAction.action.ReadValue<float>();
        animator.SetFloat("Grip", gripValue);
    }
}
