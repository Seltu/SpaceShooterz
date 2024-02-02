using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    private PlayerInputActions _inputActions;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Enable();
    }

    private void Update()
    {
        controller.OnMove.Invoke(_inputActions.Player.Move.ReadValue<Vector2>());
        controller.OnPoint.Invoke(_inputActions.Player.Point.ReadValue<Vector2>());
        if (_inputActions.Player.Shoot.inProgress)
            controller.OnShoot.Invoke();
    }
}
