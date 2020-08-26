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

    public GameObject PlacedObject = null;

    // private void Start() { }

    void Update()
    {
        if (Input.touchCount == 1 && PlacedObject == null)
        {
            placeObject();
        }

        else if (PlacedObject != null)
        {
            if (Input.touchCount > 1)
            {
                // TODO: resize and move PlacedObject
            }
        }
    }

    private void placeObject()
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                PlacedObject = Instantiate(objectToPlace, hitPose.position, hitPose.rotation);

                // disable planes so they don't interfere with raycasting against the raman graph
                planeDetection(false);
            }
        }
    }

    private void planeDetection(bool isEnabled)
    {
        ARPlaneManager arPlanes = this.GetComponent<ARPlaneManager>();
        if (arPlanes == null)
        {
            return;
        }

        arPlanes.enabled = isEnabled;

        foreach (ARPlane plane in arPlanes.trackables)
        {
            plane.gameObject.SetActive(isEnabled);
        }
    }
}
