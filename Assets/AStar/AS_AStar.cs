using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AS_AStar : MonoBehaviour
{
    public event Action<AS_Node> OnGridReady = null;
    AS_Node start = null;
    AS_Node end = null;
    AS_Node current = null;
    AS_Node successors = null;
    List<AS_Node> openList = new List<AS_Node>();

    // Start is called before the first frame update
    void Start()
    {
        start.G = 0;
        InitGrid();

    }

    // Update is called once per frame
    void Update()
    {

    }
    //générer une grille sinon pb avec les transforms positions 
    //grille x par x et chaque node c'est une grille


    void AStarTreatment()
    {
        openList.Add(start);
        while (openList.Count > 0)
        {
            current = openList[0];
            if (current != end)
            {
                openList.Remove(current);
                for (int i = 0; i < current.Successors.Count; i++)
                {
                    float _G = current.G + Vector3.Distance(current.Position, current.Successors[i].Position);
                    if (_G < current.Successors[i].G)
                    {
                        current.Successors[i] = current;
                        current.Successors[i].G = _G;
                        current.Successors[i].H = Vector3.Distance(current.Position, end.Position);
                        if (successors.Successors[i] != openList[i])
                        {
                            openList.Add(successors.Successors[i]);
                            //remonte la liste et le path se fera
                            openList.Reverse();
                        }
                    }
                }
            }
        }
    }



    int size = 4;


    public int GridSize => size;

    public void InitGrid()
    {
       List<AS_Node> listeh = new List<AS_Node>();
        for (int _x = 0; _x < openList.Count; _x++)
            for (int _z = 0; _z < openList.Count; _z++)
            {
                AS_Node _node = null;
                AStarTreatment();
                OnGridReady?.Invoke(_node);
            }

    }

    void OnDestroy()
    {
        OnGridReady = null;
    }

    //FINIR GRILLE(AVEC SUCCESSOR) ET SI ON EST OK ON FAIT L'ALGO


    private void OnDrawGizmos()
    {
        for (int _x = 0; _x < size; _x++)
        {
            for (int _z = 0; _z < size; _z++)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireCube(transform.position + new Vector3(_x, 0, _z), Vector3.one * .5f);

            }
        }
    }


}

   