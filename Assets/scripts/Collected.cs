using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Collected
{
    // Start is called before the first frame update
    private static int collected = 0;

    public static int getCollected()
    {
        return collected;
    }

    public static void setCollected(int coins)
    {
        collected = coins;
        if (collected < 0)
        {
            collected = 0;
        }
    }
}
