using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//UI 스크립트를 따로 만들어서 GameObject.Find("UI_Setting").GetComponent<UI_Setting_S>().Buy_UI();로 꺼내쓸 수 있게 

public class UI_Setting_S : MonoBehaviour
{

	int stop_land_number;

	float state_time;
	bool state;
	public bool HighPassPoint;
	public bool HighPassOK;
	GameObject UI_Ob;
	GameObject Player_1;
	[SerializeField]
	private Sprite[] Buy_images;
	public Image Buy_Image;
	public Image Festival_Image;
	public Image Golend_Key_Image;
	public Image Accident_Image;
	bool g;
	public Monopoly monopoly;
	public Image Victory_Image;
	private IEnumerator accident_cor;
	void Start()
	{
		UI_Ob = GameObject.Find("UI");
		Player_1 = GameObject.Find("Car1");
		state_time = 0f;
		HighPassPoint = false;
		g = false;
		monopoly = GameObject.Find("Monopoly").GetComponent<Monopoly>();

		Debug.Log("시작");
		Money_position(1);
	}

	void Update()
	{
		if (state)
		{
			State();
		}

	}

	//땅에 도착했을 때 UI 등장까지 잠시 state
	void State()
	{
		state_time += Time.deltaTime;
	}

	/*
	//구매 UI
	public void Normal_Land_Buy_UI()
	{

		//state_time = 0f;
		state = true;
		if (state_time > 1f)
		{
			Debug.Log("In_Buy");
			stop_land_number = Player_1.GetComponent<Player_S>().stop_land_number;
			Buy_Image.sprite = Buy_images[stop_land_number];
			UI_Ob.transform.GetChild(0).gameObject.SetActive(true);
			state = false;
		}
	}
	*/
	public void Normal_Land_Buy_UI()
	{
		//Debug.Log("NLB");
		Monopoly monopoly = GameObject.Find("Monopoly").GetComponent<Monopoly>();
		Buy_Image.sprite = Buy_images[monopoly.turnPlayerScript.position];
		UI_Ob.transform.GetChild(0).gameObject.SetActive(true);
	}

	public void Festival_Land_Buy_UI()
	{
		//state_time = 0f;

		Debug.Log("In_Festival");
		Monopoly monopoly = GameObject.Find("Monopoly").GetComponent<Monopoly>();
		Buy_Image.sprite = Buy_images[monopoly.turnPlayerScript.position];
		UI_Ob.transform.GetChild(1).gameObject.SetActive(true);
	}

	public void Normal_Land_Buy_UI_Close()
	{
		UI_Ob.transform.GetChild(0).gameObject.SetActive(false);
	}

	public void Festival_Land_Buy_UI_Close()
	{
		UI_Ob.transform.GetChild(1).gameObject.SetActive(false);
	}
	//구매UI X버튼 눌렀을 때
	/*
	public void Normal_Buy_Button_X()
    {
		state = false;
		state_time = 0f;
		//집 버튼 눌렀을 때 체크 버튼 false로, 인수할 때도 마찬가지로
		GameObject.Find("house_1").transform.GetChild(0).gameObject.SetActive(false);
		GameObject.Find("house_2").transform.GetChild(0).gameObject.SetActive(false);
		GameObject.Find("house_3").transform.GetChild(0).gameObject.SetActive(false);

		//Buy_Image false
		UI_Ob.transform.GetChild(0).gameObject.SetActive(false);
		Player_1.GetComponent<Player_S>().UI_Buy_bool = false;

	}
	*/
	public void Normal_Buy_Button_X()
	{

		//집 버튼 눌렀을 때 체크 버튼 false로, 인수할 때도 마찬가지로
		GameObject.Find("house_1").transform.GetChild(0).gameObject.SetActive(false);
		GameObject.Find("house_2").transform.GetChild(0).gameObject.SetActive(false);
		GameObject.Find("house_3").transform.GetChild(0).gameObject.SetActive(false);

		//Buy_Image false
		UI_Ob.transform.GetChild(0).gameObject.SetActive(false);

	}

	public void Festival_Buy_Button_X()
	{
		state = false;
		state_time = 0f;
		UI_Ob.transform.GetChild(1).gameObject.SetActive(false);
		Player_1.GetComponent<Player_S>().UI_Buy_bool = false;

	}


