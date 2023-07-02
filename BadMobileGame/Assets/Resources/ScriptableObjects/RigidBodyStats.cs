using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RigidBodyStats", menuName = "ScriptableObjects/RigidBodyStats", order = 1)]
public class RigidBodyStats : ScriptableObject
{
    public PhysicsMaterial2D physicsMaterial;
    public float mass;
    public float gravityScale;
    public float linearDrag;
    public float angularDrag;

}
