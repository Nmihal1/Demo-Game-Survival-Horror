using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
 
    [HideInInspector]public List<GameObject> Enemy = new List<GameObject>();
    public static GameManager Instance;


    
    void Awake() {
        Instance = this;

        foreach (var item in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Enemy.Add(item);
        }
    }

    void Register(List<GameObject> database, GameObject Item) {
        database.Add(Item);
    }

    private void Update()
    {
        if (Enemy.Count > 0)
        {
            return;
        }

        Debug.Log("Won");

    }

}
