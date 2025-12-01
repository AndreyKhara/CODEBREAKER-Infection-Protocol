using UnityEngine;
using Cysharp.Threading.Tasks;
using System; 

[Serializable] 
public class OverHitGlitch :  GlitchEffect
{
    public override void Apply(Gun gun){
        ChangeValue(() => gun._stopShoot, (val) => gun._stopShoot = val, true, 6f).Forget();
        gun._instability = 30;
    }
}
