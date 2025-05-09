using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Matsuri
{
    //Default on team but change on type card, some can be overrided on each individual card

    [CreateAssetMenu(fileName = "SkinforWorld", menuName = "Matsuri/SkinCharacter", order = 0)]
    public class ImageAsset : ScriptableObject
    {
        public string id;

        [Header("Image")]
        public Sprite body;
        public Sprite lhand;
        public Sprite rhand;
        public Sprite lpant;
        public Sprite rpant;
        public Sprite head;

        public Sprite GetBody()
        {
            return body;
        }

        public Sprite GetLeftHand()
        {
            return lhand;
        }

        public Sprite GetRightHand()
        {
            return rhand;
        }
        public Sprite GetLefttLeg()
        {
            return lpant;
        }
        public Sprite GetRightLeg()
        {
            return rpant;
        }

        public Sprite GetHead()
        {
            return head;
        }
    }
}