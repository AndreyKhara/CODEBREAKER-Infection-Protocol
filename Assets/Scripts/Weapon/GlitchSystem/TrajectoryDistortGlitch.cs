using UnityEngine;
using Cysharp.Threading.Tasks;
using System; 

[Serializable] 
public class TrajectoryDistortGlitch : GlitchEffect
{
    private float _spreadAngleAdd = 10f;

    public override void Apply(Gun gun){
        ChangeValue(() => gun.Spread, (val) => gun.Spread = val, _spreadAngleAdd, 3f).Forget();
        gun._instability = 30;
    }

   
}
