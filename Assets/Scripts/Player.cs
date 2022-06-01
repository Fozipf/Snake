using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<GameObject> bodyParts;
    [SerializeField] private GameObject bodyPrefab;
    private GameObject head;
    private Vector3 oldDirection = Vector3.up; 
    private Vector3 direction = Vector3.up;
    private bool eat = false;
    private bool directionChanged = false;

    private int intervalMillis = 500;
    private float nextActionTime = 0.0f;
    private Sprite sprite;
    public int score;


    void Start()
    {
        head = bodyParts[0];

        sprite = head.GetComponent<SpriteRenderer>().sprite;
    }

    void Update()
    {
        
        if (Time.time > nextActionTime)
        {
            nextActionTime += intervalMillis/1000.0f;
            Move();
            oldDirection = direction;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector3.left;
            directionChanged = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector3.right;
            directionChanged = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector3.up;
            directionChanged = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector3.down;
            directionChanged = true;
        }
    }

    
    /**
     * Move one step at a time until game is over
     */
    void Move()
    {       

        if (eat == true)
            {
                var tail = bodyParts[bodyParts.Count - 1];
                GameObject newTail = Instantiate(bodyPrefab, tail.transform.position, tail.transform.rotation);
                newTail.name = "Body_" + (bodyParts.Count-1);
                newTail.transform.SetParent(transform);
                bodyParts.Add(newTail);
                eat = false;
            }
            
            for (int i = bodyParts.Count - 1; i >= 1; i--)
            {
                bodyParts[i].transform.position = bodyParts[i-1].transform.position;
                bodyParts[i].transform.rotation = bodyParts[i - 1].transform.rotation;
            }

            float width = sprite.rect.width/100.0f * head.transform.localScale.x;
            float height = sprite.rect.height/100.0f * head.transform.localScale.y;

            if (directionChanged)
            {
                if(direction.x != 0)
                {
                    head.transform.position = head.transform.position + new Vector3(direction.x * (height / 2.0f + width / 2.0f), oldDirection.y*(height / 2.0f - width / 2.0f), 0);
                    head.transform.rotation = Quaternion.Euler(0, 0, 90);
                }
                else if (direction.y != 0)
                {
                    head.transform.position = head.transform.position + new Vector3(oldDirection.x * (height / 2.0f - width / 2.0f), direction.y * (height / 2.0f + width / 2.0f), 0);
                    head.transform.rotation = Quaternion.identity;
                }
            }
            else
            {
                 head.transform.position = head.transform.position + direction * 0.34f;
            }

            directionChanged = false;
    }

    /**
     * When a treat gets eaten by the snake, make it longer and increase the score
     */
    public void EatSomething()
    {
        eat = true;
        score++;
    }
}
