using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Type
{
    Human,
    Zombie
}

public class ArmyController : MonoBehaviour
{
    [SerializeField] private Type _type;

    private List<Unit> _units;
    public List<Unit> Units => _units;

    public event Action<Unit> OnUnitDeadEvent;

    private void Awake()
    {
        _units = GetComponentsInChildren<Unit>().ToList();
    }

    private void OnEnable()
    {
        foreach (var unit in _units)
        {
            unit.OnDeadEvent += Unit_OnDeadEvent;
        }        
    }

    private void OnDisable()
    {
        foreach (var unit in _units)
        {
            unit.OnDeadEvent -= Unit_OnDeadEvent;
        }
    }

    public Unit GetNearestUnit(Vector3Int position)
    {
        if (_units.Count == 0)
            return null;

        float minDistanceSqr = float.MaxValue;
        Unit result = _units[0];
        foreach (var unit in _units)
        {  
            var distanceSqr = (unit.transform.position - position).sqrMagnitude;
            if (distanceSqr < minDistanceSqr)
            {
                minDistanceSqr = distanceSqr;
                result = unit;
            }
        }

        return result;
    }

    private void Unit_OnDeadEvent(Unit unit)
    {
        _units.Remove(unit);

        OnUnitDeadEvent?.Invoke(unit);
    }
}
