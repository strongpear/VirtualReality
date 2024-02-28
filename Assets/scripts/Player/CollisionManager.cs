using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class CollisionManager : MonoBehaviour
{
    public bool isIt;

    public int maxLives = 3;
    public int currentLives;
    public int playerNumber;

    private GameObject lifeManager;
    LifeCounter lifeCounter;
    NetworkManager networkManager;
    RandomPointManager randomPointManager;

    private float timer = 0f;
    private float lifeTimer = 10f;  // Time until a life is taken away
    // Start is called before the first frame update
    void Start()
    {
        // Access LifeManager
        currentLives = maxLives;
        lifeManager = GameObject.Find("LifeManager");
        lifeCounter = lifeManager.GetComponent<LifeCounter>();

        randomPointManager = GameObject.Find("RandomPointManager").GetComponent<RandomPointManager>();

        // Assigning the first person in the lobby to be "it"
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        playerNumber = networkManager.GetPlayerCount();
        if (playerNumber <= 1)
        {
            Debug.Log("Player " + playerNumber + "is it!");
            isIt = false;
            lifeCounter.UpdatePlayerItText(playerNumber.ToString());
        }
        else
        {
            isIt = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isIt)
        {
            timer += Time.deltaTime;
            if (timer >= lifeTimer)
            {
                currentLives--;
                lifeCounter.UpdateLivesText(currentLives);
                timer = 0f;
            }       
            //Debug.Log("Timer: " + timer.ToString());
        }
        
        if (currentLives <= 0)
        {
            //HandleEndGame();
        }
    }
    public void TeleportToRandomLocation()
    {
        transform.position = randomPointManager.RandomPointOnSphere();
        Debug.Log("Teleported player to random point");
    }

    // Transferring it-ness
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedPlayer = collision.gameObject;
        if (collidedPlayer.CompareTag("Player"))
        {
            if (isIt) // If I am it, pass it to collided player
            {
                CollisionManager otherPlayerCollisionManager = collidedPlayer.GetComponent<CollisionManager>();
                Debug.Log("Transferring the It to player" + otherPlayerCollisionManager.playerNumber);
                isIt = false;
                
                // Teleport player before making them it
                otherPlayerCollisionManager.TeleportToRandomLocation();
                timer = 0f;

                StartCoroutine(WaitOneSecondCoroutine(otherPlayerCollisionManager));

                
            }     
        }
    }

    IEnumerator WaitOneSecondCoroutine(CollisionManager otherPlayerCollisionManager)
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);
        
        otherPlayerCollisionManager.isIt = true;
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
