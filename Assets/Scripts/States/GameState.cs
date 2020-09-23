using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using OpenCVMarkerBasedAR;
using UnityEngine;

public class GameState : BaseState
{
    public override void PrepareState()
    {
        base.PrepareState();
    }
    
    public override void UpdateState()
    {
        base.UpdateState();
        Debug.Log("GameState");


        GameStateMachine game = stateMachineOwner.GetComponent<GameStateMachine>();

        if (game.Ipaket.transform.position.Equals(game.Ipaket_target.transform.position))
        {
            game.SceneSwitch.SwitchToScene("Level_2");
        }



        //hier werden die eigentlichen Operationen getriggert
        //wenn genau drei Würfel gelegt wurden (ausgeschlossen ist der Zylinder)
        if (SortedCubesListScript.sortedCubes != null
            && SortedCubesListScript.sortedCubes.Count == 3)
        {
            //Debug.Log("sortedCubes[0]: " + sortedCubes[0].Value.transformationClass.transformationMatrix.operation);
            //Debug.Log("sortedCubes[1]: " + sortedCubes[1].Value.transformationClass.transformationMatrix.operation);
            //Debug.Log("sortedCubes[2]: " + sortedCubes[2].Value.transformationClass.transformationMatrix.operation);

            //wenn der mittlere Würfel ein Operationswürfel ist
            //der mittlere Würfel MUSS immer eine Operation sein
            if (SortedCubesListScript.sortedCubes[1].Value.transformationClass.transformationMatrix.elementType ==
                IntMatrix.ElementTypes.Operation)
            {
                //Quatsch-Operationen (Finten) abfragen
                if (SortedCubesListScript.sortedCubes[1].Value.transformationClass.transformationMatrix.operation == "/"
                    || SortedCubesListScript.sortedCubes[1].Value.transformationClass.transformationMatrix.operation ==
                    "%"
                    || SortedCubesListScript.sortedCubes[1].Value.transformationClass.transformationMatrix.operation ==
                    "&")
                {
                    Debug.Log("Fehler!");
                    return;
                }

                //wenn der erste Würfel ein Objekt oder Pivot ist
                if (SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
                    IntMatrix.ElementTypes.Objekt
                    || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                        .elementType ==
                    IntMatrix.ElementTypes.Pivot)
                {
                    //dann muss der dritte Würfel ein Translations-Würfel sein
                    if (SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                            .elementType ==
                        IntMatrix.ElementTypes.Vector_X
                        || SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                            .elementType ==
                        IntMatrix.ElementTypes.Vector_Y
                        || SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                            .elementType ==
                        IntMatrix.ElementTypes.Vector_Z
                        || SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                            .elementType ==
                        IntMatrix.ElementTypes.Vector_X_neg
                        || SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                            .elementType ==
                        IntMatrix.ElementTypes.Vector_Y_neg
                        || SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                            .elementType ==
                        IntMatrix.ElementTypes.Vector_Z_neg)
                    {

                        if (SortedCubesListScript.sortedCubes[1].Value.transformationClass.transformationMatrix
                                .operation == "+")
                        {
                            if (game.confirmed)
                            {
                                Vector3 transVector = SortedCubesListScript.sortedCubes[2].Value.transformationClass
                                    .transVector;

                                if (SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                                        .elementType == IntMatrix.ElementTypes.Pivot)
                                {
                                    //trigger state machine change
                                    //do a minus translation
                                    game.ScalingObject.pseudoWorldCoordinateSystem.transform.position += transVector;
                                }
                                else
                                {
                                    //Plus-Translation durchführen
                                    //Vector3 translation = sortedCubes[2].Value.transformationClass.testTrans.vector;
                                    //teilpaket.transform.position += sortedCubes[1].Value.transformationClass.transVector;

                                    //trigger state machine change
                                    //do a translation
                                    game.TranslationObject.Add(transVector);
                                }

                                //change to not confirmed
                                game.confirmed = false;

                                //change to wait state
                                stateMachineOwner.ChangeState(new WaitState());
                            }
                        }
                        else if (SortedCubesListScript.sortedCubes[1].Value.transformationClass.transformationMatrix
                                     .operation == "-")
                        {
                            if (game.confirmed)
                            {
                                Vector3 transVector = SortedCubesListScript.sortedCubes[2].Value.transformationClass
                                    .transVector;

                                if (SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                                        .elementType == IntMatrix.ElementTypes.Pivot)
                                {
                                    //trigger state machine change
                                    //do a minus translation
                                    game.ScalingObject.pseudoWorldCoordinateSystem.transform.position -= transVector;
                                }
                                else
                                {
                                    //Minus-Translation durchführen
                                    //Vector3 translation = sortedCubes[2].Value.transformationClass.testTrans.vector;
                                    //teilpaket.transform.position -= sortedCubes[1].Value.transformationClass.transVector;

                                    //trigger state machine change
                                    //do a translation
                                    game.TranslationObject.Sub(transVector);
                                }

                                //change to not confirmed
                                game.confirmed = false;

                                //change to wait state
                                stateMachineOwner.ChangeState(new WaitState());
                            }
                        }
                        else
                        {
                            //*-Operation macht keinen Sinn!
                            Debug.Log("Fehler!");
                        }
                    }
                    //ansonsten Fehler!
                    else
                    {
                        Debug.Log("Fehler!");
                    }
                }
                //wenn der erste Würfel ein Translations-Würfel ist
                else if (SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                             .elementType ==
                         IntMatrix.ElementTypes.Vector_X
                         || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                             .elementType ==
                         IntMatrix.ElementTypes.Vector_Y
                         || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                             .elementType ==
                         IntMatrix.ElementTypes.Vector_Z
                         || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                             .elementType ==
                         IntMatrix.ElementTypes.Vector_X_neg
                         || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                             .elementType ==
                         IntMatrix.ElementTypes.Vector_Y_neg
                         || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                             .elementType ==
                         IntMatrix.ElementTypes.Vector_Z_neg)
                {
                    //dann MUSS der dritte Würfel ein Objekt/Pivot sein
                    if (SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                            .elementType ==
                        IntMatrix.ElementTypes.Objekt
                        || SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                            .elementType ==
                        IntMatrix.ElementTypes.Pivot)
                    {
                        if (SortedCubesListScript.sortedCubes[1].Value.transformationClass.transformationMatrix
                                .operation == "+")
                        {
                            if (game.confirmed)
                            {
                                Vector3 transVector = SortedCubesListScript.sortedCubes[0].Value.transformationClass
                                    .transVector;

                                if (SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                                        .elementType == IntMatrix.ElementTypes.Pivot)
                                {
                                    //trigger state machine change
                                    //do a translation
                                    game.ScalingObject.pseudoWorldCoordinateSystem.transform.position += transVector;
                                }
                                else
                                {
                                    //Plus-Translation durchführen
                                    //Vector3 translation = sortedCubes[0].Value.transformationClass.testTrans.vector;
                                    //teilpaket.transform.position += sortedCubes[0].Value.transformationClass.transVector;

                                    //trigger state machine change
                                    //do a translation
                                    game.TranslationObject.Add(transVector);
                                }

                                //change to not confirmed
                                game.confirmed = false;

                                //change to wait state
                                stateMachineOwner.ChangeState(new WaitState());
                            }
                        }
                        else if (SortedCubesListScript.sortedCubes[1].Value.transformationClass.transformationMatrix
                                     .operation == "-")
                        {
                            if (game.confirmed)
                            {
                                Vector3 transVector = SortedCubesListScript.sortedCubes[0].Value.transformationClass
                                    .transVector;

                                if (SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                                        .elementType == IntMatrix.ElementTypes.Pivot)
                                {
                                    //trigger state machine change
                                    //do a minus translation
                                    game.ScalingObject.pseudoWorldCoordinateSystem.transform.position -= transVector;
                                }
                                else
                                {
                                    //Minus-Translation durchführen
                                    //Vector3 translation = sortedCubes[0].Value.transformationClass.testTrans.vector;
                                    //teilpaket.transform.position -= sortedCubes[0].Value.transformationClass.transVector;

                                    //trigger state machine change
                                    //do a translation
                                    game.TranslationObject.Sub(transVector);
                                }

                                //change to not confirmed
                                game.confirmed = false;

                                //change to wait state
                                stateMachineOwner.ChangeState(new WaitState());
                            }
                        }
                        else
                        {
                            //*-Operation macht keinen Sinn!
                            Debug.Log("Fehler!");
                        }
                    }
                    else
                    {
                        Debug.Log("Fehler");
                    }
                }
                //wenn der erste Würfel eine Rotations-Matrix ist
                else if (SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                             .elementType ==
                         IntMatrix.ElementTypes.Rotation_X
                         || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                             .elementType ==
                         IntMatrix.ElementTypes.Rotation_Y
                         || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                             .elementType ==
                         IntMatrix.ElementTypes.Rotation_Z
                         || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                             .elementType ==
                         IntMatrix.ElementTypes.Rotation_X_neg
                         || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                             .elementType ==
                         IntMatrix.ElementTypes.Rotation_Y_neg
                         || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                             .elementType ==
                         IntMatrix.ElementTypes.Rotation_Z_neg)
                {
                    //dann MUSS der dritte Würfel ein Objekt/Pivot sein
                    if (SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                            .elementType ==
                        IntMatrix.ElementTypes.Objekt
                        || SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                            .elementType ==
                        IntMatrix.ElementTypes.Pivot)
                    {
                        if (SortedCubesListScript.sortedCubes[1].Value.transformationClass.transformationMatrix
                                .operation == "*")
                        {
                            if (game.confirmed)
                            {
                                //alpha und die Rotationsachse holen
                                float alpha = SortedCubesListScript.sortedCubes[0].Value.transformationClass.alpha;
                                Vector3 axis = SortedCubesListScript.sortedCubes[0].Value.transformationClass
                                    .rotationAxisVector;

                                if (SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                                        .elementType == IntMatrix.ElementTypes.Pivot)
                                {
                                    //trigger state machine change
                                    //do a rotation for the coordinate system
                                    game.RotationObject.pseudoWorldCoordinateSystem.transform.Rotate(axis, alpha);
                                }
                                else
                                {
                                    //Rotation ausführen
                                    //teilpaket.transform.RotateAround(pseudoWorldCoordinateSystem.transform.position, axis, alpha);

                                    //trigger state machine change
                                    //do a rotation
                                    game.RotationObject.Rotate(axis, alpha);
                                }

                                //change to not confirmed
                                game.confirmed = false;

                                //change to wait state
                                stateMachineOwner.ChangeState(new WaitState());
                            }
                        }
                        else
                        {
                            //+ und - Operation macht keinen Sinn!
                            Debug.Log("Fehler!");
                        }
                    }
                    else
                    {
                        Debug.Log("Fehler");
                    }
                }
                //wenn der erste Würfel eine Skalierungs-Matrix ist
                else if (SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                             .elementType ==
                         IntMatrix.ElementTypes.Skalierung_X
                         || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                             .elementType ==
                         IntMatrix.ElementTypes.Skalierung_Y
                         || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                             .elementType ==
                         IntMatrix.ElementTypes.Skalierung_Z)
                {
                    //dann MUSS der dritte Würfel ein Objekt/Pivot sein
                    if (SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                            .elementType ==
                        IntMatrix.ElementTypes.Objekt
                        || SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                            .elementType ==
                        IntMatrix.ElementTypes.Pivot)
                    {
                        if (SortedCubesListScript.sortedCubes[1].Value.transformationClass.transformationMatrix
                                .operation == "*")
                        {
                            if (game.confirmed)
                            {
                                //Skalierungsvektor holen
                                Vector3 scale = SortedCubesListScript.sortedCubes[0].Value.transformationClass
                                    .scaleVector;

                                if (SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                                        .elementType == IntMatrix.ElementTypes.Pivot)
                                {
                                    //trigger state machine change
                                    //do a scaling for the coordinate system
                                    game.ScalingObject.pseudoWorldCoordinateSystem.transform.localScale = scale;
                                }
                                else
                                {
                                    //trigger state machine change
                                    //do a scaling
                                    game.ScalingObject.ScaleAround(scale);
                                    //stateMachineOwner.ScalingObject.Scale(scale);
                                }

                                //change to not confirmed
                                game.confirmed = false;

                                //change to wait state
                                stateMachineOwner.ChangeState(new WaitState());
                            }
                        }
                        else
                        {
                            //+ und - Operation macht keinen Sinn!
                            Debug.Log("Fehler!");
                        }
                    }
                    else
                    {
                        Debug.Log("Fehler");
                    }
                }
                //wenn der erste Würfel ein transponierter Objekt/Pivot ist
                //TODO: Transponierte Vektoren * Matrix
                else if (SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                             .elementType == IntMatrix.ElementTypes.Pivot_Transponiert
                         || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                             .elementType == IntMatrix.ElementTypes.Objekt_Transponiert)
                {
                    //dann muss der dritte Würfel eine Matrix (entweder Skalierung oder Rotation) sein
                    if (SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                            .elementType == IntMatrix.ElementTypes.Skalierung_X
                        || SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                            .elementType == IntMatrix.ElementTypes.Skalierung_Y
                        || SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                            .elementType == IntMatrix.ElementTypes.Skalierung_Z)
                    {
                        if (SortedCubesListScript.sortedCubes[1].Value.transformationClass.transformationMatrix
                                .operation == "*")
                        {
                            if (game.confirmed)
                            {
                                //Skalierungsvektor holen
                                Vector3 scale = SortedCubesListScript.sortedCubes[2].Value.transformationClass
                                    .scaleVector;

                                if (SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                                        .elementType == IntMatrix.ElementTypes.Pivot_Transponiert)
                                {
                                    //trigger state machine change
                                    //do a scaling for the coordinate system
                                    game.ScalingObject.pseudoWorldCoordinateSystem.transform.localScale = scale;
                                }
                                else
                                {
                                    //trigger state machine change
                                    //do a scaling
                                    game.ScalingObject.ScaleAround(scale);
                                    //stateMachineOwner.ScalingObject.Scale(scale);
                                }

                                //change to not confirmed
                                game.confirmed = false;

                                //change to wait state
                                stateMachineOwner.ChangeState(new WaitState());
                            }
                        }
                        else
                        {
                            //+ und - Operation macht keinen Sinn!
                            Debug.Log("Fehler!");
                        }
                    }
                    else if (SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                                 .elementType == IntMatrix.ElementTypes.Rotation_X
                             || SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                                 .elementType == IntMatrix.ElementTypes.Rotation_Y
                             || SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                                 .elementType == IntMatrix.ElementTypes.Rotation_Z
                             || SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                                 .elementType == IntMatrix.ElementTypes.Rotation_X_neg
                             || SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                                 .elementType == IntMatrix.ElementTypes.Rotation_Y_neg
                             || SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix
                                 .elementType == IntMatrix.ElementTypes.Rotation_Z_neg)
                    {
                        if (SortedCubesListScript.sortedCubes[1].Value.transformationClass.transformationMatrix
                                .operation == "*")
                        {
                            if (game.confirmed)
                            {
                                //alpha und die Rotationsachse holen
                                float alpha = SortedCubesListScript.sortedCubes[2].Value.transformationClass.alpha;
                                Vector3 axis = SortedCubesListScript.sortedCubes[2].Value.transformationClass
                                    .rotationAxisVector;

                                if (SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix
                                        .elementType == IntMatrix.ElementTypes.Pivot_Transponiert)
                                {
                                    //trigger state machine change
                                    //do a scaling for the coordinate system
                                    game.RotationObject.pseudoWorldCoordinateSystem.transform.Rotate(axis, alpha);
                                }
                                else
                                {
                                    //Rotation ausführen
                                    //teilpaket.transform.RotateAround(pseudoWorldCoordinateSystem.transform.position, axis, alpha);

                                    //trigger state machine change
                                    //do a rotation
                                    game.RotationObject.Rotate(axis, alpha);
                                }

                                //change to not confirmed
                                game.confirmed = false;

                                //change to wait state
                                stateMachineOwner.ChangeState(new WaitState());
                            }
                        }
                        else
                        {
                            //+ und - Operation macht keinen Sinn!
                            Debug.Log("Fehler!");
                        }
                    }
                }
            }
            else
            {
                //                for(int k = 0; k < SortedCubesListScript.sortedCubes.Count; k++)
                //                    Debug.Log(SortedCubesListScript.sortedCubes[k].Value.GetCubeName());
                Debug.Log("Fehler!");
            }
        }
        else
        {
            Debug.Log("Nicht genau 3 Würfel gelegt!");
        }
    }

}
