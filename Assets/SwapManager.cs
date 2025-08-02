using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwapManager : MonoBehaviour
{
    public static SwapManager Instance;
    public InputActionReference swap;

    public CharacterController fighterCharacter;
    public CharacterController explorerCharacter;

    public RectTransform LoopUI;
    public Camera fighterCamera;
    public Camera explorerCamera;

    public float CameraSwapTime;

    private float _timer;

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
        _timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
    }

    private void OnEnable()
    {
        swap.action.started += OnSwap;
    }

    private void OnSwap(InputAction.CallbackContext obj)
    {
        if (CurrentPlayer == PlayerType.FIGHTER)
        {
            CurrentPlayer = PlayerType.EXPLORER;
            explorerCharacter.enabled = true;
            fighterCharacter.enabled = false;
            StartCoroutine(SwapCamera(_timer, 0.25f));
        }
        else
        {
            CurrentPlayer = PlayerType.FIGHTER;
            explorerCharacter.enabled = false;
            fighterCharacter.enabled = true;
            StartCoroutine(SwapCamera(_timer, 0.75f));

        }
    }

    private IEnumerator SwapCamera(float startTime, float leftAmount)
    {
        while (Mathf.Abs(fighterCamera.rect.width - leftAmount) > 0.02f)
        {
            float newWidth = Mathf.Lerp(1 - leftAmount, leftAmount, (_timer - startTime) / CameraSwapTime);
            fighterCamera.rect = new Rect(0, 0, newWidth, 1);
            explorerCamera.rect = new Rect(newWidth, 0, 1 - newWidth, 1);
            LoopUI.anchoredPosition = new Vector2(1920 * newWidth - LoopUI.rect.width/2, LoopUI.anchoredPosition.y);
            yield return null;
        }
    }
}
