using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogOptionSelectorController : MonoBehaviour
{
    private bool isOpen = false;
    private bool hasFinishedDisplayingOptions = false;
    private bool isOptionConfirmed = false;

    private int selectedOptionIndex = -1;
    private DialogBoxState currentDialogBoxState;
    private string[] dialogBoxOptionTextList;
    private Button[] optionButtons;

    [SerializeField] private Image GuiBG;
    [SerializeField] private Button DefaultOption;
    [SerializeField] private TextMeshProUGUI OptionCursor;

    public float textDelay = 0.01f;

    public bool IsOptionConfirmed()
    {
        return isOptionConfirmed;
    }

    public bool IsOpen()
    {
        return isOpen;
    }

    private void Update()
    {
        if (isOpen && hasFinishedDisplayingOptions)
        {
            ApplyKeyDownEffects();
        }
    }


    public void DisplayOptionsFromState(DialogBoxState state)
    {
        isOpen = true;
        currentDialogBoxState = state;
        StartCoroutine(StartDisplayStateOptionsRoutine());
    }

    public void ClearDialogOptionSelectorBox()
    {
        dialogBoxOptionTextList = null;
        isOpen = false;
        isOptionConfirmed = false;
        hasFinishedDisplayingOptions = false;
        OptionCursor.gameObject.SetActive(false);
        foreach (Button button in optionButtons)
        {
            button.gameObject.SetActive(false);
            Destroy(button);
        }
        optionButtons = null;
    }

    private IEnumerator StartDisplayStateOptionsRoutine()
    {
        var options = currentDialogBoxState.GetNextStates();
        hasFinishedDisplayingOptions = false;
        dialogBoxOptionTextList = new string[options.Length];
        optionButtons = new Button[options.Length];
        float topOffset = 0.0f;
        for (int optionIndex = 0; optionIndex < options.Length; optionIndex++) 
        {
            var option = options[optionIndex];
            dialogBoxOptionTextList[optionIndex] = "";
            string[] words = option.GetTitle().Split(' ');
            var defaultOptionComponent = DefaultOption.GetComponent<RectTransform>();
            var optionComponent = Instantiate(DefaultOption, Vector3.zero, Quaternion.identity) as Button;
            optionButtons[optionIndex] = optionComponent;
            optionComponent.gameObject.SetActive(true);


            var optionComponentRectTransform = optionComponent.GetComponent<RectTransform>();
            optionComponentRectTransform.SetParent(GetComponent<RectTransform>());
            var componentPosition = defaultOptionComponent.position;
            componentPosition.y += topOffset;
            optionComponentRectTransform.SetPositionAndRotation(componentPosition, defaultOptionComponent.rotation);

            foreach (string word in words)
            {
                yield return AddWordToDialogOptionBox(word, optionComponent, optionIndex);
            }

            topOffset += -30;
        }
        hasFinishedDisplayingOptions = true;
        selectedOptionIndex = 0;
        OptionCursor.gameObject.SetActive(true);
    }

    private IEnumerator AddWordToDialogOptionBox(string word, Button optionComponent, int optionIndex)
    {
        var buttonTextComponent = optionComponent.GetComponentInChildren<TextMeshProUGUI>();
        dialogBoxOptionTextList[optionIndex] += ' ';
        foreach (char character in word)
        {
            dialogBoxOptionTextList[optionIndex] += character;
            buttonTextComponent.SetText(dialogBoxOptionTextList[optionIndex]);
            yield return new WaitForSeconds(textDelay);
        }
    }

    void ApplyKeyDownEffects()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveSelectionCursorDown();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveSelectionCursorUp();
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            isOptionConfirmed = true;
        }
    }

    private void MoveSelectionCursorUp()
    {
        if (selectedOptionIndex > 0)
        {
            var optionCursorRectTransform = OptionCursor.GetComponent<RectTransform>();
            var newPosition = OptionCursor.GetComponent<RectTransform>().position;
            newPosition.y += 30;
            optionCursorRectTransform.SetPositionAndRotation(newPosition, optionCursorRectTransform.rotation);
            selectedOptionIndex--;
        }
    }

    private void MoveSelectionCursorDown()
    {
        if (selectedOptionIndex < currentDialogBoxState.GetQuantityOfOptions() - 1)
        {
            var optionCursorRectTransform = OptionCursor.GetComponent<RectTransform>();
            var newPosition = OptionCursor.GetComponent<RectTransform>().position;
            newPosition.y += -30;
            optionCursorRectTransform.SetPositionAndRotation(newPosition, optionCursorRectTransform.rotation);
            selectedOptionIndex++;
        }
    }

    public DialogBoxState GetSelectedState()
    {
        return currentDialogBoxState.GetNextState(selectedOptionIndex);
    }
}
