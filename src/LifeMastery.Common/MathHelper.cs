﻿namespace LifeMastery.Common;

public static class MathHelper
{
    public static double Round(double value)
    {
        return Math.Round(value, 1);
    }

    public static decimal Round(decimal value)
    {
        return Math.Round(value, 1);
    }
}
