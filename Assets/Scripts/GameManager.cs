using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public LoopScript loopCanvas;
    public RectTransform healthBar;
    public float HP = 100;
    public int Essence = 0;
    public int Turtles = 0;

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
        healthBar.localScale = new Vector3(HP / 100.0f, 1, 1);
/*        healthBar.sizeDelta = new Vector2(HP, 100);
*/    }

    public void ChangeHP(int i)
    {
        if (HP + i > 100)
        {
            HP = 100;
            return;
        }
        HP += i;
        if(HP <= 0)
        {
            SceneManager.LoadScene(1); 
        }
    }


    public void AddEssence(int i)
    {
        Essence = Mathf.Min(100, Essence + 2);
    }

    public void AddTurtle()
    {
        Turtles = Mathf.Min(5, Turtles + 1);
    }


}
