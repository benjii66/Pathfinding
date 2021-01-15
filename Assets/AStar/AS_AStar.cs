using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AS_AStar : MonoBehaviour
{
    AS_Node start = null;
    AS_Node end = null;
    AS_Node current = null;
    AS_Node successors = null;
    List<AS_Node> openList = new List<AS_Node>();
    // Start is called before the first frame update
    void Start()
    {
        //null ref car non implémentée
        start.G = 0;
     

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
                        current.Successors[i].Predecessor = current;
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





}

   