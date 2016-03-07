using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider m_sliderComponent;

	// Use this for initialization
	void Awake ()
    {
        m_sliderComponent = GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_sliderComponent.value = Bomb.instance.GetProgress();
	}
}
