using System;

/// <summary>
/// Volume envelope holds Sample settings for envelope filter to act on
/// </summary>
[Serializable]
public class VolumeEnvelope
{
    public bool Enabled;
    public bool Reverse;
    public float Volume = 1f;
    public double Offset = 0f;
    public float AttackTime = 0f;
    public float SustainTime = 10f;
    public float ReleaseTime = 0f;
}

