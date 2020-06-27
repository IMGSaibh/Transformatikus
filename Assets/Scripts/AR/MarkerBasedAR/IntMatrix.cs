using System;

namespace OpenCVMarkerBasedAR
{
    [System.Serializable]
    public class IntMatrix
    {
        public enum ElementTypes // your custom enumeration
        {
            Objekt,
            Pivot,
            Vector, 
            Matrix,
            Operation,
            Vector_X,
            Vector_X_Transponiert,
            Vector_Y,
            Vector_Y_Transponiert,
            Vector_Z,
            Skalierung_X,
            Skalierung_Y,
            Skalierung_Z,
            Rotation_X,
            Rotation_Y,
            Rotation_Z,
            Objekt_Transponiert,
            Pivot_Transponiert,
            Objekt_Finte,
            Pivot_Finte,
            Scherung_X,
            Scherung_Y,
            Scherung_Z,
            Rotation_X_neg,
            Rotation_Y_neg,
            Rotation_Z_neg,
            Vector_X_neg,
            Vector_Y_neg,
            Vector_Z_neg
        };

        public ElementTypes elementType = ElementTypes.Vector_X;  // this public var should appear as a drop down
        
        public int matrixSize = 3;
        public string[] matrixData = new string[3 * 3];
        public string[] vectorData = new string[3];
        
        public String operation;
    }
}