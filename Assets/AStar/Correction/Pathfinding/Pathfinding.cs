using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pathfinding
{
    public Action<Path> OnPathCompleted = null;

    public void AskForPath(AS_Node _start, AS_Node _end)
    {

        CAS_GridManager.Instance.ResetGridCost();
        List<AS_Node> _openList = new List<AS_Node>(), _closeList = new List<AS_Node>();
        _start.G = 0;
        _openList.Add(_start);
        while (_openList.Count != 0)
        {
            AS_Node _current = _openList[0];
            if (_current == _end)
            {
                //on est arrivés
                Path _path = new Path(_start, _end);
                OnPathCompleted?.Invoke(_path);
                return;
            }
            //sécurité
            _openList.Remove(_current);
            _closeList.Add(_current);

            for (int i = 0; i < _current.Successors.Count; i++)
            {

                AS_Node _successor = _current.Successors[i];
                float _g = _current.G + Vector3.Distance(_current.Position, _successor.Position);
                if (_g < _successor.G)
                {
                    _successor.Predecessor = _current;
                    _successor.G = _g;
                    _successor.H = Vector3.Distance(_successor.Position, _end.Position);
                    if (!_openList.Contains(_successor) && _successor.IsNaviguable)
                        _openList.Add(_successor);

                }
            }
        }
    }


}


public class Path
{
    public List<AS_Node> FinalPath { get; private set; } = new List<AS_Node>();

    public Path(AS_Node _startNode, AS_Node _endNode)
    {

        FinalPath = GetFinalPath(_startNode, _endNode);

    }

    List<AS_Node> GetFinalPath(AS_Node _startNode, AS_Node _endNode)
    {
        List<AS_Node> _path = new List<AS_Node>();
        AS_Node _current = _endNode;
        _path.Add(_current);
        while (_current != _startNode)
        {
            _current = _current.Predecessor;
            _path.Add(_current);
        }
        _path.Reverse();
        return _path;
    }

    public void DrawPath()
    {
        Gizmos.color = Color.cyan;
        for (int i = 0; i < FinalPath.Count; i++)
        {
            Gizmos.color = Color.red;
            if (i < FinalPath.Count - 1)
            {

                Gizmos.DrawLine(FinalPath[i].Position + Vector3.up, FinalPath[i + 1].Position + Vector3.up);
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireCube(FinalPath[i].Position + Vector3.up, Vector3.one * 0.1f);
            }
        }
        Gizmos.color = Color.cyan;

    }
}