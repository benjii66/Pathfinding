using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CAS_GridManager : Singleton<CAS_GridManager>
{
    public static event Action OnGridReady = null;
    public static event Action OnGridObstacleCheck = null;
    public event Action OnDrawGridGizmos = null;


    [SerializeField, Range(2, 10)] int sizeGrid = 5;
    public List<AS_Node> AllNodes { get; private set; } = new List<AS_Node>();


    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid(sizeGrid);
        GetSuccessors();
        OnGridReady?.Invoke();

    }

    void GenerateGrid(int _sizeGrid)
    {
        for (int x = 0; x < sizeGrid; x++)
        {
            for (int y = 0; y < sizeGrid; y++)
            {
                AS_Node _node = new AS_Node(x, 0, y);
                AllNodes.Add(_node);
                OnDrawGridGizmos += () =>
                {
                    _node.DrawNode();
                    _node.DrawSuccessors();
                };
            }
        }
        OnGridObstacleCheck?.Invoke();
    }

    public void CheckForObstacle(Obstacle _obstacle)
    {
        for (int i = 0; i < AllNodes.Count; i++)
        {
            if (AllNodes[i].IsNaviguable && _obstacle.ContainsNode(AllNodes[i]))
            {
                AllNodes[i].SetNaviguable(false);
            }
        }
    }

    void GetSuccessors()
    {
        for (int i = 0; i < sizeGrid * sizeGrid; i++)
        {

            bool _canRight = i % sizeGrid != sizeGrid - 1;
            bool _canTop = i >= sizeGrid;
            bool _canDown = i < (sizeGrid * sizeGrid) - sizeGrid;
            bool _canLeft = i % sizeGrid != 0;

            if (_canRight) AllNodes[i].AddSuccessors(AllNodes[i + 1]);
            if (_canLeft) AllNodes[i].AddSuccessors(AllNodes[i - 1]);
            if (_canTop)
            {
                AllNodes[i].AddSuccessors(AllNodes[i - sizeGrid]);
                if (_canRight) AllNodes[i].AddSuccessors(AllNodes[i + 1 - sizeGrid]);
                if (_canLeft) AllNodes[i].AddSuccessors(AllNodes[i - 1 - sizeGrid]);
            }
            if (_canDown)
            {
                AllNodes[i].AddSuccessors(AllNodes[i + sizeGrid]);
                if (_canRight) AllNodes[i].AddSuccessors(AllNodes[i + 1 + sizeGrid]);
                if (_canLeft) AllNodes[i].AddSuccessors(AllNodes[i - 1 + sizeGrid]);
            }
        }
    }
    public AS_Node GetNearestNode(Vector3 _position) => AllNodes.Where(n => n.IsNaviguable).OrderBy(n => Vector3.Distance(n.Position, _position)).FirstOrDefault();

    public void ResetGridCost()
    {
        AllNodes.ForEach(n => n.ResetCost());
    }


    private void OnDrawGizmos()
    {
        DrawGrid();
        OnDrawGridGizmos?.Invoke();
    }


    void DrawGrid()
    {
        if (Application.isPlaying) return;
        for (int i = 0; i < sizeGrid; i++)
        {
            for (int j = 0; j < sizeGrid; j++)
            {
                Gizmos.DrawWireSphere(new Vector3(i, 0, j), .1f);
            }

        }
    }


    private void OnDestroy()
    {
        OnGridReady = null;
        OnDrawGridGizmos = null;
    }
}
