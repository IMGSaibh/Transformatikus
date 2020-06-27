using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using OpenCVMarkerBasedAR;
using UnityEngine;

public class WaitState : BaseState
{
    // we can use this as level transition
    public override void PrepareState()
    {
        base.PrepareState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Debug.Log("Wait State");
        
        //When cube is placed on table do somthing
        if (Input.GetKeyDown(KeyCode.M))
            stateMachineOwner.ChangeState(new MoveState());
        if (Input.GetKeyDown(KeyCode.S))
            stateMachineOwner.ChangeState(new LoadingSceneState());
        
        //hier werden die eigentlichen Operationen getriggert
        //wenn genau drei Würfel gelegt wurden (ausgeschlossen ist der Zylinder)
        if (SortedCubesListScript.sortedCubes != null && SortedCubesListScript.sortedCubes.Count == 3)
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
                    || SortedCubesListScript.sortedCubes[1].Value.transformationClass.transformationMatrix.operation == "%"
                    || SortedCubesListScript.sortedCubes[1].Value.transformationClass.transformationMatrix.operation == "&")
                {
                    Debug.Log("Fehler!");
                }

                //wenn der erste Würfel ein Objekt oder Pivot ist
                if (SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
                    IntMatrix.ElementTypes.Objekt
                    || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
                    IntMatrix.ElementTypes.Pivot)
                {
                    //dann muss der dritte Würfel ein Translations-Würfel sein
                    if (SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix.elementType ==
                        IntMatrix.ElementTypes.Vector_X
                        || SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix.elementType ==
                        IntMatrix.ElementTypes.Vector_Y
                        || SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix.elementType ==
                        IntMatrix.ElementTypes.Vector_Z
                        )
                    {
                        if (SortedCubesListScript.sortedCubes[1].Value.transformationClass.transformationMatrix.operation == "+")
                        {
                            if (stateMachineOwner.confirmed)
                            {
                                //Plus-Translation durchführen
                                //Vector3 translation = sortedCubes[2].Value.transformationClass.testTrans.vector;
                                //teilpaket.transform.position += sortedCubes[1].Value.transformationClass.transVector;

                                //trigger state machine change
                                //do a translation
                                stateMachineOwner.TranslationObject.Add(SortedCubesListScript.sortedCubes[1].Value.transformationClass.transVector);

                                //change to not confirmed
                                stateMachineOwner.confirmed = false;
                                
                                //change to wait state
                                stateMachineOwner.ChangeState(new WaitState());
                            }
                        }
                        else if (SortedCubesListScript.sortedCubes[1].Value.transformationClass.transformationMatrix.operation == "-")
                        {
                            if (stateMachineOwner.confirmed)
                            {
                                //Minus-Translation durchführen
                                //Vector3 translation = sortedCubes[2].Value.transformationClass.testTrans.vector;
                                //teilpaket.transform.position -= sortedCubes[1].Value.transformationClass.transVector;

                                //trigger state machine change
                                //do a translation
                                stateMachineOwner.TranslationObject.Sub(SortedCubesListScript.sortedCubes[1].Value.transformationClass
                                    .transVector);

                                //change to not confirmed
                                stateMachineOwner.confirmed = false;
                                
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
                else if (SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
                         IntMatrix.ElementTypes.Vector_X
                         || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
                         IntMatrix.ElementTypes.Vector_Y
                         || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
                         IntMatrix.ElementTypes.Vector_Z
                )
                {
                    //dann MUSS der dritte Würfel ein Objekt/Pivot sein
                    if (SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix.elementType ==
                        IntMatrix.ElementTypes.Objekt
                        || SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix.elementType ==
                        IntMatrix.ElementTypes.Pivot)
                    {
                        if (SortedCubesListScript.sortedCubes[1].Value.transformationClass.transformationMatrix.operation == "+")
                        {
                            if (stateMachineOwner.confirmed)
                            {
                                //Plus-Translation durchführen
                                //Vector3 translation = sortedCubes[0].Value.transformationClass.testTrans.vector;
                                //teilpaket.transform.position += sortedCubes[0].Value.transformationClass.transVector;

                                //trigger state machine change
                                //do a translation
                                stateMachineOwner.TranslationObject.Add(SortedCubesListScript.sortedCubes[0].Value.transformationClass
                                    .transVector);
                                
                                //change to not confirmed
                                stateMachineOwner.confirmed = false;

                                //change to wait state
                                stateMachineOwner.ChangeState(new WaitState());
                            }
                        }
                        else if (SortedCubesListScript.sortedCubes[1].Value.transformationClass.transformationMatrix.operation == "-")
                        {
                            if (stateMachineOwner.confirmed)
                            {
                                //Minus-Translation durchführen
                                //Vector3 translation = sortedCubes[0].Value.transformationClass.testTrans.vector;
                                //teilpaket.transform.position -= sortedCubes[0].Value.transformationClass.transVector;

                                //trigger state machine change
                                //do a translation
                                stateMachineOwner.TranslationObject.Sub(SortedCubesListScript.sortedCubes[0].Value.transformationClass
                                    .transVector);
                                
                                //change to not confirmed
                                stateMachineOwner.confirmed = false;

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
                else if (SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
                    IntMatrix.ElementTypes.Rotation_X
                    || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
                    IntMatrix.ElementTypes.Rotation_Y
                    || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
                    IntMatrix.ElementTypes.Rotation_Z
                    || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
                    IntMatrix.ElementTypes.Rotation_X_neg
                    || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
                    IntMatrix.ElementTypes.Rotation_Y_neg
                    || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
                    IntMatrix.ElementTypes.Rotation_Z_neg)
                {
                    //dann MUSS der dritte Würfel ein Objekt/Pivot sein
                    if (SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix.elementType ==
                        IntMatrix.ElementTypes.Objekt
                        || SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix.elementType ==
                        IntMatrix.ElementTypes.Pivot)
                    {
                        if (SortedCubesListScript.sortedCubes[1].Value.transformationClass.transformationMatrix.operation == "*")
                        {
                            if (stateMachineOwner.confirmed)
                            {
                                //alpha und die Rotationsachse holen
                                float alpha = SortedCubesListScript.sortedCubes[0].Value.transformationClass.alpha;
                                Vector3 axis = SortedCubesListScript.sortedCubes[0].Value.transformationClass.rotationAxisVector;

                                //Rotation ausführen
                                //teilpaket.transform.RotateAround(pseudoWorldCoordinateSystem.transform.position, axis, alpha);

                                //trigger state machine change
                                //do a rotation
                                stateMachineOwner.RotationObject.Rotate(axis, alpha);
                                
                                //change to not confirmed
                                stateMachineOwner.confirmed = false;

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
                else if (SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
                         IntMatrix.ElementTypes.Skalierung_X
                         || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
                         IntMatrix.ElementTypes.Skalierung_Y
                         || SortedCubesListScript.sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
                         IntMatrix.ElementTypes.Skalierung_Z)
                {
                    //dann MUSS der dritte Würfel ein Objekt/Pivot sein
                    if (SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix.elementType ==
                        IntMatrix.ElementTypes.Objekt
                        || SortedCubesListScript.sortedCubes[2].Value.transformationClass.transformationMatrix.elementType ==
                        IntMatrix.ElementTypes.Pivot)
                    {
                        if (SortedCubesListScript.sortedCubes[1].Value.transformationClass.transformationMatrix.operation == "*")
                        {
                            if (stateMachineOwner.confirmed)
                            {
                                //Skalierungsvektor holen
                                Vector3 scale = SortedCubesListScript.sortedCubes[0].Value.transformationClass.scaleVector;

                                //trigger state machine change
                                //do a scaling
                                stateMachineOwner.ScalingObject.ScaleAround(scale);
                                
                                //change to not confirmed
                                stateMachineOwner.confirmed = false;

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
                //TODO: Transponierte Vektoren * Matrix sind möglich, aber noch nicht reingenommen!
            }
            else
            {
                Debug.Log("Fehler!");
            }
        }
        else
        {
            Debug.Log("Nicht genau 3 Würfel gelegt!");
        }

    }
}
