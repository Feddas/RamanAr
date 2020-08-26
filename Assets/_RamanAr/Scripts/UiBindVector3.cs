using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiBindVector3 : MonoBehaviour
{
    public TMPro.TextMeshProUGUI DataViewX = null;
    public TMPro.TextMeshProUGUI DataViewY = null;
    public TMPro.TextMeshProUGUI DataViewZ = null;

    public Vector3 LastData = Vector3.zero;

    // void Start() { }
    // void Update() { }

    /// <summary> Called by OnVertexSelected </summary>
    public void UpdateUi(Vector3 toVector)
    {
        LastData = toVector;
        DataViewX.text = toVector.x.ToString("F3");
        DataViewY.text = toVector.y.ToString("F3");
        DataViewZ.text = toVector.z.ToString("F3");
    }
}
