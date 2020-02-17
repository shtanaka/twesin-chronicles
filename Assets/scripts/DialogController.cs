using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    private RectTransform parentRect;
    
    private float oldTextHeight;
    private bool isNewLine = false;
    private bool isHeightOverflowing = false;
    
    [SerializeField] private Text dialogTextComponent;
    [SerializeField] private Text hiddenDialogTextComponent;

    public string fullText = "";
    public float textDelay = 0.05f;
    public string displayedText = "";
    public bool hasFinishedDisplayingText = true;
 
    private void Start()
    {
        parentRect = GetComponent<RectTransform>();
    }

    private void CheckHiddenTextHeight()
    {
        float parentHeight = parentRect.rect.height;
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

    private void ClearDialogBox()
    {
        isNewLine = false;
        displayedText = "";
        oldTextHeight = default;
        hasFinishedDisplayingText = false;
        dialogTextComponent.text = displayedText;
        hiddenDialogTextComponent.text = displayedText;
    }

    private void AddWordToHiddenDialogBox(string word)
    {
        hiddenDialogTextComponent.text += ' ' + word;
    }

    private IEnumerator AddWordToDialogBox(string word)
    {
        displayedText += ' ';

        if (isNewLine)
        {
            displayedText += '\n';
            isNewLine = false;
        }

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

    private IEnumerator DisplayText()
    {
        hasFinishedDisplayingText = false;
        string[] words = fullText.Split(' ');

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

    public void DisplayStateText(string text)
    {
        fullText = text;
        ClearDialogBox();
        StartCoroutine(DisplayText());
    }
}
