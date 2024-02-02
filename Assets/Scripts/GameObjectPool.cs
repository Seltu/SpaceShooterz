using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    private List<GameObject> objectPool;

    public GameObject Create(GameObject objectPrefab, Vector2 position, Quaternion rotation)
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeSelf)
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
                return obj;
            }
        }
        var newObject = Instantiate(objectPrefab, position, rotation);
        objectPool.Add(newObject);
        return newObject;
    }
}
