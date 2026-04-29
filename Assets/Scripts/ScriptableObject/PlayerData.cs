using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Base Stats")]

    [Min(1f)]
    public float maxHP = 100f;

    [Min(0f)]
    public float moveSpeed = 5f;

    private void OnValidate()
    {
        if (maxHP < 1f) maxHP = 1f;
        if (moveSpeed < 0f) moveSpeed = 0f;
    }
}