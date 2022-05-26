using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collided");

        if (collider.gameObject.tag.Equals("Treat"))
        {
            Debug.Log("Collided with Treat");

            Player player = GetComponentInParent<Player>();
            player.EatSomething();
            
            Destroy(collider.gameObject);
        }

    }
}
