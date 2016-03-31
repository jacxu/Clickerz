using UnityEngine;
using System.Collections;

public class ClickController : MonoBehaviour {

    public GameObject activeSphere;
	public GameObject enemySphere;
    public GameObject fire;
    private GameController gc;

	private float lastClick, lastclickzratedown;

	private int clickzRate;
	public int ClickzRate
	{
		get {return clickzRate; }
		set {
			
			clickzRate = (value < 0) ? 0 : (value > 50) ? 50 : value; 
		}
	}

	private int clickz;
	public int Clickz
	{
		get {return clickz; }
		set {clickz = value; }
	}
		
	private float health;
	public float Health
	{
		set{ health = value;}
		get{ return health;}
	}

	private int firepower;
	public int FirePower
	{
		get{ return firepower;}
		set {firepower = value; }
	}




    private bool IsActive { get { return gc.ActiveWorld.Equals(this.gameObject); } }

    private Vector3 spawnPosition
    {
        get {
			Vector3 ran = Random.insideUnitCircle.normalized;
			return new Vector3(ran.x, ran.y, 0.0f) * (0.5f * transform.localScale.x) + transform.position;
        }
    }


    void Start()
    {
        gc = MyController.GetGameController();
        if (gc.DebugShot)
            CreateFire();
		enemySphere = gc.AttackWorld;
		Health = 100;
		FirePower = 5;
    }
		
    //// Update is called once per frame
    void Update()
    {
		ComputeFirePower ();

        if (IsActive)
        {
            activeSphere.transform.position = transform.position;
            activeSphere.transform.localScale = new Vector3(transform.localScale.x + 0.2f, transform.localScale.y + 0.2f, 0.01f);
        }
			
		transform.localScale = new Vector3 ((float)2.5 * Health/100, (float)2.5 * Health/100, (float)2.5 * Health/100);// transform.localScale * Health/100;
		//Debug.Log ("TIME" + Time.fixedTime);
		if ((Time.fixedTime - lastClick) > 0.1f * FirePower)
			if ((Time.fixedTime - lastclickzratedown) > (0.1f * FirePower)) 
			{
				ClickzRate--;
				lastclickzratedown = Time.fixedTime;
			}

		if (Health < 1)

    }





	void OnTriggerEnter(Collider other) {
		if (other.tag.Contains("fire") && !gc.ActiveWorld.name.Equals(name))
		{
			Destroy (other.gameObject);
			Health-=1f;
			Debug.Log (gc.ActiveWorld.GetComponent<ClickController> ().Health);
			gc.ActiveWorld.GetComponent<ClickController> ().Health += 1f;
		}
	}

    void CreateFire()
    {
        if (IsActive)
        {
			lastClick = Time.fixedTime;
            GameObject newCharacter = Instantiate(fire, spawnPosition, Quaternion.identity) as GameObject;
            newCharacter.transform.LookAt(transform.position);
            newCharacter.transform.Rotate(-90, 0, 0);
//			newCharacter.GetComponent<MoveFire>().
            Debug.Log("click");
        }

    }
    void OnMouseDown()
    {
		lastclickzratedown = Time.fixedTime;
		ClickzRate++;
		Clickz++;
		if (CanFire())
          CreateFire();
		

    }

	void ComputeFirePower()
	{
		//Debug.Log ("cr" + ClickzRate);
		if (ClickzRate > 45)
			FirePower = 1;
		else if (ClickzRate > 40)
			FirePower = 2;
		else if (ClickzRate > 20)
			FirePower = 3;
		else 
			FirePower = 5;
	}

	public float SetFirepowerRender()
	{
		switch (FirePower) {
			case 5:
			return (float)((float)ClickzRate / 20 );
			case 3:
			return (float)(((float)ClickzRate - 20) / 20);
			case 2:
			return (float)(((float)ClickzRate - 40) / 5 );
			case 1:
			return (float)(((float)ClickzRate - 45) / 5 );
		}
		return 0;
	}

	bool CanFire()
	{
		return (Clickz % FirePower*2) == 0;
	}

}
