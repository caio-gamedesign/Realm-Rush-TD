using System;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class QuadCubeEditor : MonoBehaviour
{
    const int POSITION_Y = 0;

    Vector2Int lastPosition;
    Waypoint waypoint;

    [SerializeField] TextMesh textMesh;

    private void Start()
    {
        waypoint = GetComponent<Waypoint>();
    }

    private void UpdateLastPosition(Vector2Int currentPosition)
    {
        lastPosition = currentPosition;
    }

    void Update()
    {
        UpdatePosition(waypoint.GetGridPosition());
    }

    private void UpdatePosition(Vector2Int currentPosition)
    {
        SnapPosition(currentPosition);

        if (currentPosition != lastPosition)
        {
            UpdateDisplayInfo(currentPosition);
            UpdateLastPosition(currentPosition);
        }
    }

    private void UpdateDisplayInfo(Vector2Int currentPosition)
    {
        gameObject.name = "Quad Cube ( " + currentPosition.x + " , " + currentPosition.y + " )";
        textMesh.text = currentPosition.x + "," + currentPosition.y;
    }

    private void SnapPosition(Vector2Int currentPosition)
    {
        float positionX = currentPosition.x * Waypoint.GRID_SIZE;
        float positionZ = currentPosition.y * Waypoint.GRID_SIZE;
        transform.position = new Vector3(positionX, POSITION_Y, positionZ);
    }
}
