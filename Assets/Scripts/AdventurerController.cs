using UnityEngine;
using UnityEngine.InputSystem;

public class AdventurerController : CharacterController
{
    public Turtle currentTurtle;


    void OnEnable()
    {
        interact.action.started += OnInteract;
    }

    void Start()
    {
        
    }

    private void OnInteract(InputAction.CallbackContext obj)
    {
        if (currentTurtle != null)
        {
            currentTurtle.uIAnimations.HideUI();
            currentTurtle.isFollowing = true;
            currentTurtle = null;
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
