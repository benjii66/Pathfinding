using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] Transform[] neighbours = null;
    [SerializeField] bool isStart = false, isArrived = false;
    [SerializeField] Color pathColor = Color.white;
    [SerializeField] Vector3[] neighboursPosition = null;
    public bool IsPassed = false;

    public bool IsStart => isStart;
    public bool IsArrived => isArrived;


    public Vector3 startVector = Vector3.zero;
    public Vector3 endVector = Vector3.zero;
    public Vector3 ClosestVector = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        SetStartVector();
        SetEndVector();
        DistanceCtoC();
        DistancePointArrived(transform.position);
    }

    private void Awake()
    {
        FillPosition();
    }
    // Update is called once per frame
    void Update()
    {
    }


    public Vector3 ClosestTarget()
    {
        for (int i = 0; i < neighbours.Length; i++)
        {
            if(neighbours[i].position.x < ClosestVector.x && neighbours[i].position.y < ClosestVector.y && neighbours[i].position.z < ClosestVector.z)
            neighbours[i].position = ClosestVector;

        }

        return ClosestVector;
    }

    void FillPosition()
    {
        for (int i = 0; i < neighbours.Length; i++)
        {
            neighboursPosition[i].x = neighbours[i].position.x;
            neighboursPosition[i].y = neighbours[i].position.y;
            neighboursPosition[i].z = neighbours[i].position.z;
        }
    }


    public float DistanceCtoC()
    {
        float _dist = 0;
        for (int i = 0; i < neighboursPosition.Length; i++)
        {
            _dist = Vector3.Distance(transform.position, neighboursPosition[i]);
        }
        return _dist;
    }

    public Vector3 SetStartVector()
    {
        Vector3 _startPos = Vector3.zero;
        if (IsStart)
        {
            _startPos = transform.position;
        }
        startVector = _startPos;
        return startVector;
    }

    public Vector3 SetEndVector()
    {
        Vector3 _EndPos = Vector3.zero;

        if (IsArrived)
        {
            _EndPos = transform.position;
           

        }
        endVector = _EndPos;
        return endVector;
    }

    public float DistancePointArrived(Vector3 _target)
    {
        float _distanceStartEnd = 0;
        _distanceStartEnd = Vector3.Distance(_target, endVector);
        return _distanceStartEnd;
    }


    private void OnDrawGizmos()
    {
        if (isStart)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position + Vector3.up * 3, .5f);
        }

        if (isArrived)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + Vector3.up * 3, .5f);
        }

        Gizmos.color = pathColor;
        for (int i = 0; i < neighbours.Length; i++)
        {
            Gizmos.DrawLine(transform.position, neighbours[i].position);
        }

    }
}
