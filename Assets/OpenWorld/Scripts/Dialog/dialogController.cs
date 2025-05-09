using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class dialogController : MonoBehaviour
{
    public static dialogController instance;
    public TextMeshProUGUI barText;
    public TextMeshProUGUI personNameText;
    public GameObject loopFinishObject;
    public GameObject spritesPrefab;

    private int sentenceIndex = -1;
    [SerializeField] private float speedFactor = 1f;
    private bool IsdialogPlay = false;
    private dialogStroy currentScene;
    private Coroutine typingCoroutine;

    private State state = State.COMPLETED;
    private enum State
    {
        PLAYING, SPEEDED_UP, COMPLETED
    }
    // Start is called before the first frame update
    void Start()
    {
        loopFinishObject.SetActive(false);
    }

    public int GetSentenceIndex()
    {
        return sentenceIndex;
    }

    public void ClearText()
    {
        barText.text = "";
        personNameText.text = "";
    }
    public void PlayScene(dialogStroy scene, int sentenceIndex = -1)
    {
        currentScene = scene;
        this.sentenceIndex = sentenceIndex;
        PlayNextSentence();
    }

    public void PlayNextSentence()
    {
        loopFinishObject.SetActive(false);
        sentenceIndex++;
        PlaySentence();
    }

    public bool IsdialogPlaystate()
    {
        return IsdialogPlay;
    }

    public void IsStart()
    {
        IsdialogPlay = true;
    }
    public void IsStop()
    {
        IsdialogPlay = false;
    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED || state == State.SPEEDED_UP;
    }

    public bool IsLastSentence()
    {
        return sentenceIndex + 1 == currentScene.sentences.Count;
    }

    public bool IsFirstSentence()
    {
        return sentenceIndex == 0;
    }

    public void SpeedUp()
    {
        state = State.SPEEDED_UP;
        speedFactor = 0.01f;
    }

    public void StopTyping()
    {
        state = State.COMPLETED;
        loopFinishObject.SetActive(false);
        StopCoroutine(typingCoroutine);
    }

    private void PlaySentence()
    {
        dialogStroy.Sentence sentence = currentScene.sentences[sentenceIndex];
        speedFactor = 1f;
        typingCoroutine = StartCoroutine(TypeText(sentence.text));
        personNameText.text = sentence.speaker;
        spritesPrefab.GetComponent<Image>().sprite = sentence.speakerimg;
    }

    private IEnumerator TypeText(string text)
    {
        barText.text = "";
        state = State.PLAYING;
        int wordIndex = 0;

        while (state != State.COMPLETED)
        {
            barText.text += text[wordIndex];
            yield return new WaitForSeconds(speedFactor * 0.05f);
            if (++wordIndex == text.Length)
            {
                loopFinishObject.SetActive(true);
                state = State.COMPLETED;
                break;
            }
        }
    }
}
