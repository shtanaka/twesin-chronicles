using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    private State state;
    [SerializeField] private State startingState;
    [SerializeField] private DialogController dialogBoxController;

    void Start()
    {
        state = startingState;
        Debug.Log(state.GetStateStory());
        dialogBoxController.DisplayStateText(state.GetStateStory());
    }

    void Update()
    {
        if (dialogBoxController.hasFinishedDisplayingText)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                state = state.GetNextStateByChoice(ChoiceEnum.OptionA);
                Debug.Log(state.GetStateStory());
                dialogBoxController.DisplayStateText(state.GetStateStory());
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                state = state.GetNextStateByChoice(ChoiceEnum.OptionB);
                Debug.Log(state.GetStateStory());
                dialogBoxController.DisplayStateText(state.GetStateStory());
            }

        }
    }
}
