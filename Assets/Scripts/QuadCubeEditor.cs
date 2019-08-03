using System;
using UnityEngine;

[ExecuteInEditMode]
public class QuadCubeEditor : MonoBehaviour
{
    public const int POSITION_Y = 0;

    [SerializeField] [Range(1, 20)] private int snapIncrement = 10;

    [SerializeField] TextMesh textMesh;

    private Vector3 lastPosition;

    private void Start()
    {
        UpdateLastPosition();
    }

    private void UpdateLastPosition()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        if (transform.position != lastPosition)
        {
            UpdatePosition();
        }
    }

    private void UpdatePosition()
    {
        SnapTransformPositionHorizontal();
        UpdateName();
        UpdateTextMesh();
    }

    private void UpdateName()
    {
        gameObject.name = "Quad Cube ( " + SnapIndex(transform.position.x) + " , " + SnapIndex(transform.position.z) + " )";
    }

    public void UpdateTextMesh()
    {
        textMesh.text = SnapIndex(transform.position.x) + "," + SnapIndex(transform.position.z);
}

    private void SnapTransformPositionHorizontal()
    {
        int snapPosX = SnapPosition(transform.position.x);
        int snapPosZ = SnapPosition(transform.position.z);

        transform.position = new Vector3(snapPosX, POSITION_Y, snapPosZ);
    }

    private int SnapIndex(float position)
    {
        return Mathf.RoundToInt(position) / snapIncrement;
    }

    private int SnapPosition(float position)
    {
        return SnapIndex(position) * snapIncrement;
    }
}
