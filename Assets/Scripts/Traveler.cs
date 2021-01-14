using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traveler : MonoBehaviour
{
    Vector3 target;
    [SerializeField] Point point = null;

    public void SetTarget(Vector3 _target) => target = _target;
    public bool IsAtPos => Vector3.Distance(target, transform.position) < .01f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveTo();   
    }


    void MoveTo()
    {
        SetTarget(point.ClosestTarget());
        if (Vector3.Distance(transform.position, target) > point.DistancePointArrived(target))
        {
            Debug.Log("trop loin");
        }

        if(IsAtPos)
        {
            Debug.Log($"t'es en position à {point.transform.name}");
            return;
        }
        //la target va être le vecteur DE la plus courte distance
        transform.position =  Vector3.MoveTowards(transform.position,target, Time.deltaTime);
        transform.LookAt(target);

    }
}
