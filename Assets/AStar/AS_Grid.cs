using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
public class AS_Grid : MonoBehaviour
{
	public event Action<AS_Node> OnGridReady = null;
	List<AS_Node> Nodes = new List<AS_Node>();


	[SerializeField] int height = 0;
	[SerializeField] int width = 0;
	// Start is called before the first frame update
	void Start()
	{
		InitGrid();

	}

	public void InitGrid()
	{
		for (int _x = 0; _x < width; _x++)
			for (int _z = 0; _z < height; _z++)
			{
				AS_Node _node = new AS_Node(_x, 0, _z);
				Nodes.Add(_node);
				OnGridReady?.Invoke(_node);
			}

		Treatment();


	}

	void Treatment()
	{
		for (int i = 0; i < Nodes.Count; i++)
		{
			if (i < Nodes.Count - 1 && (i % width) + 1 != width)
				Nodes[i].AddSuccessors(Nodes[i + 1]);

			if (i + width <= Nodes.Count - 1)
			{
				Nodes[i].AddSuccessors(Nodes[i + width]);
				if ((i % width) + 1 != 1)
					Nodes[i].AddSuccessors(Nodes[i + width - 1]);
				if (i + width + 1 <= Nodes.Count - 1 && (i % width) + 1 != width)
					Nodes[i].AddSuccessors(Nodes[i + width + 1]);
			}
			if (i - width >= 0)
			{
				Nodes[i].AddSuccessors(Nodes[i - width]);
				if (i - width - 1 >= 0 && (i % width) + 1 != 1)
					Nodes[i].AddSuccessors(Nodes[i - width - 1]);
				//
				Nodes[i].AddSuccessors(Nodes[i - width + 1]);
			}
			if (i - 1 >= 0 && i % width != 0)
				Nodes[i].AddSuccessors(Nodes[i - 1]);
			Debug.Log($"Point {i} got : {Nodes[i].Successors.Count} successors");
		}
	}

	void OnDestroy()
	{
		OnGridReady = null;
	}

	//FINIR GRILLE(AVEC SUCCESSOR) ET SI ON EST OK ON FAIT L'ALGO


	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		for (int i = 0; i < Nodes.Count; i++)
		{
			Gizmos.DrawWireCube(Nodes[i].Position, Vector3.one * .5f);
			for (int j = 0; j < Nodes[i].Successors.Count; j++)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawLine(Nodes[i].Position, Nodes[i].Successors[j].Position);

			}
				Gizmos.color = Color.cyan;
		}


	}
}
