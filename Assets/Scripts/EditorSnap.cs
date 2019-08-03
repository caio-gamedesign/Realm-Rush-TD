using UnityEngine;

[ExecuteInEditMode]
public class EditorSnap : MonoBehaviour
{
    public const int POSITION_Y = 0;

    [SerializeField] [Range(1, 20)] private int snapIncrement = 10;

    // Update is called once per frame
    void Update()
    {
        SnapTransformPositionHorizontal();
    }

    private void SnapTransformPositionHorizontal()
    {
        int snapPosX = SnapPosition(transform.position.x);
        int snapPosZ = SnapPosition(transform.position.z);

        transform.position = new Vector3(snapPosX, POSITION_Y, snapPosZ);
    }

    private int SnapPosition(float position)
    {
        return Mathf.RoundToInt(position / snapIncrement) * snapIncrement;
    }
}
