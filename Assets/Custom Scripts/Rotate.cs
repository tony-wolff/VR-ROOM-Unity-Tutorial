using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotate : MonoBehaviour
{
    public GameObject pedestal;
    public Slider rotation_slider;
    // Start is called before the first frame update

    public void rotate()
    {
        pedestal.transform.rotation = Quaternion.Euler(new Vector3(0, rotation_slider.value, 0));
    }
}
