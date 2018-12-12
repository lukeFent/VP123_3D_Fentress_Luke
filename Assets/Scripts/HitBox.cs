using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]

//public class MyFloatEvent : UnityEvent<float>
public class MyVectorEvent : UnityEvent<Vector3>
{

}

public class HitBox : MonoBehaviour {

    //public MyFloatEvent OnHit;
    public MyVectorEvent OnHit;

}
