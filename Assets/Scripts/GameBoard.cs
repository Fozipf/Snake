using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    
    [SerializeField] private GameObject treatPrefab;

    private void Start()
    {
        GameObject treat = Instantiate(treatPrefab, new Vector3(1000,1000,0), Quaternion.identity);
        treat.GetComponent<Treat>().MovePosition();
    }
}
