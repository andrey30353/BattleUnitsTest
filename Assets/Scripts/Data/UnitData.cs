using UnityEngine;

[CreateAssetMenu(fileName ="Unit", menuName ="Unit")]
public class UnitData : ScriptableObject
{
    public int Hp;
    public float Speed;
    [Space]
    public float Damage;
    public float AttackDelay;
    [Min(1f)]
    public float AttackRange = 1f;
}
