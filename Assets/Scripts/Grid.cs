using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EpPathFinding.cs;

public class Grid : MonoBehaviour
{
    [SerializeField] private ObstacleList _obstacles;

    private Vector2Int _size = new Vector2Int(80, 80);

    private StaticGrid _grid;

    private void Awake()
    {   
        bool[][] movableMatrix = new bool[_size.x][];
        for (int x = 0; x < _size.x; x++)
        {
            movableMatrix[x] = new bool[_size.y];
            for (int y = 0; y < _size.y; y++)
            {
                movableMatrix[x][y] = true;
            }
        }

        _grid = new StaticGrid(_size.x, _size.y, movableMatrix);

        var unWalkablePosition = _obstacles.GetUnWalkablePositions();
        foreach (var position in unWalkablePosition)
        {
            _grid.SetWalkableAt(position.x, position.y, false);          
        } 
    }

    public List<GridPos> GetPath(GridPos startPos, GridPos endPos)
    {      
        var jpParam = new JumpPointParam(_grid, startPos, endPos, EndNodeUnWalkableTreatment.ALLOW, DiagonalMovement.OnlyWhenNoObstacles);        
        var resultPathList = JumpPointFinder.FindPath(jpParam);
              
        _grid.Reset();
        return resultPathList;
    }

    private static void DebugPath(List<GridPos> resultPathList)
    {       
        if (resultPathList.Count >= 2)
        {
            var begin = resultPathList[0];
            for (int i = 1; i < resultPathList.Count; i++)
            {
                Debug.DrawLine(begin.GetPosition(), resultPathList[i].GetPosition(), Color.red, 100);
                begin = resultPathList[i];
            }
        }
    }
}
