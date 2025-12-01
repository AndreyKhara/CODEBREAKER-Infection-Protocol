using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlitchManager
{
    private GlitchLibrary _glitchLibrary;
    private Gun _currentGun;

    public GlitchManager(Gun gun)
    {
        _currentGun = gun;
        _glitchLibrary = Resources.Load<GlitchLibrary>("GlitchLibrary");
    }
    public void GlitchEffect()
    {
        IGlitch glitch = _glitchLibrary.GetRandomGlitch();
        glitch.Apply(_currentGun);
    }
}
