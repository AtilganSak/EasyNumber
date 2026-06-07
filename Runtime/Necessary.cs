using System;

public class Necessary
{
    public static string[] ScoreNames = new string[]
    {
        "", "K", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj",
        "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc",
        "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz",
    };

    public static string Convert(double score, int decimals = 1)
    {
        if (score < 0)
            return "-" + Convert(-score, decimals);

        if (score < 1000)
            return ((long)score).ToString();

        int tier = 0;
        double val = score;
        while (val >= 1000 && tier < ScoreNames.Length - 1)
        {
            val /= 1000.0;
            tier++;
        }

        return val.ToString("F" + decimals, System.Globalization.CultureInfo.InvariantCulture) + ScoreNames[tier];
    }
}