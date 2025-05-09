using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStorySceneShort", menuName = "Data/New Story Scene Short")]
[System.Serializable]
public class dialogStroy : ScriptableObject
{
    public List<Sentence> sentences;
    [System.Serializable]
    public struct Sentence
    {
        [TextAreaAttribute] public string text;
        public string speaker;
        public Sprite speakerimg;
    }
}
