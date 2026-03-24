using UnityEngine;

public class PlayerMovementMono : MonoBehaviour
{
    [SerializeField] private PlayerInputReader inputReader;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 12f;

    private void Update()
    {
        if (inputReader == null)
            return;
        Vector2 input = inputReader.MoveInput;
        Vector3 move = new Vector3(input.x, 0f, input.y);
        if (move.sqrMagnitude > 0.0001f)
        {
            transform.position += move.normalized * (moveSpeed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime);
        }
    }
}