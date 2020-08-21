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

    public GameObject CubeArrow = null;

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
            if (Input.touchCount == 1)
            {
                // TODO: check for ray hit with placed then place UI with info
            }
            if (Input.touchCount > 1)
            {
                // TODO: resize PlacedObject
            }
            else if (CubeArrow != null)
            {
                renderArrow();
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
            }
        }
    }

    /// <summary>
    /// Hint to locate PlacedObject
    /// Draw arrow from center of screen to placed obj, similiar to ADNF arrow
    /// </summary>
    private void renderArrow()
    {
        // Don't update arrow position when PlacedObject is on the screen
        if (inFieldOfView())
        {
            return;
        }

        // How far in front of the camera should the arrow start (1 meter / 8)
        Vector3 arrowFrom = Camera.main.transform.position + (Camera.main.transform.forward / 8);

        // determine the size of the arrow
        Vector3 arrowSpan = PlacedObject.transform.position - arrowFrom;
        Vector3 arrowCenter = arrowSpan / 8; // center of the arrow 1/8 down the line
        float arrowLength = arrowCenter.magnitude * 2;

        // apply the size of the arrow
        CubeArrow.transform.localScale = new Vector3(arrowLength / 16, arrowLength / 16, arrowLength);
        CubeArrow.transform.position = arrowFrom + arrowCenter;
        CubeArrow.transform.LookAt(PlacedObject.transform);
    }

    /// <returns> true if PlacedObject is inside the field of view of the camera </returns>
    private bool inFieldOfView()
    {
        Vector3 toPlaced = PlacedObject.transform.position - Camera.main.transform.position;
        float angleToPlaced = Vector3.Angle(Camera.main.transform.forward, toPlaced);
        return angleToPlaced < Camera.main.fieldOfView;
    }
}
