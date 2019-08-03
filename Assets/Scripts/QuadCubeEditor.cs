using UnityEngine;

[ExecuteInEditMode]
public class QuadCubeEditor : MonoBehaviour
{
    public const int POSITION_Y = 0;

    [SerializeField] [Range(1, 20)] private int snapIncrement = 10;

    [SerializeField] TextMesh textMesh;

    void Update()
    {
        SnapTransformPositionHorizontal();
    }

    private void UpdateTextMesh(int posX, int posZ)
    {
        textMesh.text = SnapIndex(posX).ToString() + "," + SnapIndex(posZ).ToString();
    }

    private void SnapTransformPositionHorizontal()
    {
        int snapPosX = SnapPosition(transform.position.x);
        int snapPosZ = SnapPosition(transform.position.z);

        transform.position = new Vector3(snapPosX, POSITION_Y, snapPosZ);

        UpdateTextMesh(snapPosX, snapPosZ);
    }

    private int SnapIndex(int position)
    {
        return position / snapIncrement;
    }

    private int SnapPosition(float position)
    {
        return Mathf.RoundToInt(position / snapIncrement) * snapIncrement;
    }
}
