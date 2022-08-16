using UnityEngine;

public class PlayerRole : MonoBehaviour
{
    private enum Role
    {
        Player1,
        Player2
    }

    [SerializeField] private Role role;
    public bool IsPlayer2 => role == Role.Player2;
    public bool IsPlayer1 => role == Role.Player1;
}
