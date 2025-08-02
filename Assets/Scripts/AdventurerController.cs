using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AdventurerController : CharacterController
{
    public Turtle currentTurtle;
    public List<Turtle> turtlesCollected = new List<Turtle>();
    public MotherTurtle isOnMotherTurtle;

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
            turtlesCollected.Add(currentTurtle);
            currentTurtle = null;
        }
        if (isOnMotherTurtle != null && turtlesCollected.Count > 0)
        {
            isOnMotherTurtle.InteractWithMotherTurtle(this);
            isOnMotherTurtle = null;
        }
    }

    public void ClearFollowingTurtles()
    {
        foreach (var turtle in turtlesCollected)
        {
            Destroy(turtle.gameObject);
        }
        turtlesCollected.Clear();
    }

    public override void Update()
    {
        base.Update();
    }
}
