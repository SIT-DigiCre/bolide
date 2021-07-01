using System;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

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
    CancellationTokenSource disposalTokenSource = new CancellationTokenSource();
    ClientWebSocket webSocket = new ClientWebSocket();
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
        this.baseUrl = baseUrl + "api/";
        this.baseWebSocketUri = baseWebSocketUri;
        handler = new HttpClientHandler();
        httpClient = new HttpClient(handler);

    }
    public void StartConnection()
    {
        StartWebSocket();
    }
    /// <summary>
    /// api/loginなどをhttp://a.com/api/loginなどに変換
    /// </summary>
    /// <param name="path">対象のpath</param>
    /// <returns></returns>
    public string GetFullURL(string path)
    {
        if (testMode) return "http://localhost:5000/" + path;
        return baseUrl + "/api/" + path;
    }
    private async void StartWebSocket()
    {
        if (webSocket.State == WebSocketState.Open) return;
        if (testMode) baseWebSocketUri = "ws://localhost:5000/";
        else baseWebSocketUri += "api/";
        string uri = baseWebSocketUri + "v1/room/" + roomName;
        await webSocket.ConnectAsync(new Uri(uri), disposalTokenSource.Token);
        _ = ReceiveLoop();
        if (webSocket.State == WebSocketState.Open)
            ConnectionStartHandler(this, new ConnectionStartArgs(uri));
        if (webSocket.State == WebSocketState.Closed)
            ConnectionErrorHandler(this, new ConnectionErrorArgs(ErorrKind.WebsocketClosed, "CLOSE"));
    }
    private async Task ReceiveLoop()
    {
        var buffer = new ArraySegment<byte>(new byte[1024]);
        while (!disposalTokenSource.IsCancellationRequested)
        {
            try
            {
                var received = await webSocket.ReceiveAsync(buffer, disposalTokenSource.Token);
                var jsonText = System.Text.Encoding.UTF8.GetString(buffer.Array, 0, received.Count);
                var eventArgs = JsonSerializer.Deserialize<CommentEventArgs>(jsonText);
                CommentEventHandler(this, eventArgs);
            }
            catch (Exception e)
            {
                ConnectionErrorHandler(this, new ConnectionErrorArgs(ErorrKind.WebsocketError, e.ToString()));
            }
        }
    }
    public async void PostComment(string text, bool isQuestion)
    {
        string jsonText = JsonSerializer.Serialize(new CommentEventArgs(text, isQuestion));
        var content = new StringContent(jsonText, System.Text.Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(GetFullURL($"v1/comment/{roomName}"), content);
        if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
        {
            ConnectionErrorHandler(this,
                new ConnectionErrorArgs(ErorrKind.WrongRoomID,
                await response.Content.ReadAsStringAsync()));
        }
    }
    public class CommentEventArgs
    {
        [JsonPropertyName("comment")]
        public string comment { get; set; }
        [JsonPropertyName("is_question")]
        public bool isQuestion { get; set; }
        public CommentEventArgs(string comment, bool isQuestion)
        {
            this.comment = comment;
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