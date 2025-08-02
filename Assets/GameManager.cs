using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public RectTransform healthBar;
    public float HP = 100;

    private float _timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        healthBar.sizeDelta = new Vector2(HP, 100);
    }

    public void ChangeHP(int i)
    {
        HP += i;
    }


}
