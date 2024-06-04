using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{

    public GameBehaviour gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game_Manager").GetComponent<GameBehaviour>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // On item pick up
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.transform.gameObject);
            Debug.Log("Item collected...");

            // Increment
            gameManager.Items++;
        }

        
    }
}
