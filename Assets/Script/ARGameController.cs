using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using System.IO;

namespace GoogleARCore
{

    public class ARGameController : MonoBehaviour
    {

        public List<GameObject> faceList;
        public List<Material> backgroundMaterials;
        public static int activeFaceIndex = 0;
        public static int activeBackgroundMaterial = 0;       
        public Camera cam;
        public Canvas canvas;
        public GameObject blink;
        public GameObject backgroundChoser;
        public GameObject aboutPanel;
        private ARCoreBackgroundRenderer backgroundRenderer;
        
        
        /// <summary>
        /// The game object that renders the face attachment on an Augmented Face.
        /// </summary>
        public GameObject FaceAttachment;

        /// <summary>
        /// True if the app is in the process of quitting due to an ARCore connection error,
        /// otherwise false.
        /// </summary>
        private bool m_IsQuitting = false;

        private List<AugmentedFace> m_TempAugmentedFaces = new List<AugmentedFace>();


        /// <summary>
        /// The Unity Awake() method.
        /// </summary>
        public void Awake()
        {
            // Enable ARCore to target 60fps camera capture frame rate on supported devices.
            // Note, Application.targetFrameRate is ignored when QualitySettings.vSyncCount != 0.
            Application.targetFrameRate = 60;
        }

        /// <summary>
        /// The Unity Update method.
        /// </summary>
        public void Update()
        {
            _UpdateApplicationLifecycle();

            // Gets all Augmented Faces.
            Session.GetTrackables<AugmentedFace>(m_TempAugmentedFaces, TrackableQueryFilter.All);

            // Only allows the screen to sleep when ARCore can't detect a face.
            if (m_TempAugmentedFaces.Count == 0)
            {
                Screen.sleepTimeout = SleepTimeout.SystemSetting;
                FaceAttachment.SetActive(false);
            }
            else
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
                FaceAttachment.SetActive(true);
            }

        }

        /// <summary>
        /// Check and update the application lifecycle.
        /// </summary>
        private void _UpdateApplicationLifecycle()
        {
            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            if (m_IsQuitting)
            {
                return;
            }

            // Quit if ARCore was unable to connect and give Unity some time for the toast to
            // appear.
            if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
            {
                _ShowAndroidToastMessage("Camera permission is needed to run this application.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
            else if (Session.Status.IsError())
            {
                _ShowAndroidToastMessage(
                    "ARCore encountered a problem connecting.  Please start the app again.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
        }

        /// <summary>
        /// Actually quit the application.
        /// </summary>
        private void _DoQuit()
        {
            Application.Quit();
        }

        /// <summary>
        /// Show an Android toast message.
        /// </summary>
        /// <param name="message">Message string to show in the toast.</param>
        public void _ShowAndroidToastMessage(string message)
        {
            AndroidJavaClass unityPlayer =
                new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity =
                unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>(
                        "makeText", unityActivity, message, 0);
                    toastObject.Call("show");
                }));
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            backgroundRenderer = cam.GetComponent<ARCoreBackgroundRenderer>();
            FaceAttachment = faceList[activeFaceIndex];
            changeFace(0);
        }


        public void changeFace(int index)
        {
            foreach (GameObject g in faceList)
            {
                if (faceList.IndexOf(g) == index)
                {
                    g.SetActive(true);
                    activeFaceIndex = index;
                    FaceAttachment = g;
                }
                else
                {
                    g.SetActive(false);
                }
            }
        }



        public void changeBackground(int index)
        {
            setActiveCurrentFace(false);
            backgroundRenderer.enabled = false;
            foreach (Material m in backgroundMaterials)
            {
                if (backgroundMaterials.IndexOf(m) == index)
                {
                    backgroundRenderer.enabled = false;
                    backgroundRenderer.BackgroundMaterial = m;
                    activeBackgroundMaterial = index;
                    break;
                }
            }
            backgroundRenderer.enabled = true;
            setActiveCurrentFace(true);
        }

        private void setActiveCurrentFace(bool active)
        {
            faceList[activeFaceIndex].SetActive(active);
        }

    
        //Take a picture :)
        public void OnClickScreenCaptureButton()
        {
            StartCoroutine(CaptureScreen());
        }

        public IEnumerator CaptureScreen()
        {
            Debug.Log("Take a picture!");
            // Wait till the last possible moment before screen rendering to hide the UI
            yield return null;
            canvas.enabled = false;
            blink.SetActive(true);
            blink.SetActive(false);
            // Wait for screen rendering to complete
            yield return new WaitForEndOfFrame();

            // Take screenshot
            string time = System.DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");
            ScreenCapture.CaptureScreenshot( "pic_"+time+".png");
               
            // Show UI after we're done
            canvas.enabled = true;
            
        }

        //Toggle background menu panel
        public void toggleBackgroundChoser()
        {
            if (backgroundChoser.activeSelf)
            {
                backgroundChoser.SetActive(false);
            }
            else
            {
                backgroundChoser.SetActive(true);
                aboutPanel.SetActive(false);
            }
        }

        //Toggle background menu panel
        public void toggleAboutPanel()
        {
            if (aboutPanel.activeSelf)
            {
                aboutPanel.SetActive(false);
            }
            else
            {
                aboutPanel.SetActive(true);
                backgroundChoser.SetActive(false);
            }
        }

    }
}
