using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private Rigidbody2D body;
    private PlayerStats _playerStats;
    private float _rotateSpeed;
    private void Awake()
    {
        controller.OnMove.AddListener(Move);
        _playerStats = controller.GetStats();
    }

    private void Move(Vector2 direction)
    {
        body.velocity = direction * _playerStats.GetMovementSpeed();
    }
}