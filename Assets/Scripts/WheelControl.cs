using UnityEngine;

public class WheelControl : MonoBehaviour
{
    public Transform wheelModel;

    [HideInInspector] public WheelCollider WheelCollider;
    
    public bool steerable, motorized;

    Vector3 position;
    Quaternion rotation;
    
    private void Start()
    {
        WheelCollider = GetComponent<WheelCollider>();
    }
    
    private void Update()
    {
        WheelCollider.GetWorldPose(out position, out rotation);
        wheelModel.transform.position = position;
        wheelModel.transform.rotation = rotation;
    }
}
