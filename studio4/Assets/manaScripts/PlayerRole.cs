using UnityEngine;

public class PlayerRole : MonoBehaviour
{
    private enum Role
    {
        Player,
        Opponent
    }

    [SerializeField] private Role role;

    public bool IsOpponent => role == Role.Opponent;
}
