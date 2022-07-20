using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//TODO names should be possible to be changed during single dialogue (a dialogue between two persons e.g. protagonist and some NPC)
//TODO along with name there should be an image tied to this name
//TODO image should be tied to a name by default, but there should be possibility to change this image (displaying emotions or state of character by changing image to)
//TODO instead of passing string through monoBehaviour, they should be taken from a text file (or json file, so that i can pass image references easily), for easier editing


public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    //TODO public Image characterImage;

    public Animator animator;


    private Queue<string> sentences;
    private bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        //animator.Play("DialogueWindow_Open");
        animator.SetBool("isOpen", true);
        isRunning = true;


        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }

    }
    private void EndDialogue()
    {
        //animator.Play("DialogueWindow_Close");
        animator.SetBool("isOpen", false);
        isRunning = false;
    }

    public bool isDialogueInProgress()
    {
        return isRunning;
    }
   
}
