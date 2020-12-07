using EpPathFinding.cs;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitData _data;
    [SerializeField] private Bullet _bulletPrefab;

    public Unit Target;

    public event Action<Unit> OnDeadEvent;

    private float _distanceThreshold = 0.2f;

    private List<GridPos> _path;
    private int _pathIndex = 1;    

    private float _hp;
    private float _attackProcess;

    private void Awake()
    {
        _hp = _data.Hp;
        _attackProcess = 0;
    }

    public Vector3Int GetPositionInt()
    {
        return new Vector3Int(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z));
    }

    public GridPos GetGridPosition()
    {
        return new GridPos(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
    }

    private void Update()
    {
        _attackProcess += Time.deltaTime;

        if (_path != null && _path.Count > 0)
        {
            var movePoint = _path[_pathIndex];
            var pointPosition = movePoint.GetPosition();
            var distance = (pointPosition - transform.position).sqrMagnitude;
            if (distance <= _distanceThreshold * _distanceThreshold)
            {
                _pathIndex++;
                if (_pathIndex >= _path.Count)
                {
                    _path = null;
                    _pathIndex = 1;
                    return;
                }
                movePoint = _path[_pathIndex];
            }

            var translation = (movePoint.GetPosition() - transform.position).normalized * _data.Speed * Time.deltaTime;
            transform.position += translation;

            transform.LookAt(pointPosition);
        }
       
        if (Target != null)
        {
            var distanceSqr = (Target.transform.position - transform.position).sqrMagnitude;
            if(distanceSqr <= _data.AttackRange * _data.AttackRange)
            {
                _path = null;
                if(_attackProcess >= _data.AttackDelay)
                {
                    Attack(Target);                   
                }
            }
        }
    }

    private void Attack(Unit target)
    {        
        transform.LookAt(target.transform.position);

        if (_bulletPrefab == null)
        {          
            target.TakeDamage(_data.Damage);
        }
        else
        {
            var bullet = Instantiate(_bulletPrefab);           
            bullet.Init(target, _data.Damage);
            bullet.transform.position = transform.position;
        }

        _attackProcess = 0;
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;
        if(_hp <= 0)
        {           
            OnDeadEvent?.Invoke(this);            
            Destroy(gameObject);
        }
    }

    public void SetPath(List<GridPos> path)
    {
        _path = path;
        _pathIndex = 1;
    }
}
