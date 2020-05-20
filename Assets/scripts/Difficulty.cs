using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Difficulty
{
    private static string difficulty = "normal";

    public static void setDifficulty(string s)
    {
        difficulty = s;
    }

    public static string getDifficulty()
    {
        return difficulty;
    }
}
