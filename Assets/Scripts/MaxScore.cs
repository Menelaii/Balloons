using UnityEngine;

[CreateAssetMenu(fileName = "MaxScore")]
public class MaxScore : ScriptableObject
{
    public int Value { get; private set; }

    public void TryUpdateValue(int score)
    {
        if (score > Value)
            Value = score;
    }
}
