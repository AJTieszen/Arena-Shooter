using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFML.Window;
using System;

public class GameInput
{
    private uint player = 0;

    // keybinds
    private uint jumpBtn = 0;
    private KeyCode jumpKey = KeyCode.Space;
    private uint menuBtn = 9;
    private KeyCode menuKey = KeyCode.Escape;

    private KeyCode forwardKey = KeyCode.W;
    private KeyCode leftKey = KeyCode.A;
    private KeyCode backKey = KeyCode.S;
    private KeyCode rightKey = KeyCode.D;

    public GameInput(uint player)
    {
        this.player = player;
    }

    public static void update ()
    {
        SFML.Window.Joystick.Update();
    }
    public Boolean isJumpPressed()
    {
        Boolean pressed = false;
        if (SFML.Window.Joystick.IsButtonPressed(player, jumpBtn))
            return true;
        if (Input.GetKeyDown(jumpKey))
            return true;
        return pressed;
    }
    public Boolean isMenuPressed()
    {
        Boolean pressed = false;
        if (SFML.Window.Joystick.IsButtonPressed(player, menuBtn))
            return true;
        if (Input.GetKeyDown(menuKey))
            return true;
        return pressed;
    }


    public Vector2 getMovement()
    {
        Vector2 move = Vector2.zero;

        move.x = (float)SFML.Window.Joystick.GetAxisPosition(player, Joystick.Axis.X) / 100f;
        move.y = (float)SFML.Window.Joystick.GetAxisPosition(player, Joystick.Axis.Y) / -100f;

        if (Input.GetKey(forwardKey))
            move.y = 1f;
        if (Input.GetKey(leftKey))
            move.x = -1f;
        if (Input.GetKey(backKey))
            move.y = -1f;
        if (Input.GetKey(rightKey))
            move.x = 1f;

        if (move.magnitude > 1)
            move.Normalize();

        return move;
    }
    public Vector2 getCameraMovement()
    {
        Vector2 move = Vector2.zero;

        move.x = (float)SFML.Window.Joystick.GetAxisPosition(player, Joystick.Axis.U) / 100f;
        move.y = (float)SFML.Window.Joystick.GetAxisPosition(player, Joystick.Axis.V) / -100f;

        return move;
    }
}
