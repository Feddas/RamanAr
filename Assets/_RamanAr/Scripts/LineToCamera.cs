using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineToCamera : MonoBehaviour
{
    private LineRenderer lineRenderer
    {
        get
        {
            if (_lineRenderer == null)
            {
                _lineRenderer = this.GetComponent<LineRenderer>();
            }
            return _lineRenderer;
        }
    }
    private LineRenderer _lineRenderer = null;

    private Transform cameraTransform
    {
        get
        {
            if (_cameraTransform == null)
            {
                _cameraTransform = Camera.main.transform;
            }
            return _cameraTransform;
        }
    }
    private Transform _cameraTransform = null;

    [Tooltip("If given, used to convert the origin point from a local space relative to this transfrom to instead be world space")]
    [SerializeField]
    private Transform originTransform = null;

    [SerializeField]
    private Vector3 lineOrigin = Vector3.zero;

    // void Start() { }

    void Update()
    {
        // 10 cm above phone
        Vector3 cameraOffset = cameraTransform.position + (cameraTransform.up / 10);
        lineRenderer.SetPosition(1, cameraOffset);
    }

    /// <summary> Called by OnVertexSelected </summary>
    public void UpdateLineOrigin(Vector3 toOrigin)
    {
        if (originTransform != null)
        {
            toOrigin = originTransform.TransformPoint(toOrigin);
        }

        lineRenderer.SetPosition(0, toOrigin);
    }
}
