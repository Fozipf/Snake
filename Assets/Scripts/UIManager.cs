using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private Text gameOverText;
    [SerializeField] private Text scoreText;

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

    public void OnGameOver()
    {
        gameOverText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        scoreText.text = "Score: " + GameManager.Instance.score;
    }
}
