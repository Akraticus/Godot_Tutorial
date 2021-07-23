using System;


public static class RandomExtensions
{
    public static float RandRange(this Random random, float min, float max)
    {
        return (float) random.NextDouble() * (max - min) + min;
    }
}