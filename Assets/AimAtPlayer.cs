using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtPlayer : MonoBehaviour
{
    [SerializeField] private LevelInfoSO levelInfoSO;

    private void Update()
    {
        Vector2 playerPosition = new Vector2();
        float closest = 0;
        for (int i = 0; i < levelInfoSO.players.Count; i++)
        {
            PlayerController player = levelInfoSO.players[i];
            var distance = Vector2.Distance(player.transform.position, transform.position);
            if (i == 0 || distance < closest)
            {
                playerPosition = player.transform.position;
                closest = distance;
            }
        }
            
        Vector2 direction = playerPosition - (Vector2)transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
