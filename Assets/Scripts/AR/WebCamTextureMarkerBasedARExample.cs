using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using DefaultNamespace;
using MarkerBasedARExample.MarkerBasedAR;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnity.Calib3dModule;
using OpenCVForUnity.UnityUtils.Helper;
using OpenCVMarkerBasedAR;
using UnityEngine.UI;

namespace MarkerBasedARExample
{
    /// <summary>
    /// WebcamTexture Marker Based AR Example
    /// This code is a rewrite of https://github.com/MasteringOpenCV/code/tree/master/Chapter2_iPhoneAR using "OpenCV for Unity".
    /// </summary>
    [RequireComponent (typeof(WebCamTextureToMatHelper))]
    public class WebCamTextureMarkerBasedARExample : MonoBehaviour
    {
        /// <summary>
        /// Teilpaket Gameobject.
        /// </summary>
        public GameObject teilpaket;

        /// <summary>
        /// state machine of the teilpaket.
        /// </summary>
        public StateMachine stateMachine;
        
        /// <summary>
        /// Pseudo-Koordinatensytem Gameobject.
        /// </summary>
        public GameObject pseudoWorldCoordinateSystem;
        
        /// <summary>
        /// The AR camera.
        /// </summary>
        public Camera ARCamera;

        /// <summary>
        /// The marker settings Two.
        /// </summary>
        private List<MarkerSettings> markerSettings2;
        
        /// <summary>
        /// The cubes.
        /// </summary>
        public GameObject[] cubes;
        
        /// <summary>
        /// The reihenfolge text.
        /// </summary>
        public Text reihenfolgeText;
        
        /// <summary>
        /// The reihenfolge text with transformations.
        /// </summary>
        public Text reihenfolgeTextTransformations;
        
        /// <summary>
        /// The first matrix text.
        /// </summary>
        public Text firstMatrixText;
        
        /// <summary>
        /// The second matrix text.
        /// </summary>
        public Text secondMatrixText;
        
        /// <summary>
        /// The third matrix text.
        /// </summary>
        public Text thirdMatrixText;
        
        /// <summary>
        /// Determines if should move AR camera.
        /// </summary>
        [Tooltip ("If true, only the first element of markerSettings will be processed.")]
        public bool shouldMoveARCamera;

        /// <summary>
        /// The texture.
        /// </summary>
        Texture2D texture;

        /// <summary>
        /// The cameraparam matrix.
        /// </summary>
        Mat camMatrix;

        /// <summary>
        /// The dist coeffs.
        /// </summary>
        MatOfDouble distCoeffs;

        /// <summary>
        /// The marker detector.
        /// </summary>
        MarkerDetector markerDetector;

        /// <summary>
        /// The matrix that inverts the Y axis.
        /// </summary>
        Matrix4x4 invertYM;

        /// <summary>
        /// The matrix that inverts the Z axis.
        /// </summary>
        Matrix4x4 invertZM;
        
        /// <summary>
        /// The transformation matrix.
        /// </summary>
        Matrix4x4 transformationM;

        /// <summary>
        /// The transformation matrix for AR.
        /// </summary>
        Matrix4x4 ARM;

        /// <summary>
        /// The webcam texture to mat helper.
        /// </summary>
        WebCamTextureToMatHelper webCamTextureToMatHelper;
        
        /// <summary>
        /// Dictionary for Cube name and the "real" AR world postition
        /// </summary>
        Dictionary<String, Vector3> cubeRealLocation;
        
        /// <summary>
        /// Dictionary for found cubes with the class FoundCube
        /// </summary>
        private Dictionary<String,FoundCube> foundCubes;

        /// <summary>
        /// Sorted list for found cubes with the class FoundCube
        /// </summary>
        //public List<KeyValuePair<string, FoundCube>> sortedCubes;

