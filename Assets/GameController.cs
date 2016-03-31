using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{


    public GameObject world1;
    public GameObject world2;

	public Transform worldpos1;
	public Transform worldpos2;
		
	private List<GameObject> worldZ;
    public GameObject sideHighlight;

    public GameObject title;
    public TextMesh titleCount;

    public Transform titleEndPos;


    private bool intro, newgame, endgame, activegame;
	private float waitTime = 0.6f;

	private GameObject attakWorld;
	public GameObject AttackWorld
	{
		get { return attakWorld; }
	}

    private GameObject activeWorld;
    public GameObject ActiveWorld
    {
        get { return activeWorld; }
    }


    private bool _debugshot = false;
    public bool DebugShot
    {
        get { return _debugshot; }
    }





    IEnumerator CountStart()
    {
        
		SetActiveWorldz(true);
		StartCoroutine(RandomizeWorldPos(2));
        yield return new WaitForSeconds(waitTime);
        titleCount.text = "3";
		StartCoroutine(RandomizeWorldPos(5));
        yield return new WaitForSeconds(waitTime);
        titleCount.text = "2";
		StartCoroutine(RandomizeWorldPos(10));
        yield return new WaitForSeconds(waitTime);
        titleCount.text = "1";

        yield return new WaitForSeconds(waitTime);
        titleCount.text = "";
        SetActiveWorldz(true);
		activegame = true;
        

    }

	IEnumerator RandomizeWorldPos(int num)
	{
		//time interval 0.6f
		for (float i = 0; i <= waitTime; i = i + (float)(waitTime / num)) {
			yield return new WaitForSeconds((float)(waitTime / num));
			Debug.Log ("rr:" + Random.Range (0, 6));
			SetWorldzPos (Random.Range (0, 6));
		}
	}


	private void SetWorldzPos(int val)
	{
		if (val > 2) {
			world1.transform.position = worldpos1.position;
			world2.transform.position = worldpos2.position;
		} else {
			world1.transform.position = worldpos2.position;
			world2.transform.position = worldpos1.position;

		}
	}
    void RemoveIntro()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("intro"))
        {
            obj.SetActive(false);
        }
    }

    void SetActiveWorldz(bool val)
    {
        world1.SetActive(val);
        world2.SetActive(val);
    }


    void _debugFirstShot()
    {

        newgame = endgame = intro = false;
        RemoveIntro();
        SetActiveWorldz(true);
        SetNewGame(world1);
        title.transform.position = titleEndPos.position;
        title.transform.localScale = titleEndPos.localScale;
        activegame = true;
        _debugshot = false;
    }

    // Use this for initialization
    void Start()
    {
        SetActiveWorldz(false);
        titleCount.text = "";
		worldZ = new List<GameObject> ();
		worldZ.Add (world1);
		worldZ.Add (world2);
        intro = true;
        newgame = endgame = activegame = false;

    }

    void WarzNewGame()
    {
        RemoveIntro();
        title.transform.position = Vector3.Slerp(title.transform.position, titleEndPos.position, 2f * Time.deltaTime);
        title.transform.localScale = Vector3.Slerp(title.transform.localScale, titleEndPos.localScale, 2f * Time.deltaTime);
        if (title.transform.position.y > 4.90f)
        {
            StartCoroutine(CountStart());

        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
		
		if (newgame && !activegame)
            WarzNewGame();
		if (activegame)
		 	CheckWin ();


    }
	void CheckWin()
	{
		foreach(GameObject obj in worldZ)
		{
			//Debug.Log(obj.name + " h: "+obj.GetComponent<ClickController>().Health);
			if (obj.GetComponent<ClickController>().Health >195){
				activegame = false;
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

    public void SetNewGame(GameObject obj)
    {
        activeWorld = obj;
		attakWorld = SetAttackWorld();
        newgame = true;
        intro = endgame = false;
    }

	GameObject SetAttackWorld()
	{
		foreach (GameObject obj in worldZ)
			if (!obj.Equals(activeWorld))
				return obj;
		return null;
	}
}


