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
            //This scales the light with the particle size
            m_Instances[i].transform.localScale = new Vector3(m_Particles[i].startSize, m_Particles[i].startSize, m_Particles[i].startSize);
            // TODO: THIS IS VERY BAD PRACTICE AND SHOULD BE CHANGED - WE SHOULD BE STORING AN ARRAY OF THE LIGHT COMPONENTS INSTEAD OF RUNNING GETCOMPONENT<>() EVERY LOOP
            m_Instances[i].GetComponent<Light2D>().intensity = m_Particles[i].GetCurrentColor(m_ParticleSystem).a/369f;

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









