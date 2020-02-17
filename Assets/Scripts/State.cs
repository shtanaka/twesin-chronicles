using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog State")]
public class State : ScriptableObject
{
    [TextArea(10, 8)] [SerializeField] private string storyText;
    [SerializeField] private State nextStateA;
    [SerializeField] private State nextStateB;

    public string GetStateStory() => storyText;

    public State GetNextStateByChoice(ChoiceEnum choice)
    {
        switch (choice)
        {
            case ChoiceEnum.OptionA:
                return nextStateA;
            case ChoiceEnum.OptionB:
                return nextStateB;
            default:
                throw new Exception("Choice need to be in ChoiceEnum");
        }
    }
}
