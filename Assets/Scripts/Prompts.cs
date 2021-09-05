using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class handles the prompts that the player will be asked during the course of the game.
public class Prompts : MonoBehaviour
{
    public delegate void promptAnsweredDelegate();
    public event promptAnsweredDelegate promptAnsweredEvent;  // Event called when the player clicks "Continue" after answering the prompt.

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
                                                 "Something that makes me laugh is: "    };

    private List<string> promptsRemaining;      // List containing the prompts that the user has not been asked yet. Will be repopulated with all prompts if the user is asked every prompt.
    private string currentPrompt;               // Prompt user is currently being asked.

    public GameObject promptUI;                 // GUI used to display prompts on screen.
    public Text promptText;                     // Text object containing the prompt currently being asked.

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
        // If all prompts have been asked, add all possible prompts back into promptsRemaining.
        if(promptsRemaining.Count == 0)
        {
            RepopulatePromptsRemaining();
        }

        // Get and show random prompt from promptsRemaining.
        int promptIndex = Random.Range(0, promptsRemaining.Count);
        currentPrompt = promptsRemaining[promptIndex];
        promptsRemaining.RemoveAt(promptIndex);
        ShowPrompt(currentPrompt);
    }

    // Adds all possible prompts back into promptsRemaining.
    private void RepopulatePromptsRemaining()
    {
        foreach (string prompt in allPrompts)
        {
            promptsRemaining.Add(prompt);
        }
    }

    // Enables the prompt GUI and sets its text to the supplied argument.
    private void ShowPrompt(string prompt)
    {
        promptUI.SetActive(true);
        promptText.text = currentPrompt;
    }

    // Function to be called when the user clicks the "Continue" button.
    public void PromptAnswered()
    {
        HidePrompt();
        if(promptAnsweredEvent != null)
        {
            promptAnsweredEvent();
        }
    }

    // Disables the prompt GUI.
    private void HidePrompt()
    {
        promptUI.SetActive(false);
    }
}