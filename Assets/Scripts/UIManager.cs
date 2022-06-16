using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    //Panels
    [SerializeField] private GameObject gameOverPanel;

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
        gameOverPanel.SetActive(true);
        scoreText.text = "Score: " + GameManager.Instance.score;
    }
}
