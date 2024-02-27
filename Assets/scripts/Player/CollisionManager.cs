using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;
    private GameObject lifeManager;
    LifeCounter lifeCounter;
    // Start is called before the first frame update
    void Start()
    {
        currentLives = maxLives;
        lifeManager = GameObject.Find("LifeManager");
        lifeCounter = lifeManager.GetComponent<LifeCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentLives--;
            lifeCounter.UpdateLivesText(currentLives);


            if (currentLives <= 0)
            {
                //HandleEndGame();
            }
        }


    }
}
