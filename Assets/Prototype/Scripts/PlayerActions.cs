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

        actions.Left.AddDefaultBinding(bindings.moveLeft);
        actions.Right.AddDefaultBinding(bindings.moveRight);
        actions.Down.AddDefaultBinding(bindings.moveDown);
        actions.Up.AddDefaultBinding(bindings.moveUp);

        actions.Jump.AddDefaultBinding(bindings.jump);

        actions.AimLeft.AddDefaultBinding(bindings.aimLeft);
        actions.AimRight.AddDefaultBinding(bindings.aimRight);
        actions.AimDown.AddDefaultBinding(bindings.aimDown);
        actions.AimUp.AddDefaultBinding(bindings.aimUp);

        actions.Shoot.AddDefaultBinding(bindings.shoot);

        actions.Submit.AddDefaultBinding(bindings.submit);
        actions.Command.AddDefaultBinding(bindings.command);
        Debug.Log("Created with keyboard bindings.");
        return actions;
    }

    public static PlayerActions CreateWithKeyboardBindings(PlayerBindings bindings)
    {
        var actions = new PlayerActions();

        actions.Left.AddDefaultBinding(Key.A);
        actions.Right.AddDefaultBinding(Key.D);
        actions.Down.AddDefaultBinding(Key.S);
        actions.Up.AddDefaultBinding(Key.W);

        actions.Jump.AddDefaultBinding(Key.Space);

        actions.AimLeft.AddDefaultBinding(Mouse.NegativeX);
        actions.AimRight.AddDefaultBinding(Mouse.PositiveX);
        actions.AimDown.AddDefaultBinding(Mouse.NegativeY);
        actions.AimUp.AddDefaultBinding(Mouse.PositiveY);

        actions.Shoot.AddDefaultBinding(Mouse.LeftButton);

        actions.Submit.AddDefaultBinding(Key.E);
        actions.Command.AddDefaultBinding(Key.Return);
        Debug.Log("Created with joystick bindings.");

        return actions;
    }
}
