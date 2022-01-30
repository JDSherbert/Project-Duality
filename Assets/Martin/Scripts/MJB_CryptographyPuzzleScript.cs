using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Sherbert.Inventory;
using Sherbert.GameplayStatics;
using Sherbert.Application;

public class MJB_CryptographyPuzzleScript : MonoBehaviour
{

    [SerializeField] private List<string> phrases = null;
    [SerializeField] private int shift = 0;
    [SerializeField] private Image characterSelect = null;
    [SerializeField] private Text shiftValueClue = null;
    [SerializeField] private Canvas puzzleCanvas = null;

    private Text[] translations;
    private List<Sherbert.Lexicon.JDH_Rune> foundRunes;
    private string phrase;
    private string encryptedPhrase;
    private int secondsLeft = 120;
    private int pointerIndex = 0;
    private bool stillGoing = true;
    private KeyCode[] alphabetKeys;

    void Start()
    {
        ReadyAndDisableAllClues();
        GetRunes();
        GetPhrase();
        ScanForHumphrey();
        SetupKeyCodes();
        MakeUI();
        StartCoroutine(Timer(GameObject.Find("Timer").GetComponent<Text>()));
    }

    private void ReadyAndDisableAllClues()
    {
        shift = Random.Range(0, 26);
        shiftValueClue.text = "Shift value = " + shift;
        shiftValueClue.enabled = false;
    }

    private void GetRunes()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            foundRunes = Sherbert.GameplayStatics.JDH_GameplayStatics.GetAllRunes();
        }
        else
        {
            foundRunes = new List<Sherbert.Lexicon.JDH_Rune>();
        }
    }

    private void GetPhrase()
    {
        phrase = phrases[Random.Range(0, phrases.Count)].ToUpper();
        encryptedPhrase = EncryptPhrase(phrase);
    }

    private void ScanForHumphrey()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            List<JDH_Item> inventoryList = JDH_GameplayStatics.GetAllItems();
            foreach (JDH_Item item in inventoryList)
            {
                if (!JDH_GameplayStatics.IsTrueNull(item))
                {
                    if (item.ID == "ITEM0000")
                    {
                        secondsLeft += 60;
                    }
                }
            }
        }
    }

    private void SetupKeyCodes()
    {
        alphabetKeys = new KeyCode[26] { KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z }; //Still technically one line
    }

    private string EncryptPhrase(string phraseToBeEncrypted)
    {
        string newPhrase = "";

        for (int i = 0; i < phraseToBeEncrypted.Length; i++)
        {
            newPhrase += GetNewLetter(phraseToBeEncrypted[i]);
        }

        return newPhrase;
    }

    private char GetNewLetter(char letter)
    {
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        if (alphabet.Contains(letter.ToString()))
        {
            int newIndex = alphabet.IndexOf(letter) + shift;
            if (newIndex > 25)
            {
                newIndex -= 26;
            }
            return alphabet[newIndex];
        }
        else
        {
            return letter;
        }
    }

    private void MakeUI()
    {
        GetTranslations();
        ShowClues();
        ShowEncryptedPhrase(GameObject.Find("ToBeDecryptedRunes").GetComponent<Text>());
        CreateDecryptionSpace(GameObject.Find("DecryptionSpace").GetComponent<TextMeshProUGUI>());
    }

    private void GetTranslations()
    {
        translations = GameObject.Find("Translations").GetComponentsInChildren<Text>();
        for (int i = 0; i < translations.Length; i++)
        {
            translations[i].enabled = false;
        }
        for (int i = 0; i < foundRunes.Count; i++)
        {
            for (int j = 0; j < translations.Length; j++)
            {
                if (foundRunes[i].letter.ToString() == translations[j].text)
                {
                    translations[j].enabled = true;
                }
            }
        }
    }

    private void ShowClues()
    {
        if (foundRunes.Count >= 15)
        {
            shiftValueClue.enabled = true;
        }
    }

    private void ShowEncryptedPhrase(Text textForPhrase)
    {
        textForPhrase.text = encryptedPhrase;
    }

    private void CreateDecryptionSpace(TextMeshProUGUI decryptionSpace)
    {
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        char[] characterOrders = new char[phrase.Length];
        for (int i = 0; i < phrase.Length; i++)
        {
            if (alphabet.Contains(phrase[i].ToString()))
            {
                characterOrders[i] = '_';
            }
            else
            {
                characterOrders[i] = phrase[i];
            }
        }
        decryptionSpace.SetText(characterOrders);
    }

    void Update()
    {
        if (stillGoing && puzzleCanvas.enabled)
        {
            UpdateCharacterPointer(GameObject.Find("DecryptionSpace").GetComponent<TextMeshProUGUI>());
            CheckForInputPointerMovement();
            CheckForInputLetterInput();
        }
    }

    private void UpdateCharacterPointer(TextMeshProUGUI decryptionSpace)
    {
        GameObject emptyGO = new GameObject();
        Transform charPosition = emptyGO.transform;
        TMP_CharacterInfo charInfo = decryptionSpace.textInfo.characterInfo[pointerIndex];
        Vector3 bottomRight = charPosition.TransformPoint(charInfo.bottomRight);
        Vector3 topLeft = charPosition.TransformPoint(charInfo.topLeft);
        Vector2 averagePos = new Vector2((bottomRight.x + topLeft.x) / 2f, ((bottomRight.y + topLeft.y) / 2f) + 30);
        characterSelect.rectTransform.anchoredPosition = averagePos;
        Destroy(emptyGO);
    }

    private void CheckForInputPointerMovement()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && pointerIndex > 0)
        {
            pointerIndex--;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && pointerIndex < phrase.Length - 1)
        {
            pointerIndex++;
        }
    }

    private void CheckForInputLetterInput()
    {
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for (int i = 0; i < alphabetKeys.Length; i++)
        {
            if (Input.GetKeyDown(alphabetKeys[i]))
            {
                ReplaceLetterInDecryptionSpace(GameObject.Find("DecryptionSpace").GetComponent<TextMeshProUGUI>(), alphabet[i]);
                CheckForWin(GameObject.Find("DecryptionSpace").GetComponent<TextMeshProUGUI>());
            }
        }
    }

    private void ReplaceLetterInDecryptionSpace(TextMeshProUGUI decryptionSpace, char newLetter)
    {
        string legalReplacementAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ_";
        if (legalReplacementAlphabet.Contains(decryptionSpace.text[pointerIndex].ToString()))
        {
            string newText = "";
            for (int i = 0; i < pointerIndex; i++)
            {
                newText += decryptionSpace.text[i];
            }
            newText += newLetter;
            for (int i = pointerIndex + 1; i < phrase.Length; i++)
            {
                newText += decryptionSpace.text[i];
            }
            decryptionSpace.SetText(newText);
        }
    }

    private void CheckForWin(TextMeshProUGUI decryptionSpace)
    {
        if (decryptionSpace.text == phrase)
        {
            JDH_ApplicationManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            stillGoing = false;
        }
    }

    private IEnumerator Timer(Text timerText)
    {
        while (secondsLeft >= 0)
        {
            timerText.text = (secondsLeft / 60).ToString() + ":";
            if (secondsLeft % 60 < 10)
            {
                timerText.text += '0';
            }
            timerText.text += (secondsLeft % 60).ToString();
            yield return new WaitForSeconds(1);
            if (puzzleCanvas.enabled)
            {
                secondsLeft--;
            }
            if (secondsLeft == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            }
        }
    }
}
