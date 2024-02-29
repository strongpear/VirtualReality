using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class CollisionManager : MonoBehaviour
{
    private PhotonView myView;
    public bool isIt = false;

    public int maxLives = 3;
    public int currentLives;
    public int playerNumber;

    private GameObject lifeManager;
    LifeCounter lifeCounter;
    NetworkManager networkManager;
    RandomPointManager randomPointManager;
    public float timer = 0f;
    private float lifeTimer = 10f; // How long before losing a life
    // GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        myView = GetComponent<PhotonView>();

        // Access Managers
        currentLives = maxLives;
        lifeManager = GameObject.Find("LifeManager");
        lifeCounter = lifeManager.GetComponent<LifeCounter>();
        randomPointManager = GameObject.Find("RandomPointManager").GetComponent<RandomPointManager>();

        

        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        playerNumber = networkManager.GetPlayerCount();

        // gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // gameManager.AssignPlayerNumber(playerNumber);
        if (myView.IsMine)
        {
            if (playerNumber <= 1)
            {
                Debug.Log("Player " + playerNumber + "is it!");
                isIt = true;
                lifeCounter.UpdatePlayerItText(playerNumber.ToString());
            }
            else
            {
                lifeCounter.UpdatePlayerItText(playerNumber.ToString());
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (myView.IsMine)
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
        lifeCounter.UpdateTimer((int)lifeTimer - (int)timer);
    }
    public void TeleportToRandomLocation()
    {
        transform.position = randomPointManager.RandomPointOnSphere();
        Debug.Log("Teleported player to random point");
    }

    // Transferring it-ness
    // private void OnCollisionEnter(Collision collision)
    // {
    //     GameObject collidedPlayer = collision.gameObject;
    //     if (collidedPlayer.CompareTag("Player"))
    //     {
    //         Debug.Log("Collision between Players Occuring");
    //         if (gameManager.isIt) // If I am it, pass it to collided player
    //         {
                
    //             gameManager.isIt = false;
    //             gameManager.timer = 0f;     
    //         }   
    //         else
    //         {
    //             TeleportToRandomLocation();
    //             StartCoroutine(WaitOneSecondCoroutine()); 
                
    //         }  
    //     }
    // }

    // Transferring it-ness
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedPlayer = collision.gameObject;
        if (collidedPlayer.CompareTag("Player"))
        {
            if (myView.IsMine)
            {
                Debug.Log("Collision between Players Occuring");
                if (isIt) // If I am it, pass it to collided player
                {
                    
                    isIt = false;
                    timer = 0f;     
                }   
                else
                {
                    TeleportToRandomLocation();
                    StartCoroutine(WaitOneSecondCoroutine()); 
                    
                }  
            }

        }
    }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     gameManager.HandleCollision(GetComponentInChildren<Collider>(), collision);
    // }

    IEnumerator WaitOneSecondCoroutine()
    {
        // This is called when I teleport and make myself it
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 1 second.
        yield return new WaitForSeconds(1);

        // Make myself it
        if (myView.IsMine)
        {
            isIt = true;
        }

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
