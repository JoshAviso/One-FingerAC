using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Mech/MechStatsScriptable")]
public class MechStatsScriptable : ScriptableObject
{
    [SerializeField] private MechStats _stats = new();
    public MechStats Stats { get { return _stats; } }
}
