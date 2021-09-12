using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(ParticleSystem))]
public class AttachLightsToFireflies : MonoBehaviour
{
    public GameObject m_Prefab;

    private ParticleSystem m_ParticleSystem;
    private List<GameObject> m_Instances = new List<GameObject>();
    private ParticleSystem.Particle[] m_Particles;
    [SerializeField] private float intensityMultiplier;
    // TODO: VV This is probably how you should start to fix the problem below! VV
    // private List<Light2D>m_Lights = new List<Light2D>();


    void Start()
    {
        m_ParticleSystem = GetComponent<ParticleSystem>();
        m_Particles = new ParticleSystem.Particle[m_ParticleSystem.main.maxParticles];
    }


    void LateUpdate()
    {
        int count = m_ParticleSystem.GetParticles(m_Particles);

        while (m_Instances.Count < count)
            m_Instances.Add(Instantiate(m_Prefab, m_ParticleSystem.transform));


        bool worldSpace = (m_ParticleSystem.main.simulationSpace == ParticleSystemSimulationSpace.World);
        for (int i = 0; i < m_Instances.Count; i++)
        {
            Light2D thisLight = m_Instances[i].GetComponent<Light2D>();
            //This scales the light with the particle size
            thisLight.pointLightOuterRadius = m_Particles[i].startSize;
            // TODO: THIS IS VERY BAD PRACTICE AND SHOULD BE CHANGED - WE SHOULD BE STORING AN ARRAY OF THE LIGHT COMPONENTS INSTEAD OF RUNNING GETCOMPONENT<>() EVERY LOOP
            thisLight.intensity = m_Particles[i].GetCurrentColor(m_ParticleSystem).a/intensityMultiplier;

            if (i < count)
            {
                if (worldSpace)
                    m_Instances[i].transform.position = m_Particles[i].position;
                else
                    m_Instances[i].transform.localPosition = m_Particles[i].position;
                m_Instances[i].SetActive(true);
            }
            else
            {
                m_Instances[i].SetActive(false);
            }
        }
    }
}









