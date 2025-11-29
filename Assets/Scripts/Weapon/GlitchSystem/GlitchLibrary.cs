using UnityEngine;
using System.Collections.Generic;
using System.Linq; 


[CreateAssetMenu(fileName = "GlitchLibrary", menuName = "Glitch System/Glitch Library")]
public class GlitchLibrary: ScriptableObject
{
    [SerializeField] private List<GlitchEffect> _allGlitches = new List<GlitchEffect>();
    
    public IGlitch[] AllGlitches => _allGlitches.ToArray();

    public IGlitch GetRandomGlitch()
    {
        int randomIndex = Random.Range(0, _allGlitches.Count);
        return _allGlitches[randomIndex];
    }
}
