using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private State state;
    [SerializeField] private State startingState;
    [SerializeField] private DialogController dialogBoxController;

    void Start()
    {
        state = startingState;
        dialogBoxController.DisplayStateText(state.GetStoryText());
    }

    void Update()
    {
        if (dialogBoxController.hasFinishedDisplayingText)
        {
            var stateType = state.GetStateType();
            switch(stateType)
            {
                case StateTypeEnum.BinaryOptionState:
                    ChangeStateWhenOptionIsSelected();
                    break;
                case StateTypeEnum.VoidState:
                    ChangeStateWhenEnterIsPressed();
                    break;
            }
        }
    }

    void ChangeBinaryState(ChoiceEnum selectedOption)
    {
        state = state.GetNextStateByChoice(selectedOption);
        dialogBoxController.DisplayStateText(state.GetStoryText());
    }

    void ChangeVoidState()
    {
        state = state.GetNextState();
        dialogBoxController.DisplayStateText(state.GetStoryText());
    }

    void ChangeStateWhenOptionIsSelected()
    {
         
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeBinaryState(ChoiceEnum.OptionA);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            ChangeBinaryState(ChoiceEnum.OptionB);
        }
    }

    void ChangeStateWhenEnterIsPressed()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ChangeVoidState();
        }
    }
}
