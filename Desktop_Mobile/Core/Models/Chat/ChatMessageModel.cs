namespace Metflix.Core.Models;

public class ChatMessageModel
{
    private bool _sendByMe;
    private DateTime _sendTime;
    private string _message;

    public ChatMessageModel(bool sendByMe, DateTime sendTime, string message)
    {
        _sendByMe = sendByMe;
        _sendTime = sendTime;
        _message = message;
    }

    public bool SendByMe
    {
        get => _sendByMe;
        set => _sendByMe = value;
    }

    public DateTime SendTime
    {
        get => _sendTime;
        set => _sendTime = value;
    }

    public string Message
    {
        get => _message;
        set => _message = value;
    }
}