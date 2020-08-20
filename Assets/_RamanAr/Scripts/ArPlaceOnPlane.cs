using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ArPlaceOnPlane : MonoBehaviour
{
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    [SerializeField]
    private ARRaycastManager raycastManager = null;

    [SerializeField]
    private GameObject objectToPlace = null;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;

                    Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
                }
            }
        }
    }
}
