using System;
using System.Net;
using System.Net.Http;
using WebSocketSharp;
using WebSocketSharp.Net;
using System.Text.Json;
using System.Text.Json.Serialization;



public class Connection
{
    public enum ErorrKind
    {
        WrongRoomID, WebsocketClosed, WebsocketError
    }
    public event EventHandler<CommentEventArgs> CommentEventHandler;
    public event EventHandler<ConnectionErrorArgs> ConnectionErrorHandler;
    public event EventHandler<ConnectionStartArgs> ConnectionStartHandler;
    bool testMode;
    string baseUrl;
    string baseWebSocketUri;
    string roomName;
    HttpClientHandler handler;
    HttpClient httpClient;
    WebSocket webSocket;
    /// <summary>
    /// サーバーとの通信を担うCommetConnectionクラスコンストラクタ
    /// </summary>
    /// <param name="testMode">trueでローカルサーバを使用</param>
    /// <param name="baseUrl">本番環境URL</param>
    /// <param name="baseWebSocketUri">本番環境WebSocketURI</param>
    public Connection(bool testMode, string roomName, string baseUrl, string baseWebSocketUri)
    {
        this.testMode = testMode;
        this.roomName = roomName;
        this.baseUrl = baseUrl;
        this.baseWebSocketUri = baseWebSocketUri;
        handler = new HttpClientHandler()
        {
            UseCookies = false,
        };
        httpClient = new HttpClient(handler);
    }
    /// <summary>
    /// api/loginなどをhttp://a.com/api/loginなどに変換
    /// </summary>
    /// <param name="path">対象のpath</param>
    /// <returns></returns>
    public string GetFullURL(string path)
    {
        if (testMode) return "http://localhost:8000/" + path;
        return baseUrl + "/" + path;
    }
    private void StartWebSocket()
    {
        if (testMode) baseWebSocketUri = "ws://localhost:8000/";
        string uri = baseWebSocketUri + roomName;
        webSocket = new WebSocket(uri);
        webSocket.OnMessage += (sender, e) =>
        {
            string jsonText = e.Data.ToString();
            var eventArgs = JsonSerializer.Deserialize<CommentEventArgs>(jsonText);
            CommentEventHandler(this, eventArgs);
        };
        webSocket.OnClose += (sender, e) =>
        {
            ConnectionErrorHandler(this, new ConnectionErrorArgs(ErorrKind.WebsocketClosed, e.Reason));
        };
        webSocket.OnError += (sender, e) =>
        {
            ConnectionErrorHandler(this, new ConnectionErrorArgs(ErorrKind.WebsocketError, e.Message));
        };
        webSocket.OnOpen += (sender, e) =>
        {
            ConnectionStartHandler(this, new ConnectionStartArgs(uri));
        };

    }
    public class CommentEventArgs
    {
        [JsonPropertyName("text")]
        public string text { get; set; }
        [JsonPropertyName("is_question")]
        public bool isQuestion { get; set; }
        public CommentEventArgs(string text, bool isQuestion)
        {
            this.text = text;
            this.isQuestion = isQuestion;
        }
    }
    public class ConnectionErrorArgs
    {
        public ErorrKind erorrKind { get; set; }
        public string text { get; set; }
        public ConnectionErrorArgs(ErorrKind erorrKind, string text)
        {
            this.erorrKind = erorrKind;
            this.text = text;
        }
    }
    public class ConnectionStartArgs
    {
        public string uri { get; set; }
        public ConnectionStartArgs(string uri)
        {
            this.uri = uri;
        }
    }
}