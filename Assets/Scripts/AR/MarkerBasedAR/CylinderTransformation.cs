using System;
using UnityEngine;

namespace MarkerBasedARExample.MarkerBasedAR
{
    public class CylinderTransformation : MonoBehaviour
    {
        /// <summary>
        /// The AR game object.
        /// </summary>
        public GameObject item;
        
        [System.Serializable]
        public enum CylinderType
        {
            Cylinder_Alpha,
            Cylinder_XYZ
        }

        public CylinderType type;
        
        [System.NonSerialized]
        public Skalar testTrans;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (item.activeSelf)
            {
                switch (type)
                {
                    case CylinderType.Cylinder_Alpha:
                    {
                        //Snapping auf 15 Grad-Schritte
                        float alpha = (Mathf.Round(item.gameObject.transform.rotation.eulerAngles.z / 15.0f) * 15.0f);

                        //Debug.Log("Alpha: " + alpha);

                        String[] skalar =
                        {
                            "" + alpha
                        };

                        testTrans = new Skalar(skalar, alpha);

                        break;
                    }
                    case CylinderType.Cylinder_XYZ:
                    {
                        //Snapping auf 15 Grad-Schritte
                        float rotation_z = (Mathf.Round(item.gameObject.transform.rotation.eulerAngles.z / 15.0f) *
                                            15.0f);

                        //float test = (rotation_z - 180.0f);
                        //float skalar = Mathf.Round((test / 18.0f));
                        float skalar = Mathf.Round((rotation_z / 18.0f));

                        //Debug.Log("Skalar: " + skalar);

                        String[] skalarString =
                        {
                            "" + skalar
                        };

                        testTrans = new Skalar(skalarString, skalar);

                        break;
                    }
                }
            }
        }
    }
}