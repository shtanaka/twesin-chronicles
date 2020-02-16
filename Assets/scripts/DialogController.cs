using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    private static readonly int LONGEST_CHAR_WIDTH = 50;

    private RectTransform parentRect;
    private float computedWidth = 0;
    
    private bool isWidthOverflowing = false;
    private bool isHeightOverflowing = false;
    
    [SerializeField] private Text dialogTextComponent;
    [SerializeField] private Text hiddenDialogTextComponent;

    public string fullText = "";
    public float textDelay = 0.05f;
    public string displayedText = "";

    void Start()
    {
        parentRect = GetComponent<RectTransform>();
        StartCoroutine(DisplayText());
    }

    private void CheckHiddenTextHeight()
    {

        float parentHeight = parentRect.rect.height;
        float textHeight = LayoutUtility.GetPreferredHeight(hiddenDialogTextComponent.rectTransform);

        isHeightOverflowing = textHeight > parentHeight;
    }

    private void CheckHiddenTextWidth()
    {
        float parentWidth = parentRect.rect.width;
        float textWidth = LayoutUtility.GetPreferredWidth(hiddenDialogTextComponent.rectTransform);
        float calcWidth = textWidth - computedWidth;

        isWidthOverflowing = calcWidth >= parentWidth - 5;

        if (isWidthOverflowing)
        {
            computedWidth += calcWidth - (calcWidth - parentWidth);
        }
    }

    private void ClearDialogBox()
    {
        displayedText = "";
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

        CheckHiddenTextWidth();
        if (isWidthOverflowing)
        {
            displayedText += '\n';
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
        string[] words = fullText.Split(' ');

        foreach (string word in words)
        {
            AddWordToHiddenDialogBox(word);
            CheckHiddenTextHeight();

            if (isHeightOverflowing)
            {
                yield return WaitForKeyPress(KeyCode.Return);
                ClearDialogBox();
            }

            yield return AddWordToDialogBox(word);
        }
    }
}
