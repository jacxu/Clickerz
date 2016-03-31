using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FirePowerRender : MonoBehaviour {


	public Image power1;
	public Image power2;
	public Image power3;
	public Image power5;

	private ClickController cc;

	void Start()
	{
		cc = MyController.cc;
		power5.fillAmount = power3.fillAmount = power2.fillAmount = power1.fillAmount = 0;
	}
	// Update is called once per frame
	void Update () {
		switch (cc.FirePower) {
		case 5:
			power5.fillAmount = cc.SetFirepowerRender ();
			Debug.Log ("fireppower" + cc.SetFirepowerRender ());
			break;
		case 3:
			power5.fillAmount = 1;
			power3.fillAmount = cc.SetFirepowerRender ();
			break;
		case 2:
			power5.fillAmount = 1;
			power3.fillAmount = 1;
			power2.fillAmount = cc.SetFirepowerRender ();
			break;
		case 1:
			power5.fillAmount = 1;
			power3.fillAmount = 1;
			power2.fillAmount = 1;
			power1.fillAmount = cc.SetFirepowerRender ();
			break;

		}
		
	
	}
}

