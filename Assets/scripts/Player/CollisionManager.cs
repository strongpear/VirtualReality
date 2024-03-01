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
    ItGameManager itGameManager;
    public float timer = 0f;
    private float lifeTimer = 120f; // How long before losing a life
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
        itGameManager = GameObject.Find("ItGameManager").GetComponent<ItGameManager>();
        

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
                //itGameManager.it_Dict.Add(playerNumber, true);
            }
            else
            {
                lifeCounter.UpdatePlayerItText(playerNumber.ToString());
                //itGameManager.it_Dict.Add(playerNumber, false);
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
                currentLives = 0;
                timer = 0;
                HandleEndGame();
            }
            lifeCounter.UpdateTimer((int)lifeTimer - (int)timer);
        }        
    }

    public void HandleEndGame()
    {
        lifeCounter.UpdateEndGame();
    }
    public int GetPlayerNumber()
    {
        if (myView.IsMine)
        {
            return playerNumber;
        }
        else
        {
            return -1;
        }
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
                    
                    // isIt = false;
                    StartCoroutine(NotIt());
                    timer = 0f;     
                }   
                else
                {
                    currentLives--;
                    lifeCounter.UpdateLivesText(currentLives);
                    TeleportToRandomLocation();
                    StartCoroutine(WaitOneSecondCoroutine()); 
                    
                }  
            }

        }
    }
    // Function to return if a player is it
    public int IsIt()
    {
        if (myView.IsMine)
        {
            if (isIt)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        
        else
        {
            return -1;
        }
    }


    // private void OnCollisionEnter(Collision collision)
    // {
    //     gameManager.HandleCollision(GetComponentInChildren<Collider>(), collision);
    // }
    IEnumerator NotIt()
    {
        yield return new WaitForSeconds(0.1f);
        if(myView.IsMine)
        {
            //itGameManager.it_Dict[playerNumber] = false;
            isIt = false;
        }
    }
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
            //itGameManager.it_Dict[playerNumber] = true;

            isIt = true;
        }

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
