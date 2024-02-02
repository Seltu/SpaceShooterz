using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertTiltOnRight : MonoBehaviour
{
    [SerializeField] private ShipStats ShipStats;

    private void Start()
    {
        if (transform.position.x > 0)
            ShipStats.GetWeaponStats().bulletTilt = -ShipStats.GetWeaponStats().bulletTilt;
    }
}
