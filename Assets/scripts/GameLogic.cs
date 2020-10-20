using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private DialogBoxState currentDialogBoxState;
    [SerializeField] private DialogBoxState startingState;
    [SerializeField] private DialogController dialogBoxController;
    [SerializeField] private DialogOptionSelectorController dialogOptionSelectorController;
     
    private Vector2 hotSpot = Vector2.zero;
    private CursorMode cursorMode = CursorMode.Auto;
    public Texture2D defaultCursorTexture;

    void Start()
    {
        currentDialogBoxState = startingState;
        dialogBoxController.DisplayState(currentDialogBoxState);
        Cursor.SetCursor(defaultCursorTexture, hotSpot, cursorMode);
    }

    void Update()
    {
        if (dialogBoxController.hasFinishedDisplayingText)
        {
            if (currentDialogBoxState.GetQuantityOfOptions() > 1)
            {
                if (dialogOptionSelectorController.IsOptionConfirmed())
                {
                    currentDialogBoxState = dialogOptionSelectorController.GetSelectedState();
                    dialogBoxController.DisplayState(currentDialogBoxState);
                    dialogOptionSelectorController.ClearDialogOptionSelectorBox();
                }
                else if (!dialogOptionSelectorController.IsOpen()) 
                {
                    dialogOptionSelectorController.DisplayOptionsFromState(currentDialogBoxState);
                }
            } else
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    currentDialogBoxState = currentDialogBoxState.GetNextState(0);
                    dialogBoxController.DisplayState(currentDialogBoxState);
                }
            }
        }
    }
}
