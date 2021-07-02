using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class TrackedImageDistanceTrigger : MonoBehaviour
{
    public GameObject usercamera;
    private GameObject projectedobject;

    ARTrackedImageManager m_TrackedImageManager;
    // Start is called before the first frame update

    void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            // Give the initial image a reasonable default scale
            trackedImage.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }


    }
    private void FixedUpdate()
    {
        projectedobject = GameObject.FindGameObjectWithTag("BookMock");
        if (projectedobject != null && usercamera != null)
        {
            float dist = Vector3.Distance(projectedobject.transform.position, usercamera.transform.position);
            if(dist > .4)
            {
                projectedobject.GetComponent<Animator>().SetBool("CameraClose", false);
            }
            else
            {
                projectedobject.GetComponent<Animator>().SetBool("CameraClose", true);
            }

           
        }
    }
}
