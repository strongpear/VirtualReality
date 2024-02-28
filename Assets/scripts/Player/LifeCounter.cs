using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LifeCounter : MonoBehaviour
{

    public int maxLives = 3;
    public int currentLives;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI playerIt;
    // Start is called before the first frame update
    void Start()
    {
        currentLives = maxLives;
        UpdateLivesText(currentLives);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
    public void UpdateLivesText(int lives)
    {
        Debug.Log("Updating Text");
        livesText.text = "Lives: " + lives.ToString();
    }
    public void UpdatePlayerItText(string player)
    {
        Debug.Log("Updating who is It");
        playerIt.text = "It Player: " + player;
    }
}
