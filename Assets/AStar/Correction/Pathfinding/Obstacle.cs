using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Obstacle : MonoBehaviour
{
    [SerializeField] Collider obstacleCollider = null;

    public bool IsValid => obstacleCollider;

    private void Awake()
    {
        CAS_GridManager.OnGridObstacleCheck += () => CAS_GridManager.Instance.CheckForObstacle(this);
    }

    public bool ContainsNode(AS_Node _node)
    {
        if (!IsValid) return false;
        return obstacleCollider.bounds.Contains(_node.Position);
    }

    private void OnDrawGizmos()
    {
        if (!IsValid) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(obstacleCollider.bounds.center, obstacleCollider.bounds.size);
    }

}
