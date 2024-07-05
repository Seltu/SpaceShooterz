using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfoSO", menuName = "ScriptableObjects/LevelInfoSO", order = 0)]
public class LevelInfoSO : ScriptableObject
{
    [SerializeField] public List<PlayerController> players;

    public void Awake()
    {
        
    }

}
