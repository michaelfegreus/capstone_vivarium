using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LEVEL_dolly_custom : MonoBehaviour
{
    public CinemachineSmoothPath myPath;
    public Transform playerAnimation;
    CinemachineDollyCart myCart;

    // Start is called before the first frame update
    void Start()
    {
        myCart = GetComponent<CinemachineDollyCart>();
    }

    // Update is called once per frame
    void Update()
    {
        myCart.m_Position = myPath.FindClosestPoint(playerAnimation.position, -0, -1, 10);
    }
}
