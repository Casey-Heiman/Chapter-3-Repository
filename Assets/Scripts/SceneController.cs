using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    private GameObject enemy;

    void Update()
    {
        if (enemy == null)
        {
            enemy = Instantiate(enemyPrefab) as GameObject;
            enemy.transform.position = new Vector3(0, 1, 0);

            float angle = Random.Range(0, 360);
            enemy.transform.Rotate(0, angle, 0);

            // Set a random height for the enemy (adjust the range as needed)
            float randomHeight = Random.Range(0.5f, 2.0f);
            enemy.transform.localScale = new Vector3(1, randomHeight, 1);

            // Set a random color for the enemy
            Renderer renderer = enemy.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = new Color(Random.value, Random.value, Random.value);
            }
        }
    }
}
