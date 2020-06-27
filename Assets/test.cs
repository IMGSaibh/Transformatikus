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
        //this.transform.RotateAround(pseudoWorldCoordinateSystem.transform.position, rotationAxis, alpha);

        //this.transform.RotateAround(pseudoWorldCoordinateSystem.transform.position, rotationAxis, alpha);

        transform.RotateAround(pseudoWorldCoordinateSystem.transform.position, Vector3.left, 90);

        //SCALE AT POINT
        //SKALIERUNG BEKOMMT ER ALS VECTOR
        //ScaleAround(this.gameObject, new Vector3(-12.0f,0,0), new Vector3(2.0f, 1.0f, 1.0f));

        //ScaleAround(this.gameObject, pseudoWorldCoordinateSystem.transform.position, new Vector3(3.0f, 1.0f, 1.0f));

        //this.transform.position.Scale(new Vector3(2.0f, 1.0f, 1.0f));
        //scaleObject(pseudoWorldCoordinateSystem.transform, 3f, 1f, 1f);
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
    

    void scaleObject(Transform koordsystem, Vector3 scaleVector) 
    {
        transform.SetParent(koordsystem);
        koordsystem.localScale = scaleVector; 
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
