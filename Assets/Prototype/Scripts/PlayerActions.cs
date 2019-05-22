using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerActions : PlayerActionSet
{
    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerAction Down;
    public PlayerAction Up;
    public PlayerTwoAxisAction Move;
    public PlayerAction Jump;
    public PlayerAction Dash;

    public PlayerAction AimLeft;
    public PlayerAction AimRight;
    public PlayerAction AimDown;
    public PlayerAction AimUp;
    public PlayerTwoAxisAction Aim;

    public PlayerAction Shoot;

    public PlayerAction Submit;
    public PlayerAction Command;

    public PlayerActions()
    {
        Left = CreatePlayerAction("Move Left");
        Right = CreatePlayerAction("Move Right");
        Down = CreatePlayerAction("Move Down");
        Up = CreatePlayerAction("Move Up");
        Move = CreateTwoAxisPlayerAction(Left, Right, Down, Up);
        Jump = CreatePlayerAction("Jump");
        Dash = CreatePlayerAction("Dash");

        AimLeft = CreatePlayerAction("Aim Left");
        AimRight = CreatePlayerAction("Aim Right");
        AimDown = CreatePlayerAction("Aim Down");
        AimUp = CreatePlayerAction("Aim Up");
        Aim = CreateTwoAxisPlayerAction(AimLeft, AimRight, AimDown, AimUp);

        Shoot = CreatePlayerAction("Shoot");

        Submit = CreatePlayerAction("Submit");
        Command = CreatePlayerAction("Command");
    }

    public static PlayerActions CreateWithJoystickBindings(PlayerBindings bindings)
    {
        var actions = new PlayerActions();

        actions.Left.AddDefaultBinding(bindings.joystickBindings.moveLeft);
        actions.Right.AddDefaultBinding(bindings.joystickBindings.moveRight);
        actions.Down.AddDefaultBinding(bindings.joystickBindings.moveDown);
        actions.Up.AddDefaultBinding(bindings.joystickBindings.moveUp);

        actions.Jump.AddDefaultBinding(bindings.joystickBindings.jump);
        actions.Dash.AddDefaultBinding(bindings.joystickBindings.dash);

        actions.AimLeft.AddDefaultBinding(bindings.joystickBindings.aimLeft);
        actions.AimRight.AddDefaultBinding(bindings.joystickBindings.aimRight);
        actions.AimDown.AddDefaultBinding(bindings.joystickBindings.aimDown);
        actions.AimUp.AddDefaultBinding(bindings.joystickBindings.aimUp);

        actions.Shoot.AddDefaultBinding(bindings.joystickBindings.shoot);

        actions.Submit.AddDefaultBinding(bindings.joystickBindings.submit);
        actions.Command.AddDefaultBinding(bindings.joystickBindings.command);
        Debug.Log("Created with joystick bindings.");
        return actions;
    }

    public static PlayerActions CreateWithKeyboardBindings(PlayerBindings bindings)
    {
        var actions = new PlayerActions();

        actions.Left.AddDefaultBinding(bindings.keyboardBindings.moveLeft);
        actions.Right.AddDefaultBinding(bindings.keyboardBindings.moveRight);
        actions.Down.AddDefaultBinding(bindings.keyboardBindings.moveDown);
        actions.Up.AddDefaultBinding(bindings.keyboardBindings.moveUp);

        actions.Jump.AddDefaultBinding(bindings.keyboardBindings.jump);
        actions.Dash.AddDefaultBinding(bindings.keyboardBindings.dash);

        //actions.AimLeft.AddDefaultBinding(bindings.keyboardBindings.aimLeft);
        //actions.AimRight.AddDefaultBinding(bindings.keyboardBindings.aimRight);
        //actions.AimDown.AddDefaultBinding(bindings.keyboardBindings.aimDown);
        //actions.AimUp.AddDefaultBinding(bindings.keyboardBindings.aimUp);

        actions.Shoot.AddDefaultBinding(bindings.keyboardBindings.shoot);

        actions.Submit.AddDefaultBinding(bindings.keyboardBindings.submit);
        actions.Command.AddDefaultBinding(bindings.keyboardBindings.command);
        Debug.Log("Created with joystick bindings.");

        return actions;
    }
}
