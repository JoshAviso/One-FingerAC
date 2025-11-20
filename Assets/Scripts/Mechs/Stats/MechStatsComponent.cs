using UnityEngine;

public class MechStatsComponent : MonoBehaviour
{
    [SerializeReference] private MechStatsScriptable _defaultMechStats;
    private MechStats _mechStats = new();
    public MechStats Stats
    {
        get { return _mechStats; }
        set
        {
            _mechStats = value;
            // Other set behavior
        }
    }
    
    void Start()
    {
        if(_defaultMechStats != null) 
            Stats = _defaultMechStats.Stats;
    }
}
