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

    //Touch input detection start
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;
    //Touch input detection end

    void Start()
    {
        head = bodyParts[0];

        sprite = head.GetComponent<SpriteRenderer>().sprite;
    }

    void Update()
    {
        if (GameManager.Instance.gameOver)
        {
            UIManager.Instance.OnGameOver();
            return;
        }

        if (Time.time > nextActionTime)
        {
            nextActionTime += intervalMillis / 1000.0f;
            Move();
            oldDirection = direction;
        }


        //PC Input
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !direction.Equals(Vector3.left) && !direction.Equals(Vector3.right))
        {
            direction = Vector3.left;
            directionChanged = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !direction.Equals(Vector3.right) && !direction.Equals(Vector3.left))
        {
            direction = Vector3.right;
            directionChanged = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !direction.Equals(Vector3.up) && !direction.Equals(Vector3.down))
        {
            direction = Vector3.up;
            directionChanged = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !direction.Equals(Vector3.down) && !direction.Equals(Vector3.up))
        {
            direction = Vector3.down;
            directionChanged = true;
        }

        //Phone Input
        foreach(Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }

            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
            }

            


        }
    }


    void checkSwipe()
    {
        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            //don't accept input if movement direction is already vertical
            if (direction.Equals(Vector3.up) || direction.Equals(Vector3.down))
            {
                return;
            }

            //Debug.Log("Vertical");
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                direction = Vector3.up;
                directionChanged = true;
            }
            else if (fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                direction = Vector3.down;
                directionChanged = true;
            }
            fingerUp = fingerDown;
        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            //don't accept input if movement direction is already horizontal
            if (direction.Equals(Vector3.left) || direction.Equals(Vector3.right))
            {
                return;
            }

            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                direction = Vector3.right;
                directionChanged = true;
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                direction = Vector3.left;
                directionChanged = true;
            }
            fingerUp = fingerDown;
        }

        //No Movement at-all
        else
        {
            //Debug.Log("No Swipe!");
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    /**
     * Move one step at a time until game is over
     */
    void Move()
    {
        GameObject tail = bodyParts[bodyParts.Count - 1];

        if (eat == true)
        {
            
            GameObject newTail = Instantiate(bodyPrefab, tail.transform.position, tail.transform.rotation);
            newTail.name = "Body_" + (bodyParts.Count - 1);
            newTail.transform.SetParent(transform);
            bodyParts.Add(newTail);
            eat = false;
        }


        float width = sprite.rect.width / 100.0f * head.transform.localScale.x;
        float height = sprite.rect.height / 100.0f * head.transform.localScale.y;
        Vector3 bufferPos = new Vector3(head.transform.position.x, head.transform.position.y, head.transform.position.z);
        Quaternion bufferRot = Quaternion.Euler(head.transform.localRotation.eulerAngles.x, head.transform.localRotation.eulerAngles.y, head.transform.localRotation.eulerAngles.z);
        
        if (directionChanged)
        {
            if (direction.x != 0)
            {
                head.transform.position = head.transform.position + new Vector3(direction.x * (height / 2.0f + width / 2.0f), oldDirection.y * (height / 2.0f - width / 2.0f), 0);
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
            head.transform.position = head.transform.position + direction * height;
        }

        for (int i = 1; i < bodyParts.Count; i++)
        {
            Vector3 bufferPos2 = new Vector3(bodyParts[i].transform.position.x, bodyParts[i].transform.position.y, bodyParts[i].transform.position.z);
            Quaternion bufferRot2 = Quaternion.Euler(bodyParts[i].transform.localRotation.eulerAngles.x, bodyParts[i].transform.localRotation.eulerAngles.y, bodyParts[i].transform.localRotation.eulerAngles.z);

            bodyParts[i].transform.position = bufferPos;
            bodyParts[i].transform.rotation = bufferRot;

            bufferPos = bufferPos2;
            bufferRot = bufferRot2;
        }

        directionChanged = false;
    }

    /**
     * When a treat gets eaten by the snake, make it longer and increase the score
     */
    public void EatSomething()
    {
        eat = true;
        GameManager.Instance.score++;
    }
}
