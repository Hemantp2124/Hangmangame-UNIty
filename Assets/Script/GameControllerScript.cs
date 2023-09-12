using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class GameControllerScript : MonoBehaviour
{

  
   
    public Text timefield;
    public GameObject[] hangMan;
    public GameObject Wintext;
    public GameObject loseText;
    public GameObject replayButton;
    public Text wordToFindfield;
    private float time;

   // private string[] wordLocal = { "Matt", "joanne", "robert", "marry jane" };
    private string[] Words = File.ReadAllLines(@"Assets/Words.txt");
    private string chosenWord;
    private string hiddenWord;
    private int fails;
    private bool gameEnd = false;
    // Start is called before the first frame update
    void Start()
    {

        //chosenWord = Words[Random.Range(0, Words.Length)];
        chosenWord = "hemant";
        for (int i = 0; i < chosenWord.Length; i++)
        {
            char letter = chosenWord[i];
            if (char.IsWhiteSpace(letter))
            {
                hiddenWord += " ";
            }
            else
            {
                hiddenWord += "_";
            }

        }
        wordToFindfield.text = hiddenWord;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameEnd == false)
        {
            time += Time.deltaTime;
            timefield.text = time.ToString();
        }
       

       
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if(e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1)
        {
            string pressedLetter = e.keyCode.ToString();
            Debug.Log("keydown event is trigger" + pressedLetter);
            if (chosenWord.Contains(pressedLetter))
            {
                int i = chosenWord.IndexOf(pressedLetter);
                while(i != -1)
                {
                    hiddenWord=hiddenWord.Substring(0,i) + pressedLetter + hiddenWord.Substring(i + 1);
                    Debug.Log(hiddenWord);
                    chosenWord = chosenWord.Substring(0,i)+"_" + chosenWord.Substring(i + 1);
                    Debug.Log(chosenWord);
                    i = chosenWord.IndexOf(pressedLetter);
                }
                wordToFindfield.text = hiddenWord;
            }
            else
            {
                hangMan[fails].SetActive(true);
                fails++;
            }

            if(fails == hangMan.Length)
            {
                loseText.SetActive(true);
                replayButton.SetActive(true);
                gameEnd = true;
            }

            //case won game
            if (!hiddenWord.Contains("_"))
            {
                Wintext.SetActive(true);
                replayButton.SetActive(true);
                gameEnd = true;
            }
        }
    }
}
