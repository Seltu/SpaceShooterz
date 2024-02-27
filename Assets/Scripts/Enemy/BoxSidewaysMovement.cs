using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSidewaysMovement : MonoBehaviour
{
    private int direction = 2;
    private float _timer = 2.5f;
    private void Update()
    {
        transform.position += direction * Time.deltaTime * Vector3.right;
        if (_timer >= 5)
        {
            _timer = 0f;
            direction *= -1;
        }
        else
            _timer += Time.deltaTime;
    }
}
