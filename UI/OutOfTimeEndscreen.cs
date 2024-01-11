using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HexKeyGames
{
    public class OutOfTimeEndscreen : MonoBehaviour
    {
        [SerializeField]
        private RawImage rawImage;

        [SerializeField]
        private List<Texture> textures = new();

        private void Awake()
        {
            if (rawImage == null) rawImage = GetComponentInChildren<RawImage>();
            rawImage.texture = textures.RandomElement();
        }
    }
}
