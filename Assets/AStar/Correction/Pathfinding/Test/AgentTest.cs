using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentTest : MonoBehaviour
{
    public event Action OnDrawAgentGizmos = null;
    [SerializeField] Transform target = null;

    Pathfinding pathFinding = new Pathfinding();

    public bool IsValid => target;


    private void Awake()
    {
        CAS_GridManager.OnGridReady += AskPath;
        pathFinding.OnPathCompleted += GetPath;
    }


    private void Update()
    {
        MoveTo();
    }

    void AskPath()
    {
        if (!IsValid) return;
        pathFinding.AskForPath(CAS_GridManager.Instance.GetNearestNode(transform.position), CAS_GridManager.Instance.GetNearestNode(target.position));
    }

    void GetPath(Path _path)
    {
        OnDrawAgentGizmos = null;
        OnDrawAgentGizmos += _path.DrawPath;
    }

    void MoveTo()
    {
        //for (int i = 0; i < Path.FinalPath.Count; i++)
        //{
        //pour faire avancer le cube
        //    transform.position = Vector3.MoveTowards(transform.position, Path.FinalPath[i].Position, Time.deltaTime * 3);
        //}
    }

    private void OnDrawGizmos()
    {
        OnDrawAgentGizmos?.Invoke();
    }

    private void OnDestroy()
    {
        CAS_GridManager.OnGridReady -= AskPath;
        pathFinding.OnPathCompleted -= GetPath;
        OnDrawAgentGizmos = null;
    }
}
