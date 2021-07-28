using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{

    public static UIController instance;
    public GameObject deathScreen;
    public TMP_Text deathText;

    private void Awake()
    {
        instance = this;
    }
}
