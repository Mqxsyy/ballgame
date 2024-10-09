using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotX;
    public float rotY;
    public float rotZ;

    void Update()
    {
        Vector3 newRotation = new Vector3(rotX, rotY, rotZ) * Time.deltaTime;
        transform.Rotate(newRotation, Space.World);
    }
}
