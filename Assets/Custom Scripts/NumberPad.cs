using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class NumberPad : MonoBehaviour
{
    int[] code;
    int[] enteredCode;
    int index;
    GameObject screen;
    TMP_Text text_screen;
    public GameObject keycard;
    AudioProcess audio_process_script;
    // Start is called before the first frame update
    void Start()
    {
        keycard.SetActive(true);
        audio_process_script = GameObject.FindGameObjectWithTag("screenDispenser").GetComponent<AudioProcess>();
        GameObject panel_code = GameObject.FindGameObjectWithTag("code");
        screen = GameObject.FindGameObjectWithTag("screen");
        text_screen = screen.GetComponentInChildren<TMP_Text>();

        string code_s = panel_code.GetComponentInChildren<TextMeshProUGUI>().text;
        code = new int[code_s.Length];
        for(int i=0; i<code_s.Length; i++)
        {
            code[i] = (code_s[i] - '0');
        }
        enteredCode = new int[code.Length];
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(index == code.Length)
        {
            foreach(int a in enteredCode)
            {
                Debug.Log(a);
            }
            if (Enumerable.SequenceEqual<int>(enteredCode, code))
            {
                text_screen.text = "code valid";
                text_screen.color = Color.green;
                keycard.SetActive(true);
                audio_process_script.PlayAudioSucess();
                gameObject.SetActive(false);
            }
            else
            {
                text_screen.text = "invalid code";
                text_screen.color = Color.red;
                audio_process_script.PlayAudioFailure();
            }
            index = 0;
        }
    }

    public void setCode(int n)
    {
        if (index == 0)
        {
            text_screen.text = "";
            text_screen.color = Color.black;
        }
        enteredCode[index] = n;
        text_screen.text += n.ToString();
        index++;
    }
}
