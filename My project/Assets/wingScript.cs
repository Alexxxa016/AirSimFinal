using System;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class wingScript : MonoBehaviour
{
   

    Transform topPlane, bottomPlane;

    public float Radius
    {
        get { return transform.localScale.x / 2; }
        internal set { transform.localScale = (value / 2f) * Vector3.one; }
    }

    internal (bool, float,Vector3) isCollidingWith(Vector3 testPointPosition, float colRadius)
    {
        //print("Point " + testPointPosition.ToString() + " Cylinder " + transform.position.ToString());
       // cylinder check
       Vector3 toTestPoint = testPointPosition - transform.position;
        if (Vector3.Dot(toTestPoint,transform.forward) >0) // if angle between is less than 90 degrees
        {
            //// test collision with cylinder
            // float forward = parallel(toTestPoint,transform.forward).magnitude;
            // float vert = parallel(toTestPoint, transform.up).magnitude;
            // float dist = Mathf.Sqrt(vert * vert + forward * forward);
            // print(parallel(toTestPoint, transform.forward).magnitude);
            // print(parallel(toTestPoint, transform.right).magnitude);

            Vector3 toCylinder = perp(toTestPoint, transform.up);
           float dist= toCylinder.magnitude;
            print(toCylinder.ToString() + "   " +  dist.ToString());
            float overlap =  Radius+colRadius -  dist ;
            if (overlap > 0)
            {
                print("Point " + testPointPosition.ToString() + " Cylinder " + transform.position.ToString());
                print("Cylinder " + overlap.ToString());
            }
                return (overlap > 0, overlap, toTestPoint.normalized);

        }

        // Top 
        toTestPoint = testPointPosition - topPlane.position;
        if (Vector3.Dot(toTestPoint,topPlane.up)>0)
            {
   
            Vector3 perpComp = parallel(toTestPoint, topPlane.up);
            float overlap = colRadius - perpComp.magnitude ;
            //if (overlap > 0) print("Top");
            return (overlap > 0, overlap, perpComp.normalized);
        }

        toTestPoint = testPointPosition - bottomPlane.position;
        if (Vector3.Dot(toTestPoint, -bottomPlane.up) > 0)
        {
            Vector3 perpCompBottom = parallel(toTestPoint, -bottomPlane.up);
            float overlapBottom = colRadius - perpCompBottom.magnitude;
            //if (overlapBottom > 0) print("Bottom");
            return (overlapBottom > 0, overlapBottom, perpCompBottom.normalized);
        }

        return (false, 0, Vector3.zero);
    }


    Vector3 parallel(Vector3 v, Vector3 n)
    {
        return Vector3.Dot(v, n) * n;
    }
    Vector3 perp(Vector3 v, Vector3 n)

    {
        return v - parallel(v,n);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.name =="Top")
                topPlane = child;
            if (child.name =="Bottom")
                bottomPlane = child;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
