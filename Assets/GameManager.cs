using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public InputActionReference swap;

    public CharacterController fighterCharacter;
    public CharacterController explorerCharacter;

    public enum PlayerType
    {
        FIGHTER,
        EXPLORER
    }

    public PlayerType CurrentPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        CurrentPlayer = PlayerType.FIGHTER;
        explorerCharacter.enabled = false;
    }

    void Start()
    {
        CurrentPlayer = PlayerType.FIGHTER;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        swap.action.started += OnSwap;
    }

    private void OnSwap(InputAction.CallbackContext obj)
    {
        if(CurrentPlayer == PlayerType.FIGHTER)
        {
            CurrentPlayer = PlayerType.EXPLORER;
            explorerCharacter.enabled = true;
            fighterCharacter.enabled = false;
        }
        else
        {
            CurrentPlayer = PlayerType.FIGHTER;
            explorerCharacter.enabled = false;
            fighterCharacter.enabled = true;
        }
    }
}
