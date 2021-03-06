using UnityEngine;
using System.Collections;
using System.Net;

public class Sequence : MonoBehaviour
{

	private Mode m_mode;

	public const int IPNumInAdrList = 0; 
	private string serverAddress;

	private HostType hostType;

	private const int m_port = 50765;

	private TransportTCP m_transport = null;

	private int m_counter = 0;

	public GUITexture bgTexture;
	public GUITexture pushTexture;

	private static float WINDOW_WIDTH = 640.0f;
	private static float WINDOW_HEIGHT = 480.0f;

	enum Mode
	{
		SelectHost = 0,
		Connection,
		Game,
		Disconnection,
		Error,
	};

	enum HostType
	{
		None = 0,
		Server,
		Client,
	};


	void Awake()
	{
		m_mode = Mode.SelectHost;
		hostType = HostType.None;
		serverAddress = "";

		// Network 클래스의 컴포넌트 취득.
		GameObject obj = new GameObject("Network");
		m_transport = obj.AddComponent<TransportTCP>();
		DontDestroyOnLoad(obj);

		// 호스트명을 가져옵니다.
		string hostname = Dns.GetHostName();
		// 호스트명에서 IP주소를 가져옵니다.
		IPAddress[] adrList = Dns.GetHostAddresses(hostname);
		for(int i=0; i<adrList.Length;i++)
        {
			Debug.Log("adrList " + i + ":" + adrList[i].ToString());
        }
		//serverAddress = adrList[IPNumInAdrList].ToString();
		serverAddress = "127.0.0.1";
	}

	void Update()
	{

		switch (m_mode)
		{
			case Mode.SelectHost:
				OnUpdateSelectHost();
				break;

			case Mode.Connection:
				OnUpdateConnection();
				break;

			case Mode.Game:
				OnUpdateGame();
				break;

			case Mode.Disconnection:
				OnUpdateDisconnection();
				break;
		}

		++m_counter;
	}

	//
	void OnGUI()
	{
		switch (m_mode)
		{
			case Mode.SelectHost:
				OnGUISelectHost(); // 
				break;

			case Mode.Connection:
				OnGUIConnection();
				break;

			case Mode.Game:
				break;

			case Mode.Disconnection:
				break;

			case Mode.Error:
				OnGUICError();
				break;
		}
	}


	// Sever 또는 Client 선택화면
	void OnUpdateSelectHost()
	{

		switch (hostType)
		{
			case HostType.Server:
				{
					bool ret = m_transport.StartServer(m_port, 1);
					m_mode = ret ? Mode.Connection : Mode.Error;
				}
				break;

			case HostType.Client:
				{
					bool ret = m_transport.Connect(serverAddress, m_port);
					m_mode = ret ? Mode.Connection : Mode.Error;
				}
				break;

			default:
				break;
		}
	}

	void OnUpdateConnection()
	{
		if (m_transport.IsConnected() == true)
		{
			m_mode = Mode.Game;

			GameObject game = GameObject.Find("Monopoly");
			game.GetComponent<Monopoly>().GameStart();
		}
	}

	void OnUpdateGame()
	{
		GameObject game = GameObject.Find("Monopoly");

		if (game.GetComponent<Monopoly>().IsGameOver() == true)
		{
			m_mode = Mode.Disconnection;
		}
	}


	void OnUpdateDisconnection()
	{
		switch (hostType)
		{
			case HostType.Server:
				m_transport.StopServer();
				break;

			case HostType.Client:
				m_transport.Disconnect();
				break;

			default:
				break;
		}

		m_mode = Mode.SelectHost;
		hostType = HostType.None;
		//serverAddress = "";
		// 호스트명을 가져옵니다.
		string hostname = Dns.GetHostName();
		// 호스트명에서 IP 주소를 가져옵니다.
		IPAddress[] adrList = Dns.GetHostAddresses(hostname);
		serverAddress = adrList[5].ToString();
	}


	void OnGUISelectHost()
	{
		// 배경 표시.
		DrawBg(true);

		if (GUI.Button(new Rect(Screen.width/2-150.0f/2, Screen.height/4, 150, 20), "대전 상대를 기다립니다"))
		{
			hostType = HostType.Server;
		}

		// 클라이언트를 선택했을 때 접속할 서버 주소를 입력합니다.
		if (GUI.Button(new Rect(Screen.width/2-150.0f/2, Screen.height*2.0f/4, 150, 20), "대전 상대와 접속합니다"))
		{
			hostType = HostType.Client;
		}

		Rect labelRect = new Rect(Screen.width/2-100, Screen.height*2.5f/4, 200, 30);
		GUIStyle style = new GUIStyle();
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.white;
		GUI.Label(labelRect, "상대방 IP 주소", style);
		labelRect.y -= 2;
		style.fontStyle = FontStyle.Normal;
		style.normal.textColor = Color.black;
		GUI.Label(labelRect, "상대방 IP 주소", style);

		serverAddress = GUI.TextField(new Rect(Screen.width/2-100, Screen.height*2.7f/4 , 200, 20), serverAddress);
	}


	void OnGUIConnection()
	{
		// 배경 표시.
		DrawBg(false);

		// 클라이언트를 선택했을 때 접속할 서버 주소를 입력합니다.
		if(GUI.Button(new Rect(Screen.width / 2 - 150.0f / 2, Screen.height / 4, 150, 20), "기다리지 않으려면 클릭"))
        {
			m_mode = Mode.SelectHost;
			hostType = HostType.None;
			///////////////
			m_transport.StopServer();
		}
	}

	void OnGUICError()
	{
		// 배경 표시.
		DrawBg(false);

		float px = Screen.width * 0.5f - 150.0f;
		float py = Screen.height * 0.5f;

		if (GUI.Button(new Rect(px, py, 300, 80), "접속할 수 없습니다.\n\n버튼을 누르세요"))
		{
			m_mode = Mode.SelectHost;
			hostType = HostType.None;
		}
	}

	// 배경 표시.
	void DrawBg(bool blink)
	{
		// 배경을 그립니다.
		Rect bgRect = new Rect(Screen.width / 2 - Screen.width * 0.5f,
							 Screen.height / 2 - Screen.height * 0.5f,
							 Screen.width,
							 Screen.height);
		Graphics.DrawTexture(bgRect, bgTexture.texture);
	}
}
