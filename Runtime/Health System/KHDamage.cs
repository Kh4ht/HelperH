using System;
using UnityEngine;

[Serializable]
public struct KHDamage
{
    public PhysicalDmg physical;
    public FireDmg fire;
    public PoisonDmg poison;


    [Serializable]
    public struct PhysicalDmg
    {
        [Tooltip("First / Instant damage dealt to target")]
        [Min(0)] public float damage;
    }

    [Serializable]
    public struct FireDmg
    {
        [Header("FIRE")]
        [Tooltip("First / Instant damage dealt to target")]
        [Min(0)] public float damage;
        [Min(0)] public float subDamage;
        [Min(1)] public float duration;
        [Min(0.1f)] public float interval;
    }

    [Serializable]
    public struct PoisonDmg
    {
        [Header("POISON")]
        [Tooltip("First / Instant damage dealt to target")]
        [Min(0)] public float damage;
        [Min(0)] public float subDamage;
        [Min(4)] public float duration;
        [Min(0.5f)] public float interval;
    }
}