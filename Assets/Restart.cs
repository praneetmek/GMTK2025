using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    // Called by UI Button to reload the main game scene
    public void ReloadGame()
    {
        SceneManager.LoadScene("MainGame"); // Replace "MainGame" with your scene name if different
    }
}
