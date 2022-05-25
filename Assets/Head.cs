using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] private GameObject trailingBody;
    private Vector3 direction = Vector3.up;

    void Start()
    {
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

    // Move one step at a time until game is over and trigger movement of the trailing body
    IEnumerator Move()
    {
        Vector3 oldPosition; 

        while (!GameManager.Instance.gameOver)
        {
            oldPosition = transform.position;
            transform.position = transform.position+direction*GameManager.Instance.gridSize;
            trailingBody.GetComponent<Body>().Move(oldPosition);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
