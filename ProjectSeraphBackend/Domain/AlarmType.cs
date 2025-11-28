namespace ProjectSeraphBackend.Domain
{
    /// <summary>
    /// This enum represents the type of alarm generated from a measurement.
    /// </summary>
    public enum AlarmType
    {
        Green, //Measurement is normal
        Yellow, //Abnormal but not critical
        Red //Critical measurement 
    }
}
