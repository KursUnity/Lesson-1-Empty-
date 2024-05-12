using UnityEngine;

public class Cube : MonoBehaviour
{
    private int _splitChance = 200;
    private int _splitDigit = 2;

    public int TakeSplitChance()
    {
        return _splitChance /= _splitDigit;
    }
}