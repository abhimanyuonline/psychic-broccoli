using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace broccoli.Manager
{

    public class GameManger : MonoBehaviour
    {
        [SerializeField] GameObject MenuScreen;
        [SerializeField] GameObject GameScreen;

        void Start()
        {

        }

        void InitialScreenSetUp()
        {
            MenuScreen.SetActive(true);
            GameScreen.SetActive(false);
        }


    }
}

