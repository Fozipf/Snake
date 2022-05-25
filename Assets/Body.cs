using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{

    [SerializeField] private GameObject trailingBody;


    //Move myself and trigger my trailing body to move with me
    public void Move(Vector3 newPosition)
    {
        if(trailingBody != null)
        {
            trailingBody.GetComponent<Body>().Move(transform.position);    
        }
        transform.position = newPosition;
    }
}
