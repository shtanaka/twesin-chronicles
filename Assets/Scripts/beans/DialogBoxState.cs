using System;
using UnityEngine;

[CreateAssetMenu(menuName = "State")]
public class DialogBoxState : ScriptableObject
{
    [SerializeField] private DialogBoxState[] nextStates;
    [TextArea(10, 8)] [SerializeField] private string title;
    [TextArea(10, 8)] [SerializeField] private string storyText;

    public bool HasOptions()
    {
        return nextStates.Length > 1;
    }

    public string GetTitle()
    {
        return title;
    }

    public int GetQuantityOfOptions()
    {
        return nextStates.Length;
    }

    public DialogBoxState GetNextState(int index)
    {
        return nextStates[index];
    }

    public DialogBoxState[] GetNextStates()
    {
        return nextStates;
    }

    public string GetStoryText() => storyText;
}
