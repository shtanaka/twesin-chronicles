using System;
using UnityEngine;

[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject
{
    [SerializeField] private State nextState;
    [SerializeField] private State nextStateA;
    [SerializeField] private State nextStateB;
    [TextArea(10, 8)] [SerializeField] private string storyText;

    public StateTypeEnum GetStateType()
    {
        if (nextState == null && nextStateA != null && nextStateB != null)
        {
            return StateTypeEnum.BinaryOptionState;
        }
        else if (nextState != null && nextStateA == null && nextStateB == null)
        {
            return StateTypeEnum.VoidState;
        }

        throw new Exception("Malformed state.");
    }

    public State GetNextState()
    {
        if (nextState == null)
        {
            throw new Exception("This is not a void State.");
        }
        return nextState;
    }

    public State GetNextStateByChoice(ChoiceEnum selectedOption)
    {
        if (nextStateA == null || nextStateB == null)
        {
            throw new Exception("This is not a binary option state.");
        }

        switch (selectedOption)
        {
            case ChoiceEnum.OptionA:
                return nextStateA;
            case ChoiceEnum.OptionB:
                return nextStateB;
        }
        throw new Exception("Choice need to be in ChoiceEnum");
    }

    public string GetStoryText() => storyText;
}
