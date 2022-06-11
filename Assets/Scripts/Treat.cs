using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treat : MonoBehaviour
{
    float minX = float.MaxValue;
    float maxX = float.MinValue;
    float minY = float.MaxValue;
    float maxY = float.MinValue;

    private void Start()
    {
       
        //Get colliders from the gameboard walls and get their min and max positions
        var colliders = GameBoard.Instance.boundaries.GetComponentsInChildren<BoxCollider2D>();
        foreach(var collider in colliders)
        {
            if(collider.bounds.center.x < minX)
            {
                minX = collider.bounds.center.x;
            }
            if (collider.bounds.center.x > maxX)
            {
                maxX = collider.bounds.center.x;
            }
            if (collider.bounds.center.y < minY)
            {
                minY = collider.bounds.center.y;
            }
            if (collider.bounds.center.y > maxY)
            {
                maxY = collider.bounds.center.y;
            }
        }

        //Adjust min/max values so that the treat has a space to the wall
        minX += GameManager.Instance.gridSize;
        maxX -= GameManager.Instance.gridSize;
        minY += GameManager.Instance.gridSize;
        maxY -= GameManager.Instance.gridSize;

        Debug.Log("minX: " + minX);
        Debug.Log("maxX: " + maxX);
        Debug.Log("minY: " + minY);
        Debug.Log("maxY: " + maxY);

        MovePosition();
    }

    /**
     * Move to a random position within the game boundaries
    */
    public void MovePosition()
    {
        float randomX = TrimToGridSize(Random.Range(minX, maxX));
        float randomY = TrimToGridSize(Random.Range(minY, maxY));

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
