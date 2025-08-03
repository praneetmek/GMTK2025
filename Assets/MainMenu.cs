using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0f;    
    }

    public void StartGame()
    {
        Time.timeScale = 1f; // Resume time when starting the game
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
