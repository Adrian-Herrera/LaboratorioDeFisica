using System.Collections;
using System;
using System.Collections.Generic;
using WebSocketSharp;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class WsClient : MonoBehaviour
{
    public static WsClient Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] private Button _create, _join, _leave, _sala;
    [SerializeField] private TMP_Text _serverMessage;
    [SerializeField] private TMP_InputField _roomNumber;
    private bool _msgFlag = false, _roomFlag = false, _startFlag = false;
    private Coroutine _searchRoom;
    private Room _room;
    private string _serverMsg;
    public WsCommand _wsCommand;
    WebSocket ws;
    public void Init()
    {
        ws = new WebSocket("ws://localhost:8080");

        ws.OnOpen += (sender, e) =>
        {
            string msg = "{ \"Type\": \"SignIn\", \"Params\": { \"Nombre\": \"" + CredentialManager.Current.UserInfo.Nombre + "\", \"Id\": \"" + CredentialManager.Current.JwtCredential.userId + "\", \"Type\": \"3\" } }";
            ws.Send(msg);
        };
        ws.OnMessage += (sender, e) =>
        {
            string message = "Message Received from " + ((WebSocket)sender).Url + ", Data : " + e.Data;
            Debug.Log(message);
            _wsCommand = JsonUtility.FromJson<WsCommand>(e.Data);
            if (_wsCommand.Type == "CheckRooms")
            {
                _serverMsg = "Buscando sala";
                _roomFlag = true;
            }
            if (_wsCommand.Type == "Start")
            {
                _serverMsg = "Buscando sala";
                _startFlag = true;
            }
            _msgFlag = true;
        };
        ws.Connect();
        // _create.onClick.AddListener(Create);
        _sala.onClick.AddListener(Join);
        // _leave.onClick.AddListener(Leave);
        StartSearchRoom();

    }
    private void Update()
    {
        if (ws == null)
        {
            return;
        }
        if (_msgFlag)
        {
            // _serverMessage.text = _serverMsg;
            _msgFlag = false;
        }
        if (_roomFlag)
        {
            Debug.Log(_wsCommand.Params.Room.Id);
            _room = _wsCommand.Params.Room;
            _sala.GetComponentInChildren<TMP_Text>().text = _room.Id.ToString();
            Debug.Log(_room.Quiz);
            Debug.Log("---");
            StopCoroutine(_searchRoom);
            _roomFlag = false;
        }
        if (_startFlag)
        {
            QuizManager.Current.StartQuiz();
            _startFlag = false;
        }
    }
    private void StartSearchRoom()
    {
        _searchRoom = StartCoroutine(SearchRoom());
    }
    private IEnumerator SearchRoom()
    {
        WsCommand command = new()
        {
            Type = "CheckRooms"
        };
        while (true)
        {
            yield return new WaitForSeconds(2);
            ws.Send(JsonUtility.ToJson(command));
        }
    }
    private void SignIn()
    {

    }
    private void Join()
    {
        // Debug.Log(_roomNumber.text);
        WsCommand command = new();
        WsParams _params = new();
        command.Type = "Join";
        _params.Code = _room.Id;
        command.Params = _params;
        Debug.Log(JsonUtility.ToJson(command));
        ws.Send(JsonUtility.ToJson(command));
        LevelManager.Instance.ChargeOnlineQuiz(_room.Quiz);
    }
    private void Leave()
    {
        WsCommand command = new();
        command.Type = "Leave";
        ws.Send(JsonUtility.ToJson(command));
        // ws.Send("{ \"type\": \"leave\" }");
    }
    public void UpdateScore(int score)
    {
        // var command = new { Type = "Score", Params = new { Score = score } };
        WsCommand command = new();
        WsParams _params = new();
        command.Type = "Score";
        _params.Score = score;
        _params.Code = _room.Id;
        command.Params = _params;
        Debug.Log(JsonUtility.ToJson(command));
        ws.Send(JsonUtility.ToJson(command));
    }
    [Serializable]
    public class WsCommand
    {
        public string Type;
        public WsParams Params;
    }
    [Serializable]
    public class WsParams
    {
        public string Code;
        public Room Room;
        public int Score;
    }
    [Serializable]
    public class Room
    {
        public string Id;
        public Cuestionario Quiz;
    }
}
