using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<GameObject> bodyParts;
    [SerializeField] private GameObject bodyPrefab;
    private GameObject head;
    private Vector3 direction = Vector3.up;
    private bool eat = false;

    void Start()
    {
        head = bodyParts[0];
        StartCoroutine(Move());

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector3.down;
        }
    }

    // Move one step at a time until game is over
    IEnumerator Move()
    {
        while (!GameManager.Instance.gameOver)
        {
            if (eat == true)
            {
                var tail = bodyParts[bodyParts.Count - 1];
                GameObject newTail = Instantiate(bodyPrefab, tail.transform.position, Quaternion.identity);
                newTail.name = "Body_" + (bodyParts.Count-1);
                newTail.transform.SetParent(transform);
                bodyParts.Add(newTail);
                eat = false;
            }
            
            for (int i = bodyParts.Count - 1; i >= 1; i--)
            {
                
                bodyParts[i].transform.position = bodyParts[i-1].transform.position;
                
            }

            head.transform.position = head.transform.position + direction * GameManager.Instance.gridSize;

            yield return new WaitForSeconds(0.4f);
        }
    }

    public void EatSomething()
    {
        eat = true;
    }
}
