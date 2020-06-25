using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    //TODO: add GameObject pseudo KS
    public GameObject pseudoWorldCoordinateSystem;

    private PseudoCoordinateSystem ks;

    private Vector3 rotationAxis;

    // Start is called before the first frame update
    void Start()
    {
        ks = pseudoWorldCoordinateSystem.GetComponent<PseudoCoordinateSystem>();

        //set rotation axis
        ConvertToGivenPlane();

        //TRANSLATION
        //this.transform.position += new Vector3(12.0f, 0f, 0f);

        float alpha = 45.0f;
        
        //this.transform.RotateAround(new Vector3(-12.0f,0,0),  new Vector3(0,0,1),45.0f);
        
        //ROTATION AROUND POINT
        //ROTATION BEKOMMT ER ALS ROTATIONS ACHSE UND ALPHA VON DEN WÜRFELN
        this.transform.RotateAround(pseudoWorldCoordinateSystem.transform.position, rotationAxis, alpha);
        
        //SCALE AT POINT
        //SKALIERUNG BEKOMMT ER ALS VECTOR
        //ScaleAround(this.gameObject, new Vector3(-12.0f,0,0), new Vector3(2.0f, 1.0f, 1.0f));
        ScaleAround(this.gameObject, pseudoWorldCoordinateSystem.transform.position, new Vector3(1.0f, 2.0f, 1.0f));
        //this.transform.position.Scale(new Vector3(2.0f, 1.0f, 1.0f));
        
        //this.transform.RotateAround(pseudoWorldCoordinateSystem.transform.position, rotationAxis, -alpha);
        
    }

    private void ConvertToGivenPlane()
    {
        if (ks.XYebene)
        {
            rotationAxis = new Vector3(0f,0f, 1.0f);
        }
        else if (ks.YZebene)
        {
            rotationAxis = new Vector3(1.0f,0f, 0f);
        }
        else if (ks.XZebene)
        {
            rotationAxis = new Vector3(0f,1.0f, 0f);
        }
    }

    //TODO: put in helper class
    public static void ScaleAround(GameObject target, Vector3 pivot, Vector3 newScale)
    {
        Vector3 A = target.transform.localPosition;
        Vector3 B = pivot;
     
        Vector3 C = A - B; // diff from object pivot to desired pivot/origin
     
        float RS = newScale.x / target.transform.localScale.x; // relative scale factor
     
        // calc final position post-scale
        Vector3 FP = B + C * RS;
     
        // finally, actually perform the scale/translation
        target.transform.localScale = newScale;
        target.transform.localPosition = FP;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
