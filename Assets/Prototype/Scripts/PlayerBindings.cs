using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerBindings : MonoBehaviour
{
    [System.Serializable]
    public class JoystickBindings
    {
        public InputControlType moveLeft;
        public InputControlType moveRight;
        public InputControlType moveDown;
        public InputControlType moveUp;

        public InputControlType dash;
        public InputControlType jump;

        public InputControlType aimLeft;
        public InputControlType aimRight;
        public InputControlType aimDown;
        public InputControlType aimUp;

        public InputControlType shoot;

        public InputControlType submit;

        public InputControlType command;
    }

    [System.Serializable]
    public class KeyboardBindings
    {
        public Key moveLeft;
        public Key moveRight;
        public Key moveDown;
        public Key moveUp;

        public Key dash;
        public Key jump;

        //public Key aimLeft;
        //public Key aimRight;
        //public Key aimDown;
        //public Key aimUp;

        public Mouse shoot;

        public Key submit;

        public Key command;
    }

    public JoystickBindings joystickBindings;
    public KeyboardBindings keyboardBindings;
}