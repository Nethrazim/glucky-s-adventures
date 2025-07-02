using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject enemy;
    public HashSet<GameObject> enemies = new HashSet<GameObject>();
    private List<Vector3> enemyPositions = new List<Vector3>();
    private int numberOfEnemies = 0;



// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var enemy in GameObject.FindGameObjectsWithTag("enemies"))
        {
            enemies.Add(enemy);
        };

        numberOfEnemies = enemies.Count();

        foreach (GameObject enemy in enemies)
        {
            enemyPositions.Add(enemy.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemies.Count() <= 0)
        {
            foreach (Vector3 pos in enemyPositions)
            {
                enemies.Add(Instantiate(enemy, pos, Quaternion.identity));
            }
        }
    }
}
