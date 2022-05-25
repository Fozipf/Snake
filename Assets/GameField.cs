using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{

    [SerializeField] private GameObject treatPrefab;
    [SerializeField] float interval = 2; // Create a treat every x seconds
    
    void Start()
    {
        if(treatPrefab == null)
        {
            Debug.LogError("Treat Prefab not set properly");
        }
        else
        {
            StartCoroutine(TreatCreation(interval));
        }

    }

    IEnumerator TreatCreation(float interval)
    {
        while (!GameManager.Instance.gameOver)
        {
            float randomX = TrimToGridSize(Random.Range(-2.5f, 2.5f));
            float randomY = TrimToGridSize(Random.Range(-4.5f, 4.5f));


            Vector3 randomPosition = new Vector3(randomX, randomY, 0);

            Instantiate(treatPrefab, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(interval);

        }
    }

    //Take a float value and trim it to make it dividable trough the grid size
    //Example: Random value is 3.141572 => Result for gridSize = 0.2f : 3.2f
    //         Random value is 3.041572 => Result for gridSize = 0.2f : 3.0f
    //         Values between 3.0f and 3.2f are eliminated with this method
    private float TrimToGridSize(float f)
    {
        f /= GameManager.Instance.gridSize;
        f = Mathf.RoundToInt(f);
        f *= GameManager.Instance.gridSize;

        return f;
    }
}
