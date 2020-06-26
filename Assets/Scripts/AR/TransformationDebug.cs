using System;
using MarkerBasedARExample.MarkerBasedAR;
using OpenCVMarkerBasedAR;
using UnityEngine;

namespace MarkerBasedARExample
{
    public class TransformationDebug : MonoBehaviour
    {
        /// <summary>
        /// The AR game object.
        /// </summary>
        public GameObject item;

        public IntMatrix transformationMatrix;

        private Matrix4x4 transMatrix;

        //TODO: vielleicht noch den Vector füllen
        private Vector3 transVector;

        public Transformation testTrans;

        public CylinderTransformation cylinderObject;

        // Start is called before the first frame update
        void Start()
        {
            testTrans = new Matrix();
        }

        // Update is called once per frame
        void Update()
        {
            if (item.activeSelf)
            {

                switch (transformationMatrix.elementType)
                {
                    case IntMatrix.ElementTypes.Objekt:
                    {
                        testTrans = new Objekt();

                        break;
                    }
                    case IntMatrix.ElementTypes.Pivot:
                    {
                        testTrans = new Pivot();

                        break;
                    }
                    case IntMatrix.ElementTypes.Vector:
                    {
                        //get XYZ from cylinder
                        if (cylinderObject.item.activeSelf &&
                            cylinderObject.type == CylinderTransformation.CylinderType.Cylinder_XYZ)
                        {
                            float alpha = cylinderObject.testTrans.skalar;

                            String[] vector =
                            {
                                "    1    ",
                                "    0    ",
                                "    0    ",
                            };

                            Vector3 translationVector = new Vector3(alpha, 0f, 0f);

                            //vectorTransformation = new VectorTransformation(vector, translationVector);

                            testTrans = new Vector(vector, translationVector);
                        }

                        break;
                    }
                    case IntMatrix.ElementTypes.Vector_X:
                    {
                        //get XYZ from cylinder
                        if (cylinderObject.item.activeSelf &&
                            cylinderObject.type == CylinderTransformation.CylinderType.Cylinder_XYZ)
                        {
                            float alpha = cylinderObject.testTrans.skalar;

                            String[] vector =
                            {
                                "  " + alpha + "  ",
                                "  0  ",
                                "  0  ",
                            };

                            Vector3 translationVector = new Vector3(alpha, 0f, 0f);

                            testTrans = new Vector(vector, translationVector);
                        }
                        else
                        {
                            //else set ? for the value
                            String alpha = "?";

                            String[] vector =
                            {
                                "  " + alpha + "  ",
                                "  0  ",
                                "  0  ",
                            };

                            Vector3 translationVector = new Vector3(0f, 0f, 0f);

                            testTrans = new Vector(vector, translationVector);
                        }

                        break;
                    }
                    case IntMatrix.ElementTypes.Vector_Y:
                    {
                        //get XYZ from cylinder
                        if (cylinderObject.item.activeSelf &&
                            cylinderObject.type == CylinderTransformation.CylinderType.Cylinder_XYZ)
                        {
                            float alpha = cylinderObject.testTrans.skalar;

                            String[] vector =
                            {
                                "  0  ",
                                "  " + alpha + "  ",
                                "  0  ",
                            };

                            Vector3 translationVector = new Vector3(0f, alpha, 0f);

                            testTrans = new Vector(vector, translationVector);
                        }
                        else
                        {
                            //else set ? for the value
                            String alpha = "?";

                            String[] vector =
                            {
                                "  0  ",
                                "  " + alpha + "  ",
                                "  0  ",
                            };

                            Vector3 translationVector = new Vector3(0f, 0f, 0f);

                            testTrans = new Vector(vector, translationVector);
                        }

                        break;
                    }
                    case IntMatrix.ElementTypes.Vector_Z:
                    {
                        //get XYZ from cylinder
                        if (cylinderObject.item.activeSelf &&
                            cylinderObject.type == CylinderTransformation.CylinderType.Cylinder_XYZ)
                        {
                            float alpha = cylinderObject.testTrans.skalar;

                            String[] vector =
                            {
                                "  0  ",
                                "  0  ",
                                "  " + alpha + "  "
                            };

                            Vector3 translationVector = new Vector3(0f, 0f, alpha);

                            testTrans = new Vector(vector, translationVector);
                        }
                        else
                        {
                            //else set ? for the value
                            String alpha = "?";

                            String[] vector =
                            {
                                "  0  ",
                                "  0  ",
                                "  " + alpha + "  "
                            };

                            Vector3 translationVector = new Vector3(0f, 0f, 0f);

                            testTrans = new Vector(vector, translationVector);
                        }

                        break;
                    }
                    case IntMatrix.ElementTypes.Vector_X_Transponiert:
                    {
                        //TODO: transponierter Vektor realisieren

                        //get XYZ from cylinder
                        if (cylinderObject.item.activeSelf &&
                            cylinderObject.type == CylinderTransformation.CylinderType.Cylinder_XYZ)
                        {
                            float alpha = cylinderObject.testTrans.skalar;

                            String[] vector =
                            {
                                "  " + alpha + "  ", "  0  ", "  0  "
                            };

                            Vector3 translationVector = new Vector3(alpha, 0f, 0f);

                            testTrans = new Vector(vector, translationVector);
                        }
                        else
                        {
                            //else set ? for the value
                            String alpha = "?";

                            String[] vector =
                            {
                                "  " + alpha + "  ", "  0  ", "  0  "
                            };

                            Vector3 translationVector = new Vector3(0f, 0f, 0f);

                            testTrans = new Vector(vector, translationVector);
                        }

                        break;
                    }
                    case IntMatrix.ElementTypes.Vector_Y_Transponiert:
                    {
                        //TODO: transponierter Vektor realisieren --> brauchen wir eher nicht

                        //get XYZ from cylinder
                        if (cylinderObject.item.activeSelf &&
                            cylinderObject.type == CylinderTransformation.CylinderType.Cylinder_XYZ)
                        {
                            float alpha = cylinderObject.testTrans.skalar;

                            String[] vector =
                            {
                                "  0  ", "  " + alpha + "  ", "  0  "
                            };

                            Vector3 translationVector = new Vector3(0f, alpha, 0f);

                            testTrans = new Vector(vector, translationVector);
                        }
                        else
                        {
                            //else set ? for the value
                            String alpha = "?";

                            String[] vector =
                            {
                                "  0  ", "  " + alpha + "  ", "  0  "
                            };

                            Vector3 translationVector = new Vector3(0f, 0f, 0f);

                            testTrans = new Vector(vector, translationVector);
                        }

                        break;
                    }
                    case IntMatrix.ElementTypes.Matrix:
                    {
                        break;
                    }
                    case IntMatrix.ElementTypes.Operation:
                    {
                        String[] operation =
                        {
                            transformationMatrix.operation
                        };

                        testTrans = new Operation(operation);

                        break;
                    }
                    case IntMatrix.ElementTypes.Rotation_X:
                    {
                        //get alpha from cylinder
                        if (cylinderObject.item.activeSelf &&
                            cylinderObject.type == CylinderTransformation.CylinderType.Cylinder_Alpha)
                        {
                            float alpha = cylinderObject.testTrans.skalar;

                            String[] matrix =
                            {
                                "    1    ", "    0    ", "    0    ",
                                "    0    ", "cos(" + alpha + ")", "-sin(" + alpha + ")",
                                "    0    ", "sin(" + alpha + ")", "cos(" + alpha + ")"
                            };

                            //create object that holds textual matrix and the rel matrix
                            Vector4 column1 = new Vector4(1f, 0f, 0f, 0f);
                            Vector4 column2 = new Vector4(0f, (float) Math.Cos(alpha), (float) -Math.Sin(alpha), 0f);
                            Vector4 column3 = new Vector4(0f, (float) Math.Sin(alpha), (float) Math.Cos(alpha), 0f);
                            Vector4 column4 = new Vector4(0f, 0f, 0f, 1f);

                            transMatrix = new Matrix4x4(column1, column2, column3, column4);

                            testTrans = new Matrix(matrix, transMatrix);
                        }
                        else
                        {
                            //else set ? for the value
                            String alpha = "?";

                            String[] matrix =
                            {
                                "    1    ", "    0    ", "    0    ",
                                "    0    ", "cos(" + alpha + ")", "-sin(" + alpha + ")",
                                "    0    ", "sin(" + alpha + ")", "cos(" + alpha + ")"
                            };

                            Vector4 column1 = new Vector4(1f, 0f, 0f, 0f);
                            Vector4 column2 = new Vector4(0f, 1f, 0f, 0f);
                            Vector4 column3 = new Vector4(0f, 0f, 1f, 0f);
                            Vector4 column4 = new Vector4(0f, 0f, 0f, 1f);

                            transMatrix = new Matrix4x4(column1, column2, column3, column4);

                            testTrans = new Matrix(matrix, transMatrix);
                        }

                        break;
                    }
                    case IntMatrix.ElementTypes.Rotation_Y:
                    {
                        //get alpha from cylinder
                        if (cylinderObject.item.activeSelf &&
                            cylinderObject.type == CylinderTransformation.CylinderType.Cylinder_Alpha)
                        {
                            float alpha = cylinderObject.testTrans.skalar;

                            String[] matrix =
                            {
                                "cos(" + alpha + ")", "    0    ", "sin(" + alpha + ")",
                                "    0    ", "    1    ", "    0    ",
                                "-sin(" + alpha + ")", "     0    ", "cos(" + alpha + ")"
                            };

                            Vector4 column1 = new Vector4((float) Math.Cos(alpha), 0f, (float) Math.Sin(alpha), 0f);
                            Vector4 column2 = new Vector4(0f, 1f, 0f, 0f);
                            Vector4 column3 = new Vector4((float) -Math.Sin(alpha), 0f, (float) Math.Cos(alpha), 0f);
                            Vector4 column4 = new Vector4(0f, 0f, 0f, 1f);

                            transMatrix = new Matrix4x4(column1, column2, column3, column4);

                            testTrans = new Matrix(matrix, transMatrix);
                        }
                        else
                        {
                            //else set ? for the value
                            String alpha = "?";

                            String[] matrix =
                            {
                                "cos(" + alpha + ")", "    0    ", "sin(" + alpha + ")",
                                "    0    ", "    1    ", "    0    ",
                                "-sin(" + alpha + ")", "     0    ", "cos(" + alpha + ")"
                            };

                            Vector4 column1 = new Vector4(1f, 0f, 0f, 0f);
                            Vector4 column2 = new Vector4(0f, 1f, 0f, 0f);
                            Vector4 column3 = new Vector4(0f, 0f, 1f, 0f);
                            Vector4 column4 = new Vector4(0f, 0f, 0f, 1f);

                            transMatrix = new Matrix4x4(column1, column2, column3, column4);

                            testTrans = new Matrix(matrix, transMatrix);
                        }

                        break;
                    }
                    case IntMatrix.ElementTypes.Rotation_Z:
                    {
                        //get alpha from cylinder
                        if (cylinderObject.item.activeSelf &&
                            cylinderObject.type == CylinderTransformation.CylinderType.Cylinder_Alpha)
                        {
                            float alpha = cylinderObject.testTrans.skalar;

                            String[] matrix =
                            {
                                "cos(" + alpha + ")", "-sin(" + alpha + ")", "   0    ",
                                "sin(" + alpha + ")", "cos(" + alpha + ")", "    0    ",
                                "    0    ", "    0    ", "    1    "
                            };

                            Vector4 column1 = new Vector4((float) Math.Cos(alpha), (float) -Math.Sin(alpha), 0f, 0f);
                            Vector4 column2 = new Vector4((float) Math.Sin(alpha), (float) Math.Cos(alpha), 0f, 0f);
                            Vector4 column3 = new Vector4(0f, 0f, 1f, 0f);
                            Vector4 column4 = new Vector4(0f, 0f, 0f, 1f);

                            transMatrix = new Matrix4x4(column1, column2, column3, column4);

                            testTrans = new Matrix(matrix, transMatrix);
                        }
                        else
                        {
                            //else set ? for the value
                            String alpha = "?";

                            String[] matrix =
                            {
                                "cos(" + alpha + ")", "-sin(" + alpha + ")", "   0    ",
                                "sin(" + alpha + ")", "cos(" + alpha + ")", "    0    ",
                                "    0    ", "    0    ", "    1    "
                            };

                            Vector4 column1 = new Vector4(1f, 0f, 0f, 0f);
                            Vector4 column2 = new Vector4(0f, 1f, 0f, 0f);
                            Vector4 column3 = new Vector4(0f, 0f, 1f, 0f);
                            Vector4 column4 = new Vector4(0f, 0f, 0f, 1f);

                            transMatrix = new Matrix4x4(column1, column2, column3, column4);

                            testTrans = new Matrix(matrix, transMatrix);
                        }

                        break;
                    }
                    case IntMatrix.ElementTypes.Skalierung_X:
                    {
                        //get XYZ from cylinder
                        if (cylinderObject.item.activeSelf
                            && cylinderObject.type == CylinderTransformation.CylinderType.Cylinder_XYZ)
                        {
                            float alpha = cylinderObject.testTrans.skalar;
                            alpha *= 0.25f;

                            String[] matrix =
                            {
                                "" + alpha, "0", "0",
                                "0", "1", "0",
                                "0", "0", "1"
                            };

                            Vector4 column1 = new Vector4(alpha, 0f, 0f, 0f);
                            Vector4 column2 = new Vector4(0f, 1f, 0f, 0f);
                            Vector4 column3 = new Vector4(0f, 0f, 1f, 0f);
                            Vector4 column4 = new Vector4(0f, 0f, 0f, 1f);

                            transMatrix = new Matrix4x4(column1, column2, column3, column4);

                            testTrans = new Matrix(matrix, transMatrix);
                        }
                        else
                        {
                            //else set ? for the value
                            String[] matrix =
                            {
                                "?", "0", "0",
                                "0", "1", "0",
                                "0", "0", "1"
                            };

                            Vector4 column1 = new Vector4(1f, 0f, 0f, 0f);
                            Vector4 column2 = new Vector4(0f, 1f, 0f, 0f);
                            Vector4 column3 = new Vector4(0f, 0f, 1f, 0f);
                            Vector4 column4 = new Vector4(0f, 0f, 0f, 1f);

                            transMatrix = new Matrix4x4(column1, column2, column3, column4);

                            testTrans = new Matrix(matrix, transMatrix);
                        }

                        break;
                    }
                    case IntMatrix.ElementTypes.Skalierung_Y:
                    {
                        //get XYZ from cylinder
                        if (cylinderObject.item.activeSelf &&
                            cylinderObject.type == CylinderTransformation.CylinderType.Cylinder_XYZ)
                        {
                            float alpha = cylinderObject.testTrans.skalar;
                            alpha *= 0.25f;

                            String[] matrix =
                            {
                                "1", "0", "0",
                                "0", "" + alpha, "0",
                                "0", "0", "1"
                            };

                            Vector4 column1 = new Vector4(1f, 0f, 0f, 0f);
                            Vector4 column2 = new Vector4(0f, alpha, 0f, 0f);
                            Vector4 column3 = new Vector4(0f, 0f, 1f, 0f);
                            Vector4 column4 = new Vector4(0f, 0f, 0f, 1f);

                            transMatrix = new Matrix4x4(column1, column2, column3, column4);

                            testTrans = new Matrix(matrix, transMatrix);
                        }
                        else
                        {
                            //else set ? for the value
                            String[] matrix =
                            {
                                "1", "0", "0",
                                "0", "?", "0",
                                "0", "0", "1"
                            };

                            Vector4 column1 = new Vector4(1f, 0f, 0f, 0f);
                            Vector4 column2 = new Vector4(0f, 1f, 0f, 0f);
                            Vector4 column3 = new Vector4(0f, 0f, 1f, 0f);
                            Vector4 column4 = new Vector4(0f, 0f, 0f, 1f);

                            transMatrix = new Matrix4x4(column1, column2, column3, column4);

                            testTrans = new Matrix(matrix, transMatrix);
                        }

                        break;
                    }
                    case IntMatrix.ElementTypes.Skalierung_Z:
                    {
                        //get XYZ from cylinder
                        if (cylinderObject.item.activeSelf &&
                            cylinderObject.type == CylinderTransformation.CylinderType.Cylinder_XYZ)
                        {
                            float alpha = cylinderObject.testTrans.skalar;
                            alpha *= 0.25f;

                            String[] matrix =
                            {
                                "1", "0", "0",
                                "0", "1", "0",
                                "0", "0", "" + alpha
                            };

                            Vector4 column1 = new Vector4(1f, 0f, 0f, 0f);
                            Vector4 column2 = new Vector4(0f, 1f, 0f, 0f);
                            Vector4 column3 = new Vector4(0f, 0f, alpha, 0f);
                            Vector4 column4 = new Vector4(0f, 0f, 0f, 1f);

                            transMatrix = new Matrix4x4(column1, column2, column3, column4);

                            testTrans = new Matrix(matrix, transMatrix);
                        }
                        else
                        {
                            //else set ? for the value
                            String[] matrix =
                            {
                                "1", "0", "0",
                                "0", "1", "0",
                                "0", "0", "?"
                            };

                            Vector4 column1 = new Vector4(1f, 0f, 0f, 0f);
                            Vector4 column2 = new Vector4(0f, 1f, 0f, 0f);
                            Vector4 column3 = new Vector4(0f, 0f, 1f, 0f);
                            Vector4 column4 = new Vector4(0f, 0f, 0f, 1f);

                            transMatrix = new Matrix4x4(column1, column2, column3, column4);

                            testTrans = new Matrix(matrix, transMatrix);
                        }

                        break;
                    }
                }
            }
        }
    }
}
