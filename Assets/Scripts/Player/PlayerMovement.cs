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
        controller.OnPoint.AddListener(Point);
        _playerStats = controller.GetStats();
    }

    private void Move(Vector2 direction)
    {
        body.velocity = direction * _playerStats.GetMovementSpeed();
    }

    private void Point(Vector2 position)
    {
        // Convert mouse position to world space
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(position);

        // Calculate the direction from the current position to the mouse position
        Vector2 direction = worldMousePosition - (Vector2)transform.position;

        // Calculate the angle in radians
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        // Rotate the GameObject towards the mouse position
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }
}