//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventListenertInterfaceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public interface IPulseListener {
    void OnPulse(AudioEntity entity, double thisPulseTime, double nextPulseTime, double period, uint pulsesPerBeat, double latency);
}