        // Use this for initialization
        void Start ()
        {
            markerSettings2 = new List<MarkerSettings>();
            cubeRealLocation = new Dictionary<String, Vector3>();
            foundCubes = new Dictionary<String, FoundCube>();
            //sortedCubes = new List<KeyValuePair<string, FoundCube>>();
            SortedCubesListScript.sortedCubes = new List<KeyValuePair<string, FoundCube>>();

            webCamTextureToMatHelper = gameObject.GetComponent<WebCamTextureToMatHelper> ();

            //get the sides for every cube
            foreach (var cube in cubes)
            {
                //MarkerSettings[] markerSidesFromCube = cube.GetComponentsInChildren<MarkerSettings>();
                markerSettings2.AddRange(cube.GetComponentsInChildren<MarkerSettings>());
                //Debug.Log(markerSettings2[0]);
            }

            #if UNITY_ANDROID && !UNITY_EDITOR
            // Avoids the front camera low light issue that occurs in only some Android devices (e.g. Google Pixel, Pixel2).
            webCamTextureToMatHelper.avoidAndroidFrontCameraLowLightIssue = true;
            #endif
            webCamTextureToMatHelper.Initialize ();
            
//            //add button listener
//            button.onClick.AddListener(delegate()
//            {
//                this.ButtonClicked();
//            });
        }
        
//        public void ButtonClicked () {
//            this.confirmed = !this.confirmed;
//        }

        /// <summary>
        /// Raises the web cam texture to mat helper initialized event.
        /// </summary>
        public void OnWebCamTextureToMatHelperInitialized ()
        {
            Debug.Log ("OnWebCamTextureToMatHelperInitialized");
            
            Mat webCamTextureMat = webCamTextureToMatHelper.GetMat ();

            texture = new Texture2D (webCamTextureMat.cols (), webCamTextureMat.rows (), TextureFormat.RGBA32, false);
            gameObject.GetComponent<Renderer> ().material.mainTexture = texture;

            gameObject.transform.localScale = new Vector3 (webCamTextureMat.cols (), webCamTextureMat.rows (), 1);
            
            Debug.Log ("Screen.width " + Screen.width + " Screen.height " + Screen.height + " Screen.orientation " + Screen.orientation);


            float width = webCamTextureMat.width ();
            float height = webCamTextureMat.height ();
            
            float imageSizeScale = 1.0f;
            float widthScale = (float)Screen.width / width;
            float heightScale = (float)Screen.height / height;
            if (widthScale < heightScale) {
                Camera.main.orthographicSize = (width * (float)Screen.height / (float)Screen.width) / 2;
                imageSizeScale = (float)Screen.height / (float)Screen.width;
            } else {
                Camera.main.orthographicSize = height / 2;
            }

            
            //set cameraparam
            int max_d = (int)Mathf.Max (width, height);
            double fx = max_d;
            double fy = max_d;
            double cx = width / 2.0f;
            double cy = height / 2.0f;
            camMatrix = new Mat (3, 3, CvType.CV_64FC1);
            camMatrix.put (0, 0, fx);
            camMatrix.put (0, 1, 0);
            camMatrix.put (0, 2, cx);
            camMatrix.put (1, 0, 0);
            camMatrix.put (1, 1, fy);
            camMatrix.put (1, 2, cy);
            camMatrix.put (2, 0, 0);
            camMatrix.put (2, 1, 0);
            camMatrix.put (2, 2, 1.0f);
            Debug.Log ("camMatrix " + camMatrix.dump ());
            
            distCoeffs = new MatOfDouble (0, 0, 0, 0);
            Debug.Log ("distCoeffs " + distCoeffs.dump ());
            
            //calibration camera
            Size imageSize = new Size (width * imageSizeScale, height * imageSizeScale);
            double apertureWidth = 0;
            double apertureHeight = 0;
            double[] fovx = new double[1];
            double[] fovy = new double[1];
            double[] focalLength = new double[1];
            Point principalPoint = new Point (0, 0);
            double[] aspectratio = new double[1];
            
            
            Calib3d.calibrationMatrixValues (camMatrix, imageSize, apertureWidth, apertureHeight, fovx, fovy, focalLength, principalPoint, aspectratio);
            
            Debug.Log ("imageSize " + imageSize.ToString ());
            Debug.Log ("apertureWidth " + apertureWidth);
            Debug.Log ("apertureHeight " + apertureHeight);
            Debug.Log ("fovx " + fovx [0]);
            Debug.Log ("fovy " + fovy [0]);
            Debug.Log ("focalLength " + focalLength [0]);
            Debug.Log ("principalPoint " + principalPoint.ToString ());
            Debug.Log ("aspectratio " + aspectratio [0]);


            //To convert the difference of the FOV value of the OpenCV and Unity. 
            double fovXScale = (2.0 * Mathf.Atan ((float)(imageSize.width / (2.0 * fx)))) / (Mathf.Atan2 ((float)cx, (float)fx) + Mathf.Atan2 ((float)(imageSize.width - cx), (float)fx));
            double fovYScale = (2.0 * Mathf.Atan ((float)(imageSize.height / (2.0 * fy)))) / (Mathf.Atan2 ((float)cy, (float)fy) + Mathf.Atan2 ((float)(imageSize.height - cy), (float)fy));
            
            Debug.Log ("fovXScale " + fovXScale);
            Debug.Log ("fovYScale " + fovYScale);
            
            
            //Adjust Unity Camera FOV https://github.com/opencv/opencv/commit/8ed1945ccd52501f5ab22bdec6aa1f91f1e2cfd4
            if (widthScale < heightScale) {
                ARCamera.fieldOfView = (float)(fovx [0] * fovXScale);
            } else {
                ARCamera.fieldOfView = (float)(fovy [0] * fovYScale);
            }

            
            //MarkerDesign[] markerDesigns = new MarkerDesign[markerSettings.Length];
            MarkerDesign[] markerDesigns = new MarkerDesign[markerSettings2.Count];
            
            for (int i = 0; i < markerDesigns.Length; i++) {
                //markerDesigns [i] = markerSettings [i].markerDesign;
                markerDesigns [i] = markerSettings2[i].markerDesign;
            }

            markerDetector = new MarkerDetector (camMatrix, distCoeffs, markerDesigns);



            invertYM = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (1, -1, 1));
            Debug.Log ("invertYM " + invertYM.ToString ());
            
