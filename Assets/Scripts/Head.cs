using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collided");

        // When player collides with a treat
        if (collider.gameObject.tag.Equals("Treat"))
        {
            Debug.Log("Collided with Treat");

            //Consume the treat and make the snake longer
            Player player = GetComponentInParent<Player>();
            player.EatSomething();
            
            //Place treat on another position (Not destroyed, but reused)
            collider.gameObject.GetComponent<Treat>().MovePosition();
        }

    }
}
