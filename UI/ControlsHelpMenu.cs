using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexKeyGames
{
    public class ControlsHelpMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] lessHelp, moreHelp;
        private bool more = false;

        private void Awake()
        {
            foreach (var item in moreHelp) item.SetActive(more);
            foreach (var item in lessHelp) item.SetActive(!more);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                more = !more;
                foreach (var item in moreHelp) item.SetActive(more);
                foreach (var item in lessHelp) item.SetActive(!more);
            }
        }
    }
}
