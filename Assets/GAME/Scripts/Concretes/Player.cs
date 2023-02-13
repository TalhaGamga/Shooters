using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : CharacterBase, IDirectable
{
    private delegate void MovementDel();

    MovementDel movement;

    [SerializeField] private MovementState movementState = MovementState.automatic;

    private enum MovementState
    {
        states,
        automatic,
        directed
    }

    public void GoDirectedPlace()
    {
        throw new System.NotImplementedException();
    }

    private void SwitchMovementState()
    {
        switch (movementState)
        {
            case MovementState.automatic:
                movement = Move;
                break;

            case MovementState.directed:
                movement = GoDirectedPlace;
                break;
        }
    }
}
