using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AS_Node 
{

    public Vector3 Position { get; private set; } = Vector3.zero;

    public bool IsNaviguable { get; private set; } = true;

    //F != infini = ça a déjà été parcouru
    public float F => G + H; //poids total des deux
    public float G { get; set; } = float.MaxValue;// node par rapport au start
    public float H { get; set; } = float.MaxValue; //heuristic -> node par rapport à l'arrivé

    public AS_Node Predecessor { get; set; } = null;
    public List<AS_Node> Successors { get; set; } = new List<AS_Node>();


    public AS_Node(float _x, float _y, float _z)
    {
        Position = new Vector3(_x, _y, _z);


    }

    public void AddSuccessors(AS_Node _node)=> Successors.Add(_node);
    
}

/**
 * start = node 0
 * end = node x(on sait pas où il est)
 * start.G = 0; (node au départ quoi)
 * open.Add(start) liste ouverte de type Node
 * while open.Count > 0 
 * do current = open[0];
 * if (current != end) sinon on return le path car current == end
 * open.Remove(current); //tu peux l'add dans une close list (tous les nodes que tu jettes)
 * for ( parcours tous les succesors de current)
 * G temporaire (float _G) = _current.G + distance(current.position, successors[i].position);
 * if(_g < _current.Successors[i].G)
 *  _c.p[i] = _current; //je suis le prédécessors du successor                        s = successor       c = current            g = node par rapport au start      h = node par rapport au end
 *  _c.s[i].G = _g;
 *  _c.s[i].H = distance(_c.position, _end.position); //heuristic réduit sinon on va vers l'infini quoi
 *  if(_s[i] != open //if naviguable -> _open.Add(s[i]) //si le successor est différent de l'open, on regarde si il est naviguable si oui on ajoute le successeur
 *  
 *  après on remonte la file pour avoir le chemin entier
 *  
 */