using System;
using UnityEngine;

[Serializable]
public struct KHDamage
{
    [Tooltip("First / Instant damage dealt to target")]
    [Min(0)] public float damage;

    [Tooltip("Additional Damage Percent Of Main Damage Dealt Over Time After First Hit")]
    [Range(0f, 1f)] public float overTimeDamagePercent;
    [Min(0f)] public float duration, interval;

    // Getters
    public bool HasDamage => damage != 0;
    public bool HasOvertimeDamage => duration != 0;


    public static KHDamage operator +(KHDamage a, KHDamage b)
    {
        a.damage += b.damage;
        a.overTimeDamagePercent += b.overTimeDamagePercent;
        a.duration += b.duration;
        a.interval += b.interval;

        return a;
    }
}