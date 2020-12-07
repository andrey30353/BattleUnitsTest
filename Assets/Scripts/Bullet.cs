using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _distanceThreshold = 0.2f;

    private Unit _target;   
    private float _damage;
    
    public void Init(Unit target, float damage)
    {
        _target = target;
        _damage = damage;

        var dir = (_target.transform.position - transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);        
    }   

    private void Update()
    {   
        if (_target != null)
        {
            var directionVector = _target.transform.position - transform.position;
            var translation = directionVector.normalized * _speed * Time.deltaTime;
            transform.position += translation;

            var distanceSqr = directionVector.sqrMagnitude;
            if(distanceSqr <= _distanceThreshold * _distanceThreshold)
            {
                _target.TakeDamage(_damage);

                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject, 1f);
        }
    }
}
