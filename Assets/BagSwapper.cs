using UnityEngine;
using UnityEngine.UI;

public class BagSwapper : MonoBehaviour
{
    public Sprite[] bagSprites;
    public Image BagImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BagImage.sprite = bagSprites[GameManager.Instance.Turtles];   
    }
}
