using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Joystick movementJoystick;
    
    private int lookFingerId = -1;
    [SerializeField] private Button sneakButton, runButton;

    public Vector2 joystickInput, lookInput;
    public float camSen = 3;

    public bool runToggleButton = false;
    public bool sneakToggleButton = false;
    public bool aimToggleButton = false;

    private void Start()
    {
        //sneakButton = GameObject.Find("ToggleSneakButton").GetComponent<Button>();
        //runButton = GameObject.Find("ToggleRunButton").GetComponent<Button>();
    }

    void Update()
    {
        handleJoystick();
        handleTouch();
    }

    void handleJoystick()
    {
        joystickInput.x = movementJoystick.Horizontal * 2;
        joystickInput.y = movementJoystick.Vertical * 2;
    }

    void handleTouch()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            switch (t.phase)
            {
                case TouchPhase.Began:
                    //if the touch has began and is not over an UI object
                    if (lookFingerId == -1 && !(EventSystem.current.IsPointerOverGameObject(t.fingerId)))
                    {
                        //save the FingerID
                        lookFingerId = t.fingerId;
                    }
                    break;

                case TouchPhase.Moved:
                    if (t.fingerId == lookFingerId)
                        lookInput = t.deltaPosition * camSen * Time.deltaTime;
                    break;

                case TouchPhase.Stationary:
                    if (t.fingerId == lookFingerId)
                        lookInput = Vector2.zero;
                    break;

                case TouchPhase.Canceled:
                case TouchPhase.Ended:
                    if (t.fingerId == lookFingerId)
                    {
                        lookInput = Vector2.zero;
                        lookFingerId = -1;
                    }
                    break;
            }
        }
    }

    public void setRunSneakColor()
    {
        if (sneakToggleButton)
        {
            sneakButton.image.color = new Color32(36, 96,75,255);
        }
        else
            sneakButton.image.color = new Color32(28, 28, 28, 163);

        if (runToggleButton)
        {
            runButton.image.color = new Color32(36, 96, 75, 255);
        }
        else
            runButton.image.color = new Color32(28, 28, 28, 163);
    }

    public void toggleSneakButton()
    {
        if (!sneakToggleButton)
        {
            sneakToggleButton = true;
            runToggleButton = false;
        }
        else
        {
            sneakToggleButton = false;
            runToggleButton = false;
        }
        setRunSneakColor();
    }

    public void toggleRunButton()
    {
        if (!runToggleButton)
        {
            sneakToggleButton = false;
            runToggleButton = true;
        }
        else
        {
            sneakToggleButton = false;
            runToggleButton = false;
        }
        setRunSneakColor();
    }

    public void toggleAimButton()
    {
        if (!aimToggleButton)
        {
            aimToggleButton = true;
        }
        else
        {
            aimToggleButton = false;
        }
    }

    public void resetRunAndSneak()
    {
        if (runToggleButton)
            runToggleButton = false;

        if (sneakToggleButton)
            sneakToggleButton = false;

        setRunSneakColor();
    }

    public void resetForDamage()
    {
        runToggleButton = true;
        sneakToggleButton = false;
        aimToggleButton = false;
        setRunSneakColor();
    }

}
