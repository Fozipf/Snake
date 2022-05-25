using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject[] bodyParts;
    private GameObject head;
    private Vector3 direction = Vector3.up;

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
            
            for (int i = bodyParts.Length - 1; i >= 1; i--)
            {
                
                bodyParts[i].transform.position = bodyParts[i-1].transform.position;
                
            }

            head.transform.position = head.transform.position + direction * GameManager.Instance.gridSize;

            yield return new WaitForSeconds(1.5f);
        }
    }
}
