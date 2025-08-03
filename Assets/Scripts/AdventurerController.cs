using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AdventurerController : MonoBehaviour
{
    public Turtle currentTurtle;
    public List<Turtle> turtlesCollected = new List<Turtle>();
    public MotherTurtle isOnMotherTurtle;

    public InputActionReference interact;

    void OnEnable()
    {
        interact.action.started += OnInteract;
    }


    private void OnInteract(InputAction.CallbackContext obj)
    {
        if (currentTurtle != null)
        {
            currentTurtle.uIAnimations.HideUI();
            currentTurtle.isFollowing = true;
            turtlesCollected.Add(currentTurtle);
            currentTurtle.PlayFollowSFX();
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

}
