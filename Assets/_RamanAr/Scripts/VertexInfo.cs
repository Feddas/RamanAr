using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(Collider))]
public class VertexInfo : MonoBehaviour
{
    [System.Serializable]
    public class Vector3Event : UnityEngine.Events.UnityEvent<Vector3> { }

    [Tooltip("When true, the closest vertex to the touch point is selected")]
    public bool SnapToVertex = true;

    public Vector3Event OnVertexSelected = null;

    private Vector3 lastCursorPosition;

    private Mesh mesh
    {
        get
        {
            if (_mesh == null)
            {
                _mesh = this.GetComponent<MeshFilter>().mesh;
            }
            return _mesh;
        }
    }
    private Mesh _mesh = null;

    // void Start() { }
    // void Update() { }

    /// <summary>
    /// Handle Touch.Began to register stationary taps
    /// </summary>
    private void OnMouseDown()
    {
        OnMouseDrag();
    }

    private void OnMouseDrag()
    {
        // Debug.Log("OnMouseDown ");
        Vector3 cursorPosition = touchOrMouseNewPosition();
        if (cursorPosition == Vector3.zero)
        {
            return;
        }

        var ray = Camera.main.ScreenPointToRay(cursorPosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 20))
        {
            Vector3 hit = hitInfo.point;
            Debug.Log("hit " + hitInfo.transform.name
                + " at " + hit.ToString("F3")
                + " snap to " + nearestVertex(hit).ToString("F3"));
            raiseVertexSelected(hit);
        }
    }

    private void raiseVertexSelected(Vector3 pointOnCollider)
    {
        if (OnVertexSelected != null)
        {
            if (SnapToVertex)
            {
                OnVertexSelected.Invoke(nearestVertex(pointOnCollider));
            }
            else
            {
                // convert to local space
                pointOnCollider = transform.InverseTransformPoint(pointOnCollider);

                OnVertexSelected.Invoke(pointOnCollider);
            }
        }
    }

    private Vector3 touchOrMouseNewPosition()
    {
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Stationary)
            {
                return touch.position;
            }
        }
        else if (Input.GetMouseButton(0)
            && Input.mousePosition != lastCursorPosition)
        {
            lastCursorPosition = Input.mousePosition;
            return lastCursorPosition;
        }

        return Vector3.zero;
    }

    /// <summary>
    /// https://answers.unity.com/questions/7788/closest-point-on-mesh-collider.html
    /// </summary>
    /// <param name="toPoint"></param>
    /// <returns> local position of vertex on mesh closest to sent point </returns>
    private Vector3 nearestVertex(Vector3 toPoint)
    {
        // convert to local space
        toPoint = transform.InverseTransformPoint(toPoint);

        float minDistanceSqr = Mathf.Infinity;
        Vector3 nearestVertex = Vector3.zero;

        // scan all vertex in mesh.vertices
        foreach (var vertex in mesh.vertices)
        {
            float distSqr = (toPoint - vertex).sqrMagnitude;
            if (distSqr < minDistanceSqr)
            {
                minDistanceSqr = distSqr;
                nearestVertex = vertex;
            }
        }

        return nearestVertex;
    }
}
