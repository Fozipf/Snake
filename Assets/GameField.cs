using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{

    [SerializeField] private GameObject treatPrefab;
    [SerializeField] float interval = 2; // Create a treat every x seconds
    private bool gameOver = false;

    void Start()
    {
        if(treatPrefab == null)
        {
            Debug.LogError("Treat Prefab not set properly");
        }
        else
        {
            StartCoroutine(treatCreation(interval));
        }

    }

    void Update()
    {
        
    }

    IEnumerator treatCreation(float interval)
    {
        

        while (!gameOver)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-4.5f, 4.5f), 0);

            Instantiate(treatPrefab, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(interval);

        }
    }
}
