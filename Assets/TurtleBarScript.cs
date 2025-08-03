using UnityEngine;
using UnityEngine.UI;

public class TurtleBarScript : MonoBehaviour
{
    private Image _image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        _image.fillAmount = GameManager.Instance.Essence / 100.0f;

    }
}
