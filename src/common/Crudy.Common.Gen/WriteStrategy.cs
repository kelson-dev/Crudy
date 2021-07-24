namespace Crudy.Common.Gen
{
    /// <summary>
    /// Indicates how the value written to storage should be determined
    /// </summary>
    public enum WriteStrategy
    {
        Direct, // write the value as-is
        Random, // generate a new random value (defer to storage layer if possible)
        Increment, // increase the value by one (defer to storage layer if possible)
        CurrentTime, // current time (defer to storage layer if possible)
        Expression, // execute a specified expression to determine the value before sending to storage layer
    }
}
