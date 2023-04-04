using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider volumeSlider;
    public TMP_Text value;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        value.text = "" + volumeSlider.value;
    }
}
