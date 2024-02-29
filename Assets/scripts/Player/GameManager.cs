// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Photon.Pun;
// public class GameManager : MonoBehaviour
// {

//     private int maxLives = 3;
//     private bool isIt = false;       // not it by default at start
//     public int currentLives;
//     public int myPlayerNumber;

//     private GameObject lifeManager;
//     LifeCounter lifeCounter;
//     NetworkManager networkManager;
//     RandomPointManager randomPointManager;
//     public float timer = 0f;
//     private float lifeTimer = 10f; // How long before losing a life
//     // Start is called before the first frame update
//     void Start()
//     {
//         currentLives = maxLives;
//         lifeCounter = GameObject.Find("LifeManager").GetComponent<LifeCounter>();
//         networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
//         randomPointManager = GameObject.Find("RandomPointManager").GetComponent<RandomPointManager>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (isIt)
//         {
//             timer += Time.deltaTime;
//             if (timer >= lifeTimer)
//             {
//                 currentLives--;
//                 lifeCounter.UpdateLivesText(currentLives);
//                 timer = 0f;
//             }       
//             //Debug.Log("Timer: " + timer.ToString());
//         }

//         if (currentLives <= 0)
//         {
//             //Debug.Log("Handling End Game");
//             //HandleEndGame();
//         }

//     }

//     public void AssignPlayerNumber(int playerNumber)
//     {
//         myPlayerNumber = playerNumber;
//         // It-ness logic
//         if (playerNumber <= 1)
//         {
//             Debug.Log("Player " + playerNumber + "is it!");
//             isIt = true;
//             lifeCounter.UpdatePlayerItText(playerNumber.ToString());
//         }
//         else
//         {
//             lifeCounter.UpdatePlayerItText(playerNumber.ToString());
//         }
//     }

//     // public void TeleportToRandomLocation(GameObject collider)
//     // {
//     //     collider.transform.position = randomPointManager.RandomPointOnSphere();
//     //     Debug.Log("Teleported player to random point");
//     // }

//     public void HandleCollision(Collider collider, Collision collision)
//     {
//         GameObject collidingPlayer = collider.gameObject;
//         GameObject collidedPlayer = collision.gameObject;
//         if (collidedPlayer.CompareTag("Player"))
//         {
//             Debug.Log("Collision between Players Occuring");
//             if (isIt) // If I am it, pass it to collided player
//             {                
//                 isIt = false;
//                 timer = 0f;                 
//             }   
//             else
//             {
//                 collidingPlayer.GetComponent<CollisionManager>().TeleportToRandomLocation();   
//                 StartCoroutine(WaitOneSecondCoroutine());                 
//             }  
//         }

//     }

//     IEnumerator WaitOneSecondCoroutine()
//     {
//         // This is called when I teleport and make myself it
//         //Print the time of when the function is first called.
//         Debug.Log("Started Coroutine at timestamp : " + Time.time);

//         //yield on a new YieldInstruction that waits for 1 second.
//         yield return new WaitForSeconds(1);
//         if (myView.IsMine)
//         {
//             Debug.Log("Making someone it");
//             // Make myself it
//             isIt = true;
//         }


//         //After we have waited 5 seconds print the time again.
//         Debug.Log("Finished Coroutine at timestamp : " + Time.time);
//     }
// }