	public void Accident_Button_X()
	{
		state = false;
		state_time = 0f;
		UI_Ob.transform.GetChild(3).gameObject.SetActive(false);
		Player_1.GetComponent<Player_S>().UI_Buy_bool = false;

	}
	public void HighPass_Button()
	{
		state = false;
		state_time = 0f;
		UI_Ob.transform.GetChild(5).gameObject.SetActive(false);
		HighPassPoint = true;

	}

	public void Take_Button_Yes()
	{
		state = false;
		state_time = 0f;
		UI_Ob.transform.GetChild(7).gameObject.SetActive(false);

	}
	public void Take_Button_No()
	{
		state = false;
		state_time = 0f;
		UI_Ob.transform.GetChild(7).gameObject.SetActive(false);

	}
	//구매 UI에서 집 클리깃 체크표시


	public void Start_UI()
	{

	}

	public void Accident_UI() //8번 지역에 도착하면 차가 달려와서 교통사고 나고 layer 바뀌어서 조명 8번에만 비추고 UI 출력
	{
		//if(!accident)
		accident_cor = Accident_cor();
		Debug.Log("코루틴");
		StartCoroutine(accident_cor);

	}

	IEnumerator Accident_cor()
	{

		Debug.Log("코루틴!");
		GameObject Accident_car = GameObject.Find("Accident_car_parent").transform.GetChild(0).gameObject;
		Accident_car.SetActive(true);

		while (true)
		{

			if (Accident_car.transform.position == Player_1.transform.position)
			{
				for (int i = 0; i < 3; i++)
				{
					Accident_car.SetActive(false);
					Child_Layer_Chage(GameObject.Find("Map_Obj").transform.GetChild(i), 8);

				}
				GameObject.Find("8").gameObject.layer = 0;
				//UI_Ob.transform.GetChild(3).gameObject.SetActive(true);
				Accident_car.transform.position = new Vector3(-22, 0, -7);
				Accident_car.SetActive(false);
				yield break;
			}
			else
			{
				Accident_car.transform.position = Vector3.MoveTowards(Accident_car.transform.position, Player_1.transform.position, Time.deltaTime * 30f);
			}
			yield return new WaitForSecondsRealtime(0.0f);
		}
	}

	IEnumerator Fade(int sec, GameObject go)
	{
		for (int i = 0; i < sec; i++)
		{
			yield return new WaitForSeconds(1);
		}
		go.SetActive(false);
	}

	public void Accident_Notice()
	{
		Debug.Log("Accident_UI");
		GameObject accidentUI = GameObject.Find("UI").transform.Find("Accident_UI").gameObject;

		accidentUI.SetActive(true);
		accidentUI.transform.Find("Text").GetComponent<Text>().text = "" + monopoly.turnPlayerScript.isolatedCount;


		StartCoroutine(Fade(2, accidentUI));
	}

	void Child_Layer_Chage(Transform parent, int layer_number) //자식 오브젝트의 모든 layer 변경
	{
		parent.gameObject.layer = layer_number;
		foreach (Transform child in parent)
		{
			Child_Layer_Chage(child, layer_number);
		}
	}

	public void Increase_UI()
	{

	}
	public void HighPass_UI()
	{
		state = true;
		if (state_time > 0.5f)
		{
			for (int i = 0; i < 3; i++)
			{
				Child_Layer_Chage(GameObject.Find("Map_Obj").transform.GetChild(i), 8);

			}
			GameObject.Find("24").gameObject.layer = 0;
			UI_Ob.transform.GetChild(5).gameObject.SetActive(true);
		}
	}

