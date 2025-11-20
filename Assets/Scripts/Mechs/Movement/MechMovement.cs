using UnityEngine;

public class MechMovement : MonoBehaviour
{
    private MechController _controller;
    private MechStatsComponent _mechStats;
    void Start()
    {
        _controller = gameObject.GetComponentInParent<MechController>();
        if (_controller == null)
        {
            LogUtils.LogWarning(this, "No Controller in parent found");
            Destroy(this);
        }
        _mechStats = gameObject.GetComponentInParent<MechStatsComponent>();
        if (_mechStats == null)
        {
            LogUtils.LogWarning(this, "No mech stats in parent found");
            Destroy(this);
        }
    }
    
    void Update()
    {
        
    }
}