            invertZM = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (1, 1, -1));
            Debug.Log ("invertZM " + invertZM.ToString ());


            //if WebCamera is frontFaceing,flip Mat.
            webCamTextureToMatHelper.flipHorizontal = webCamTextureToMatHelper.GetWebCamDevice().isFrontFacing;
        }

        /// <summary>
        /// Raises the web cam texture to mat helper disposed event.
        /// </summary>
        public void OnWebCamTextureToMatHelperDisposed ()
        {
            Debug.Log ("OnWebCamTextureToMatHelperDisposed");
        }

        /// <summary>
        /// Raises the web cam texture to mat helper error occurred event.
        /// </summary>
        /// <param name="errorCode">Error code.</param>
        public void OnWebCamTextureToMatHelperErrorOccurred (WebCamTextureToMatHelper.ErrorCode errorCode)
        {
            Debug.Log ("OnWebCamTextureToMatHelperErrorOccurred " + errorCode);
        }

        // Update is called once per frame
        void Update ()
        {
            if (webCamTextureToMatHelper.IsPlaying () && webCamTextureToMatHelper.DidUpdateThisFrame ()) {
                
                Mat rgbaMat = webCamTextureToMatHelper.GetMat ();

                markerDetector.processFrame (rgbaMat, 1);


                //foreach (MarkerSettings settings in markerSettings) {
                foreach (MarkerSettings settings in markerSettings2) {
                    settings.setAllARGameObjectsDisable ();
                }
                
                if (shouldMoveARCamera) {
                    List<Marker> findMarkers = markerDetector.getFindMarkers ();
                    if (findMarkers.Count > 0) {

                        Marker marker = findMarkers [0];

                        //if (markerSettings.Length > 0) {
                        if (markerSettings2.Count > 0) {
                            //MarkerSettings settings = markerSettings [0];
                            MarkerSettings settings = markerSettings2[0];
                    
                            if (marker.id == settings.getMarkerId ()) {
                                transformationM = marker.transformation;
//                                                      Debug.Log ("transformationM " + transformationM.ToString ());

                                GameObject ARGameObject = settings.getARGameObject ();
                                if (ARGameObject != null) {
                                    ARM = ARGameObject.transform.localToWorldMatrix * invertZM * transformationM.inverse * invertYM;
                                    //Debug.Log ("arM " + arM.ToString ());
                                    ARGameObject.SetActive (true);
                                    ARUtils.SetTransformFromMatrix (ARCamera.transform, ref ARM);
                                }
                            }
                        }
                    }
                } else {
                    cubeRealLocation.Clear();
                    foundCubes.Clear();

                    List<Marker> findMarkers = markerDetector.getFindMarkers ();
                    
                    for (int i = 0; i < findMarkers.Count; i++) {
                        Marker marker = findMarkers[i];
                    
                        //foreach (MarkerSettings settings in markerSettings) {
                        foreach (MarkerSettings settings in markerSettings2) {
                            if (marker.id == settings.getMarkerId ()) {
                                transformationM = marker.transformation;
//                              Debug.Log ("transformationM " + transformationM.ToString ());

                                ARM = ARCamera.transform.localToWorldMatrix * invertYM * transformationM * invertZM;
                                //Debug.Log ("arM " + arM.ToString ());

                                GameObject ARGameObject = settings.getARGameObject ();
                                
                                if (ARGameObject != null) {
                                    ARUtils.SetTransformFromMatrix (ARGameObject.transform, ref ARM);
                                    ARGameObject.SetActive (true);
                                    
                                    //add the found cube to the list
                                    if (!settings.shouldNotBeAddedToFoundCubes)
                                    {
                                        //get transformation debug script
                                        TransformationDebug test = ARGameObject.transform.parent
                                            .GetComponent<TransformationDebug>();

                                        //get name from cube and position
                                        String cubeName = ARGameObject.transform.root.name;
                                        Vector3 cubePosition = ARGameObject.transform.position;
                                        //Debug.Log("Name vom Cube: "+cubeName);
                                        //Debug.Log("ARGameObject.transform.position: "+cubePosition);

                                        FoundCube foundCube = new FoundCube(cubeName, test, cubePosition);

                                        if (foundCubes.ContainsKey(cubeName))
                                            foundCubes[cubeName] = foundCube;
                                        else
                                            foundCubes.Add(cubeName, foundCube);
                                    }
                                }
                            }
                        }
                    }
                }

                Utils.fastMatToTexture2D (rgbaMat, texture);

                firstMatrixText.text = "";
                secondMatrixText.text = "";
                thirdMatrixText.text = "";

                //REIHENFOLGE DER WÜRFEL:
                //Sort list nach X-Werte für Reihenfolge
                //List<KeyValuePair<string, FoundCube>> myList2 = foundCubes.ToList();
                //sortedCubes = foundCubes.ToList();
                SortedCubesListScript.sortedCubes = foundCubes.ToList();
                //sortedCubes.Sort((pair1,pair2) => pair1.Value.cubePosition.x.CompareTo(pair2.Value.cubePosition.x));
                SortedCubesListScript.sortedCubes.Sort((pair1,pair2) => pair1.Value.cubePosition.x.CompareTo(pair2.Value.cubePosition.x));

                StringBuilder reihenfolge = new StringBuilder();
                StringBuilder reihenfolge2 = new StringBuilder();

                //if (cubeRealLocation.Count > 1)
                //if (sortedCubes.Count > 1)
                if (SortedCubesListScript.sortedCubes.Count > 1)
                {
//                    for (int i = (myList.Count - 1); i >= 0; i--)
//                    {
//                        reihenfolge.Append(myList[i].Key);
//                        reihenfolge.Append(", ");
//                        //Debug.Log(myList[i].Key);
//                    }
                    
                    //for (int i = 0; i < sortedCubes.Count; i++)
                    for (int i = 0; i < SortedCubesListScript.sortedCubes.Count; i++)
                    {
                        //Debug.Log(myList2[i].Key +": " +myList2[i].Value.transformationClass.matrixTransformation);
                        //reihenfolge2.Append(myList2[i].Value.transformationClass.testTrans.GetTransformation());
                        //reihenfolge2.Append(sortedCubes[i].Value.transformationClass.testTrans.GetTransformation());
                        reihenfolge2.Append(SortedCubesListScript.sortedCubes[i].Value.transformationClass.testTrans.GetTransformation());
                        reihenfolge2.Append("      ");

                        if (i == 0)
                        {
                            //firstMatrixText.text = sortedCubes[i].Value.transformationClass.testTrans.GetTransformation();
                            firstMatrixText.text = SortedCubesListScript.sortedCubes[i].Value.transformationClass.testTrans.GetTransformation();
                        }
                        else if (i == 1)
                        {
                            //secondMatrixText.text = sortedCubes[i].Value.transformationClass.testTrans.GetTransformation();
                            secondMatrixText.text = SortedCubesListScript.sortedCubes[i].Value.transformationClass.testTrans.GetTransformation();
                        }
                        else
                        {
                            //thirdMatrixText.text = sortedCubes[i].Value.transformationClass.testTrans.GetTransformation();
                            thirdMatrixText.text = SortedCubesListScript.sortedCubes[i].Value.transformationClass.testTrans.GetTransformation();
                        }

                        //reihenfolge.Append(sortedCubes[i].Key);
                        reihenfolge.Append(SortedCubesListScript.sortedCubes[i].Key);
                        reihenfolge.Append(", ");
                    }
                    
                    reihenfolgeText.text = reihenfolge.ToString();
                    reihenfolgeTextTransformations.text = reihenfolge2.ToString();
                }
                //else if (cubeRealLocation.Count == 1)
                //else if (sortedCubes.Count == 1)
                else if (SortedCubesListScript.sortedCubes.Count == 1)
                {
                    //reihenfolgeText.text = sortedCubes[0].Key;
                    reihenfolgeText.text = SortedCubesListScript.sortedCubes[0].Key;
                    //reihenfolgeTextTransformations.text = sortedCubes[0].Value.transformationClass.testTrans.GetTransformation();
                    reihenfolgeTextTransformations.text = SortedCubesListScript.sortedCubes[0].Value.transformationClass.testTrans.GetTransformation();
                    
                    //firstMatrixText.text = sortedCubes[0].Value.transformationClass.testTrans.GetTransformation();
                    firstMatrixText.text = SortedCubesListScript.sortedCubes[0].Value.transformationClass.testTrans.GetTransformation();
                    secondMatrixText.text = "";
                    thirdMatrixText.text = "";
                }
                else
                {
                    reihenfolgeText.text = "";
                    reihenfolgeTextTransformations.text = "";
                    firstMatrixText.text = "";
                    secondMatrixText.text = "";
                    thirdMatrixText.text = "";
                }

//                //hier werden die Operationen getriggert
//                
//                //wenn genau drei Würfel gelegt wurden (ausgeschlossen ist der Zylinder)
//                if (sortedCubes != null && sortedCubes.Count == 3)
//                {
//                    //Debug.Log("sortedCubes[0]: " + sortedCubes[0].Value.transformationClass.transformationMatrix.operation);
//                    //Debug.Log("sortedCubes[1]: " + sortedCubes[1].Value.transformationClass.transformationMatrix.operation);
//                    //Debug.Log("sortedCubes[2]: " + sortedCubes[2].Value.transformationClass.transformationMatrix.operation);
//
//                    //wenn der mittlere Würfel ein Operationswürfel ist
//                    //der mittlere Würfel MUSS immer eine Operation sein
//                    if (sortedCubes[1].Value.transformationClass.transformationMatrix.elementType ==
//                        IntMatrix.ElementTypes.Operation)
//                    {
//                        //Quatsch-Operationen (Finten) abfragen
//                        if (sortedCubes[1].Value.transformationClass.transformationMatrix.operation == "/"
//                            || sortedCubes[1].Value.transformationClass.transformationMatrix.operation == "%"
//                            || sortedCubes[1].Value.transformationClass.transformationMatrix.operation == "&")
//                        {
//                            Debug.Log("Fehler!");
//                        }
//
//                        //wenn der erste Würfel ein Objekt oder Pivot ist
//                        if (sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
//                            IntMatrix.ElementTypes.Objekt
//                            || sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
//                            IntMatrix.ElementTypes.Pivot)
//                        {
//                            //dann muss der dritte Würfel ein Translations-Würfel sein
//                            if (sortedCubes[2].Value.transformationClass.transformationMatrix.elementType ==
//                                IntMatrix.ElementTypes.Vector_X
//                                || sortedCubes[2].Value.transformationClass.transformationMatrix.elementType ==
//                                IntMatrix.ElementTypes.Vector_Y
//                                || sortedCubes[2].Value.transformationClass.transformationMatrix.elementType ==
//                                IntMatrix.ElementTypes.Vector_Z
//                                )
//                            {
//                                if (sortedCubes[1].Value.transformationClass.transformationMatrix.operation == "+")
//                                {
//                                    if (confirmed)
//                                    {
//                                        //Plus-Translation durchführen
//                                        //Vector3 translation = sortedCubes[2].Value.transformationClass.testTrans.vector;
//                                        //teilpaket.transform.position += sortedCubes[1].Value.transformationClass.transVector;
//
//                                        //trigger state machine change
//                                        //do a translation
//                                        stateMachine.TranslationObject.Add(sortedCubes[1].Value.transformationClass.transVector);
//
//                                        //change to wait state
//                                        stateMachine.ChangeState(new WaitState());
//                                    }
//                                }
//                                else if (sortedCubes[1].Value.transformationClass.transformationMatrix.operation == "-")
//                                {
//                                    if (confirmed)
//                                    {
//                                        //Minus-Translation durchführen
//                                        //Vector3 translation = sortedCubes[2].Value.transformationClass.testTrans.vector;
//                                        //teilpaket.transform.position -= sortedCubes[1].Value.transformationClass.transVector;
//
//                                        //trigger state machine change
//                                        //do a translation
//                                        stateMachine.TranslationObject.Sub(sortedCubes[1].Value.transformationClass
//                                            .transVector);
//
//                                        //change to wait state
//                                        stateMachine.ChangeState(new WaitState());
//                                    }
//                                }
//                                else
//                                {
//                                    //*-Operation macht keinen Sinn!
//                                    Debug.Log("Fehler!");
//                                }
//                            }
//                            //ansonsten Fehler!
//                            else
//                            {
//                                Debug.Log("Fehler!");
//                            }
//                        }
//                        //wenn der erste Würfel ein Translations-Würfel ist
//                        else if (sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
//                                 IntMatrix.ElementTypes.Vector_X
//                                 || sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
//                                 IntMatrix.ElementTypes.Vector_Y
//                                 || sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
//                                 IntMatrix.ElementTypes.Vector_Z
//                        )
//                        {
//                            //dann MUSS der dritte Würfel ein Objekt/Pivot sein
//                            if (sortedCubes[2].Value.transformationClass.transformationMatrix.elementType ==
//                                IntMatrix.ElementTypes.Objekt
//                                || sortedCubes[2].Value.transformationClass.transformationMatrix.elementType ==
//                                IntMatrix.ElementTypes.Pivot)
//                            {
//                                if (sortedCubes[1].Value.transformationClass.transformationMatrix.operation == "+")
//                                {
//                                    if (confirmed)
//                                    {
//                                        //Plus-Translation durchführen
//                                        //Vector3 translation = sortedCubes[0].Value.transformationClass.testTrans.vector;
//                                        //teilpaket.transform.position += sortedCubes[0].Value.transformationClass.transVector;
//
//                                        //trigger state machine change
//                                        //do a translation
//                                        stateMachine.TranslationObject.Add(sortedCubes[0].Value.transformationClass
//                                            .transVector);
//
//                                        //change to wait state
//                                        stateMachine.ChangeState(new WaitState());
//                                    }
//                                }
//                                else if (sortedCubes[1].Value.transformationClass.transformationMatrix.operation == "-")
//                                {
//                                    if (confirmed)
//                                    {
//                                        //Minus-Translation durchführen
//                                        //Vector3 translation = sortedCubes[0].Value.transformationClass.testTrans.vector;
//                                        //teilpaket.transform.position -= sortedCubes[0].Value.transformationClass.transVector;
//
//                                        //trigger state machine change
//                                        //do a translation
//                                        stateMachine.TranslationObject.Sub(sortedCubes[0].Value.transformationClass
//                                            .transVector);
//
//                                        //change to wait state
//                                        stateMachine.ChangeState(new WaitState());
//                                    }
//                                }
//                                else
//                                {
//                                    //*-Operation macht keinen Sinn!
//                                    Debug.Log("Fehler!");
//                                }
//                            }
//                            else
//                            {
//                                Debug.Log("Fehler");
//                            }
//                        }
//                        //wenn der erste Würfel eine Rotations-Matrix ist
//                        else if (sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
//                            IntMatrix.ElementTypes.Rotation_X
//                            || sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
//                            IntMatrix.ElementTypes.Rotation_Y
//                            || sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
//                            IntMatrix.ElementTypes.Rotation_Z)
//                        {
//                            //dann MUSS der dritte Würfel ein Objekt/Pivot sein
//                            if (sortedCubes[2].Value.transformationClass.transformationMatrix.elementType ==
//                                IntMatrix.ElementTypes.Objekt
//                                || sortedCubes[2].Value.transformationClass.transformationMatrix.elementType ==
//                                IntMatrix.ElementTypes.Pivot)
//                            {
//                                if (sortedCubes[1].Value.transformationClass.transformationMatrix.operation == "*")
//                                {
//                                    if (confirmed)
//                                    {
//                                        //alpha und die Rotationsachse holen
//                                        float alpha = sortedCubes[0].Value.transformationClass.alpha;
//                                        Vector3 axis = sortedCubes[0].Value.transformationClass.rotationAxisVector;
//
//                                        //Rotation ausführen
//                                        //teilpaket.transform.RotateAround(pseudoWorldCoordinateSystem.transform.position, axis, alpha);
//
//                                        //trigger state machine change
//                                        //do a rotation
//                                        stateMachine.RotationObject.Rotate(axis, alpha);
//
//                                        //change to wait state
//                                        stateMachine.ChangeState(new WaitState());
//                                    }
//                                }
//                                else
//                                {
//                                    //+ und - Operation macht keinen Sinn!
//                                    Debug.Log("Fehler!");
//                                }
//                            }
//                            else
//                            {
//                                Debug.Log("Fehler");
//                            }
//                        }
//                        //wenn der erste Würfel eine Skalierungs-Matrix ist
//                        else if (sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
//                                 IntMatrix.ElementTypes.Skalierung_X
//                                 || sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
//                                 IntMatrix.ElementTypes.Skalierung_Y
//                                 || sortedCubes[0].Value.transformationClass.transformationMatrix.elementType ==
//                                 IntMatrix.ElementTypes.Skalierung_Z)
//                        {
//                            //dann MUSS der dritte Würfel ein Objekt/Pivot sein
//                            if (sortedCubes[2].Value.transformationClass.transformationMatrix.elementType ==
//                                IntMatrix.ElementTypes.Objekt
//                                || sortedCubes[2].Value.transformationClass.transformationMatrix.elementType ==
//                                IntMatrix.ElementTypes.Pivot)
//                            {
//                                if (sortedCubes[1].Value.transformationClass.transformationMatrix.operation == "*")
//                                {
//                                    if (confirmed)
//                                    {
//                                        //Skalierungsvektor holen
//                                        Vector3 scale = sortedCubes[0].Value.transformationClass.scaleVector;
//
//                                        //trigger state machine change
//                                        //do a scaling
//                                        stateMachine.ScalingObject.Scale(pseudoWorldCoordinateSystem.transform, scale);
//
//                                        //change to wait state
//                                        stateMachine.ChangeState(new WaitState());
//                                    }
//                                }
//                                else
//                                {
//                                    //+ und - Operation macht keinen Sinn!
//                                    Debug.Log("Fehler!");
//                                }
//                            }
//                            else
//                            {
//                                Debug.Log("Fehler");
//                            }
//                        }
//                        //TODO: Transponierte Vektoren * Matrix sind möglich, aber noch nicht reingenommen!
//                    }
//                    else
//                    {
//                        Debug.Log("Fehler!");
//                    }
//                }
//                else
//                {
//                    Debug.Log("Nicht genau 3 Würfel gelegt!");
//                }
            }
        }

        /// <summary>
        /// Raises the destroy event.
        /// </summary>
        void OnDestroy ()
        {
            webCamTextureToMatHelper.Dispose ();
        }

        /// <summary>
        /// Raises the back button click event.
        /// </summary>
        public void OnBackButtonClick ()
        {
            SceneManager.LoadScene ("MarkerBasedARExample");
        }

        /// <summary>
        /// Raises the play button click event.
        /// </summary>
        public void OnPlayButtonClick ()
        {
            webCamTextureToMatHelper.Play ();
        }

        /// <summary>
        /// Raises the pause button click event.
        /// </summary>
        public void OnPauseButtonClick ()
        {
            webCamTextureToMatHelper.Pause ();
        }

        /// <summary>
        /// Raises the stop button click event.
        /// </summary>
        public void OnStopButtonClick ()
        {
            webCamTextureToMatHelper.Stop ();
        }

        /// <summary>
        /// Raises the change camera button click event.
        /// </summary>
        public void OnChangeCameraButtonClick ()
        {
            webCamTextureToMatHelper.requestedIsFrontFacing = !webCamTextureToMatHelper.IsFrontFacing ();
        }
    }
}
