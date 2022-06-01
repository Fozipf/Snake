using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treat : MonoBehaviour
{
    /**
     * Move to a random position within the game boundaries
    */
    public void MovePosition()
    {
        float randomX = TrimToGridSize(Random.Range(-2.1f, 2.1f));
        float randomY = TrimToGridSize(Random.Range(-4.5f, 4.5f));

        Vector3 randomPosition = new Vector3(randomX, randomY, 0);

        transform.position = randomPosition;
    }
    

    /**
     * Take a float value and trim it to make it dividable trough the grid size
     * Example: Random value is 3.141572 => Result for gridSize = 0.2f : 3.2f
     *         Random value is 3.041572 => Result for gridSize = 0.2f : 3.0f
     *         Values between 3.0f and 3.2f are eliminated with this method
    */
    private float TrimToGridSize(float f)
    {
        f /= GameManager.Instance.gridSize;
        f = Mathf.RoundToInt(f);
        f *= GameManager.Instance.gridSize;

        return f;
    }
}
