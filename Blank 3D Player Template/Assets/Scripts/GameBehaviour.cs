using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

using UnityEngine.SceneManagement;
using UnityEditor;

public class GameBehaviour : MonoBehaviour
{

    // UI
    public TMP_Text healthText;
    public TMP_Text itemText;

    private int _playerHP = 10;
    private int _itemsCollected = 0;
    public int _maxItems = 3;

    // Trigger state machine
    public Button winButton;

    public int HP
    {
        get { return _playerHP; }
        set { 

            _playerHP = value; 
            Debug.LogFormat($"Lives: {_playerHP}");

            healthText.text = $"Health: {HP}";
        
        }
    }

    public int Items
    {
        get { return _itemsCollected; }
        set { 

            _itemsCollected = value; 
            Debug.LogFormat("Item picked up!");

            itemText.text = $"Items: {Items}";

            if ( _itemsCollected >= _maxItems)
            {
                winButton.gameObject.SetActive(true);
                Time.timeScale = 0f;
            };
        }

    }

    public void RestartScene()
    {
        Debug.Log("Restart trigerred...");

        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        itemText.text += _itemsCollected;
        healthText.text += _playerHP;
    }

    // Update is called once per frame
    void Update(){}
}
