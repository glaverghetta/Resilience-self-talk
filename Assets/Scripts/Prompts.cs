using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prompts : MonoBehaviour
{
    public delegate void promptAnsweredDelegate();
    public event promptAnsweredDelegate promptAnsweredEvent;  // Event called when the player clicks "Continue" after answering the prompt

    private static readonly string[] allPrompts = { "I show kindness by: ",                                  // All prompts that may be asked in the game.
                                                 "I am strong because I: ",
                                                 "I am unique because: ",
                                                 "Something that makes me smile is: ",
                                                 "A time I was forgiving was: ",
                                                 "I'm proud of myself because: ",
                                                 "I am motivated by: ",
                                                 "I am talented at: ",
                                                 "A time I exceeded expectations was: ",
                                                 "My greatest accomplishment is: ",
                                                 "I overcome adversity by: ",
                                                 "My favorite part of the day is: ",
                                                 "Something that makes me laugh is: "
                                                                                         };

    private List<string> promptsRemaining;      // List containing the prompts that the user has not been asked yet. Will be repopulated with all prompts if the user is asked every prompt.
    private string currentPrompt;               // Prompt user is currently being asked.

    public GameObject promptUI;
    public Text promptText;

    void Start()
    {
        promptsRemaining = new List<string>();
        if(FindObjectOfType<TowerManager>() != null)
        {
            FindObjectOfType<TowerManager>().waitTimerFinishedEvent += PickRandomPrompt;
        }
    }

    private void PickRandomPrompt()
    {
        if(promptsRemaining.Count == 0)
        {
            RepopulatePromptsRemaining();
        }
        int promptIndex = Random.Range(0, promptsRemaining.Count - 1);
        currentPrompt = promptsRemaining[promptIndex];
        promptsRemaining.RemoveAt(promptIndex);
        ShowPrompt(currentPrompt);
    }

    private void RepopulatePromptsRemaining()
    {
        foreach (string prompt in allPrompts)
        {
            promptsRemaining.Add(prompt);
        }
    }

    private void ShowPrompt(string prompt)
    {
        promptUI.SetActive(true);
        promptText.text = currentPrompt;
    }

    public void PromptAnswered()
    {
        HidePrompt();
        if(promptAnsweredEvent != null)
        {
            promptAnsweredEvent();
        }
    }

    private void HidePrompt()
    {
        promptUI.SetActive(false);

    }
}
