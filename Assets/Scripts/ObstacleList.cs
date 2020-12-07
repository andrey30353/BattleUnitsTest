using System.Collections.Generic;
using UnityEngine;

public class ObstacleList : MonoBehaviour
{
    private Obstacle[] _obstacles;

    private void Awake()
    {
        _obstacles = GetComponentsInChildren<Obstacle>();
    }

    public IEnumerable<Vector2Int> GetUnWalkablePositions()
    {
        foreach (var obstacle in _obstacles)
        {
            foreach (var position in obstacle.GetUnWalkablePositions())
            {
                yield return position;
            }
        }
    }    
}
