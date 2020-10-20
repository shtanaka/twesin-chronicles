using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    private float oldTextHeight;
    private bool isNewLine = false;
    private bool isHeightOverflowing = false;

    private DialogBoxState currentDialogBoxState;

    [SerializeField] private Text dialogTextComponent;
    [SerializeField] private Text hiddenDialogTextComponent;

    public float textDelay = 0.05f;
    public string displayedText = "";
    public bool hasFinishedDisplayingText = true;

    public void DisplayState(DialogBoxState state)
    {
        currentDialogBoxState = state;
        ClearDialogBox();
        StartCoroutine(StartDisplayStateRoutine());
    }

    private void ClearDialogBox()
    {
        isNewLine = false;
        displayedText = "";
        oldTextHeight = default;
        hasFinishedDisplayingText = false;
        dialogTextComponent.text = displayedText;
        hiddenDialogTextComponent.text = displayedText;
    }

    private IEnumerator StartDisplayStateRoutine()
    {
        hasFinishedDisplayingText = false;
        string[] words = currentDialogBoxState.GetStoryText().Split(' ');

        foreach (string word in words)
        {
            AddWordToHiddenDialogBox(word);
            CheckHiddenTextHeight();

            if (isHeightOverflowing)
            {
                yield return WaitForKeyPress(KeyCode.Return);
                ClearDialogBox();
                AddWordToHiddenDialogBox(word);
            }

            yield return AddWordToDialogBox(word);
        }
        hasFinishedDisplayingText = true;
    }

    private void CheckHiddenTextHeight()
    {
        // Dialog Box Component
        float parentHeight = GetComponent<RectTransform>().rect.height;
        float textHeight = LayoutUtility.GetPreferredHeight(hiddenDialogTextComponent.rectTransform);
        isHeightOverflowing = textHeight > parentHeight;

        if (oldTextHeight == default)
        {
            oldTextHeight = textHeight;
        }

        isNewLine = textHeight > oldTextHeight;
        if (isNewLine)
        {
            oldTextHeight = textHeight;
        }
    }

    private void AddWordToHiddenDialogBox(string word)
    {
        hiddenDialogTextComponent.text += ' ' + word;
    }

    private IEnumerator AddWordToDialogBox(string word)
    {
        displayedText += ' ';

        foreach (char character in word)
        {
            displayedText += character;
            dialogTextComponent.text = displayedText;
            yield return new WaitForSeconds(textDelay);
        }
    }

    private IEnumerator WaitForKeyPress(KeyCode key)
    {
        bool done = false;
        while (!done)
        {
            if (Input.GetKeyDown(key)) done = true;
            yield return null;
        }
    }
}
