using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesLightningPlayer : MonoBehaviour
{
    public float chargesMaxAngularVel = 4f;
    [SerializeField]
    ParticleSystem particlesys;

    //// Start is called before the first frame update
    //public void SetValueParticlesVel(float factor)
    //{
    //    var sty = new ParticleSystem.VelocityOverLifetimeModule();

    //    sty.radialMultiplier = factor * Time.deltaTime * chargesMaxAngularVel;

    //    //particlesys. = sty;

        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
