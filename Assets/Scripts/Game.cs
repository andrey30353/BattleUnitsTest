using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Grid _grid;

    [SerializeField] private ArmyController _humanController;
    [SerializeField] private ArmyController _zombieController;

    [SerializeField] private float _updatePathTimeout = 2f;
    private float _updatePathProcess = 0f;

    public int ZombiesCount => _zombieController.Units.Count;
    public int HumansCount => _humanController.Units.Count;

    public event Action OnUnitCountChangeEvent;

    private void Start()
    {
        FindTargets();
    }

    private void OnEnable()
    {
        _humanController.OnUnitDeadEvent += UnitDead;
        _zombieController.OnUnitDeadEvent += UnitDead;
    }

    private void OnDisable()
    {
        _humanController.OnUnitDeadEvent -= UnitDead;
        _zombieController.OnUnitDeadEvent -= UnitDead;
    }

    private void Update()
    {
        if (ZombiesCount == 0 || HumansCount == 0)
            return;

        _updatePathProcess += Time.deltaTime;
        if(_updatePathProcess >= _updatePathTimeout)
        {
            FindTargets();
            _updatePathProcess = 0f;
            return;
        }

        foreach (var human in _humanController.Units)
        {
            if (human.Target == null)
                FindTargetFor(human, _zombieController);           
        }

        foreach (var human in _humanController.Units)
        {
            if (human.Target == null)
                FindTargetFor(human, _zombieController);
        }
    }

    private void FindTargetFor(Unit unit, ArmyController enemies)
    {
        var position = unit.GetPositionInt();
        var target = enemies.GetNearestUnit(position);

        if (target == null)
            return;

        unit.Target = target;

        var path = _grid.GetPath(unit.GetGridPosition(), target.GetGridPosition());
        unit.SetPath(path);

        //Debug.DrawLine(unit.GetPositionInt(), target.GetPositionInt(), Color.blue, 60f);
    }

    private void FindTargets()
    {
        foreach (var human in _humanController.Units)
        {
            FindTargetFor(human, _zombieController);
        }

        foreach (var zombie in _zombieController.Units)
        {
            FindTargetFor(zombie, _humanController);
        }
    }    

    private void UnitDead(Unit unit)
    {
        OnUnitCountChangeEvent?.Invoke();
    }
}
