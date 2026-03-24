using UnityEngine;

public class FloatingJoystickAdapter : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;

    public Vector2 Move
    {
        get
        {
            if (joystick == null)
                return Vector2.zero;
            return new Vector2(joystick.Horizontal, joystick.Vertical);
        }
    }
}