	public void High_Pass_Point()
	{
		if (HighPassPoint == true && Input.GetMouseButtonDown(0)) //선택한 지역 이동
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{

				Debug.Log(hit.transform.gameObject.name);
				hit.transform.gameObject.layer = 0;
				Player_1.GetComponent<Player_S>().stop_land_number = int.Parse(hit.transform.gameObject.name);
				HighPassPoint = false;



				for (int i = 0; i < 3; i++)
				{
					Child_Layer_Chage(GameObject.Find("Map_Obj").transform.GetChild(i), 0);
				}


			}
		}
	}
	/*
	public void GoldenKey_UI()
	{
		state = true;
		if (state_time > 1f)
		{
			Debug.Log("In_GolendKey");
			int card_number = Random.Range(32, 37);
			stop_land_number = Player_1.GetComponent<Player_S>().stop_land_number;
			Golend_Key_Image.sprite = Buy_images[stop_land_number];
			UI_Ob.transform.GetChild(2).gameObject.SetActive(true);
			state = false;
		}
	}
	*/

	public void GoldenKey_UI()
	{
		UI_Ob.transform.GetChild(2).gameObject.SetActive(true);
	}
	public void GoldenKey_UI_Close()
	{
		UI_Ob.transform.GetChild(2).gameObject.SetActive(false);
	}
	public void GoldenKey_Result_UI(Monopoly.GoldKeyType goldKeyType)
	{
		if (goldKeyType == Monopoly.GoldKeyType.Airport)
		{
			UI_Ob.transform.Find("Airport").gameObject.SetActive(true);
		}
		else if (goldKeyType == Monopoly.GoldKeyType.ForcedSell)
		{
			UI_Ob.transform.Find("ForcedSell").gameObject.SetActive(true);
		}
		else if (goldKeyType == Monopoly.GoldKeyType.FreePass)
		{
			UI_Ob.transform.Find("FreePass").gameObject.SetActive(true);
		}
		else if (goldKeyType == Monopoly.GoldKeyType.Isolated)
		{
			UI_Ob.transform.Find("Isolated").gameObject.SetActive(true);
		}
		else if (goldKeyType == Monopoly.GoldKeyType.Lotto)
		{
			UI_Ob.transform.Find("Lotto").gameObject.SetActive(true);
		}
		else if (goldKeyType == Monopoly.GoldKeyType.Olympic)
		{
			UI_Ob.transform.Find("Olympic").gameObject.SetActive(true);
		}


	}
	public void GoldenKey_Result_UI_Close(Monopoly.GoldKeyType goldKeyType)
	{
		if (goldKeyType == Monopoly.GoldKeyType.Airport)
		{
			UI_Ob.transform.Find("Airport").gameObject.SetActive(false);
		}
		else if (goldKeyType == Monopoly.GoldKeyType.ForcedSell)
		{
			UI_Ob.transform.Find("ForcedSell").gameObject.SetActive(false);
		}
		else if (goldKeyType == Monopoly.GoldKeyType.FreePass)
		{
			UI_Ob.transform.Find("FreePass").gameObject.SetActive(false);
		}
		else if (goldKeyType == Monopoly.GoldKeyType.Isolated)
		{
			UI_Ob.transform.Find("Isolated").gameObject.SetActive(false);
		}
		else if (goldKeyType == Monopoly.GoldKeyType.Lotto)
		{
			UI_Ob.transform.Find("Lotto").gameObject.SetActive(false);
		}
		else if (goldKeyType == Monopoly.GoldKeyType.Olympic)
		{
			UI_Ob.transform.Find("Olympic").gameObject.SetActive(false);
		}
	}

	public void Goldeney_UI_Clcik()
	{
		Debug.Log("Random_card");
		int card_number = Random.Range(32, 37);
		Debug.Log(card_number);
		Golend_Key_Image.sprite = Buy_images[card_number];
		//Golend_Key_Image.sprite =

	}

	public void FreePass_UI()
	{
		UI_Ob.transform.Find("FreePass_UI").gameObject.SetActive(true);
	}

	public void FreePass_UI_Close()
	{
		UI_Ob.transform.Find("FreePass_UI").gameObject.SetActive(false);
	}

	public void Acquisit_UI()
	{
		UI_Ob.transform.Find("Take_UI").gameObject.SetActive(true);
	}

	public void Acquisit_UI_Close()
	{
		UI_Ob.transform.Find("Take_UI").gameObject.SetActive(false);
	}
	public void Victory(int n)
	{
		AudioClip clip = GameObject.Find("ExplosionSound").GetComponent<AudioSource>().clip;
		GameObject.Find("ExplosionSound").GetComponent<AudioSource>().PlayOneShot(clip);

		GameObject.Find("Directional Light").GetComponent<Light>().intensity = 0.1f;
		GameObject.Find("End").transform.GetChild(0).gameObject.SetActive(true);
		if (n == 0) //파산승리
		{
			Victory_Image.sprite = Buy_images[38];
		}
		else if (n == 1) //계절독점승리
		{
			Victory_Image.sprite = Buy_images[39];
		}
		else if (n == 2) //전국여행승리
		{
			Victory_Image.sprite = Buy_images[40];
		}
		else if(n== 3)
        {
			Victory_Image.sprite = Buy_images[42];
		}
		else if (n == 4) //턴승리
		{
			Victory_Image.sprite = Buy_images[41];
			/*
			if(monopoly.winner == Monopoly.Winner.Player1 || monopoly.winner == Monopoly.Winner.Player2)
            {
				Victory_Image.sprite = Buy_images[41];
				if( (monopoly.winner == Monopoly.Winner.Player1 && monopoly.localPlayer == Monopoly.PlayerType.Player2) ||
					(monopoly.winner == Monopoly.Winner.Player2 && monopoly.localPlayer == Monopoly.PlayerType.Player1)
                    )
                {
					GameObject.Find("End").transform.GetChild(0).gameObject.SetActive(false);
				}
			}
            else
            {
				GameObject.Find("End").transform.GetChild(0).gameObject.SetActive(false);
			}
			*/
		}
		Fireworks_On();
	}
	public void Fireworks_On()
	{
		for (int i = 0; i < 11; i++)
		{
			GameObject.Find("Fireworks").gameObject.transform.GetChild(i).gameObject.SetActive(true);
		}
	}
	public void Fireworks_Off()
	{
		for (int i = 0; i < 11; i++)
		{
			GameObject.Find("Fireworks").gameObject.transform.GetChild(i).gameObject.SetActive(false);
		}
	}

	//돈
	public void Money_position(int p) // 1 = 구매(내 위치->가운데) , 2 = 인수(내 위치-> 상대 위치) ,3 = (가운데 -> 내 위치) , 4 = 통행료(가운데 -> 상대)
	{

		if (p == 1)
		{
			StartCoroutine(Money_Cor(1));

		}
		else if (p == 2)  //자리에 알맞게 Money_position(2)넣어야함
		{
			StartCoroutine(Money_Cor(2));
		}
		else if (p == 3) ////자리에 알맞게 Money_position(3)넣어야함
		{
			StartCoroutine(Money_Cor(3));
		}
		else if (p == 4) ////자리에 알맞게 Money_position(3)넣어야함
		{
			StartCoroutine(Money_Cor(4));
		}
	}
	IEnumerator Money_Cor(int p)
	{
		GameObject Money = GameObject.Find("Money_p").transform.GetChild(0).gameObject;
		//GameObject take_obj = GameObject.Find("Player_Window").transform.GetChild(1).gameObject;
		//아래쪽 기준
		GameObject Player1 = GameObject.Find("Player1").gameObject;
		GameObject Player2 = GameObject.Find("Player2").gameObject;
		GameObject Center = GameObject.Find("Center").gameObject;
		if (monopoly.turnPlayerScript.playerType == Monopoly.PlayerType.Player1) //플레이어 1에서 2로
		{
			Money.gameObject.SetActive(true);

			if (p == 1)
			{
				Money.transform.position = Player1.transform.position;
				while (true)
				{
					Money.transform.position = Vector3.MoveTowards(Money.transform.position, Center.transform.position, Time.deltaTime * 10f);
					if (Money.transform.position == Center.transform.position)
					{
						Money.gameObject.SetActive(false);
						yield break;

					}
					yield return new WaitForSecondsRealtime(0.001f);
				}
			}
			else if (p == 2)
			{
				Money.transform.position = Player1.transform.position;
				while (true)
				{
					Money.transform.position = Vector3.MoveTowards(Money.transform.position, Player2.transform.position, Time.deltaTime * 10f);
					if (Money.transform.position == Player2.transform.position)
					{
						Money.gameObject.SetActive(false);
						yield break;

					}
					yield return new WaitForSecondsRealtime(0.001f);
				}


			}
			else if (p == 3)
			{
				Money.transform.position = Center.transform.position;
				while (true)
				{
					Money.transform.position = Vector3.MoveTowards(Money.transform.position, Player1.transform.position, Time.deltaTime * 10f);
					if (Money.transform.position == Player1.transform.position)
					{
						Money.gameObject.SetActive(false);
						yield break;

					}
					yield return new WaitForSecondsRealtime(0.001f);
				}
			}
			else if (p == 4)
			{
				Money.transform.position = Center.transform.position;
				while (true)
				{
					Money.transform.position = Vector3.MoveTowards(Money.transform.position, Player2.transform.position, Time.deltaTime * 10f);
					if (Money.transform.position == Player2.transform.position)
					{
						Money.gameObject.SetActive(false);
						yield break;

					}
					yield return new WaitForSecondsRealtime(0.001f);
				}
			}
		}
        else if(monopoly.turnPlayerScript.playerType == Monopoly.PlayerType.Player2)
        {
			Money.gameObject.SetActive(true);

			if (p == 1)
			{
				Money.transform.position = Player2.transform.position;
				while (true)
				{
					Money.transform.position = Vector3.MoveTowards(Money.transform.position, Center.transform.position, Time.deltaTime * 10f);
					if (Money.transform.position == Center.transform.position)
					{
						Money.gameObject.SetActive(false);
						yield break;

					}
					yield return new WaitForSecondsRealtime(0.001f);
				}
			}
			else if (p == 2)
			{
				Money.transform.position = Player2.transform.position;
				while (true)
				{
					Money.transform.position = Vector3.MoveTowards(Money.transform.position, Player1.transform.position, Time.deltaTime * 10f);
					if (Money.transform.position == Player1.transform.position)
					{
						Money.gameObject.SetActive(false);
						yield break;

					}
					yield return new WaitForSecondsRealtime(0.001f);
				}


			}
			else if (p == 3)
			{
				Money.transform.position = Center.transform.position;
				while (true)
				{
					Money.transform.position = Vector3.MoveTowards(Money.transform.position, Player2.transform.position, Time.deltaTime * 10f);
					if (Money.transform.position == Player2.transform.position)
					{
						Money.gameObject.SetActive(false);
						yield break;

					}
					yield return new WaitForSecondsRealtime(0.001f);
				}
			}
			else if (p == 4)
			{
				Money.transform.position = Center.transform.position;
				while (true)
				{
					Money.transform.position = Vector3.MoveTowards(Money.transform.position, Player1.transform.position, Time.deltaTime * 10f);
					if (Money.transform.position == Player1.transform.position)
					{
						Money.gameObject.SetActive(false);
						yield break;

					}
					yield return new WaitForSecondsRealtime(0.001f);
				}
			}
		}
	}

	public void Season_Effect(int land_number)
	{
		if (state == false)
		{
			if (land_number == 3)
			{
				GameObject.Find("SeasonEffect").transform.GetChild(0).gameObject.SetActive(true);
			}
			else if (land_number == 12)
			{
				GameObject.Find("SeasonEffect").transform.GetChild(1).gameObject.SetActive(true);
			}
			else if (land_number == 23)
			{
				GameObject.Find("SeasonEffect").transform.GetChild(2).gameObject.SetActive(true);
			}
			else if (land_number == 28)
			{
				GameObject.Find("SeasonEffect").transform.GetChild(3).gameObject.SetActive(true);
			}
			StartCoroutine(Delay_2(land_number));
		}


		if (state == true)
		{
			if (land_number == 3)
			{
				GameObject.Find("SeasonEffect").transform.GetChild(0).gameObject.SetActive(false);
			}
			else if (land_number == 12)
			{
				GameObject.Find("SeasonEffect").transform.GetChild(1).gameObject.SetActive(false);
			}
			else if (land_number == 23)
			{
				GameObject.Find("SeasonEffect").transform.GetChild(2).gameObject.SetActive(false);
			}
			else if (land_number == 28)
			{
				GameObject.Find("SeasonEffect").transform.GetChild(3).gameObject.SetActive(false);
			}
			Debug.Log("3f");
		}
	}
	IEnumerator Delay_2(int land_number) //seasondeffect
	{

		yield return new WaitForSeconds(3f); // 해당 시간동안 기다림
		Debug.Log("코루틴");
		state = true;
		Season_Effect(land_number);
	}

}
