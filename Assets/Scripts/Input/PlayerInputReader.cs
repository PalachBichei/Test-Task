using UnityEngine;

public class PlayerInputReader : MonoBehaviour
{
    [SerializeField] private FloatingJoystickAdapter joystickAdapter;

    public Vector2 MoveInput { get; private set; }

    private void Update()
    {
        MoveInput = joystickAdapter != null 
            ? joystickAdapter.Move 
            : Vector2.zero;
    }
}