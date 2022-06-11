using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public static GameBoard Instance { get; private set; }

    [SerializeField] private GameObject treatPrefab;
    public GameObject boundaries;



    private void Start()
    {
        GameObject treat = Instantiate(treatPrefab, new Vector3(1000, 1000, 0), Quaternion.identity);
    }


    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
