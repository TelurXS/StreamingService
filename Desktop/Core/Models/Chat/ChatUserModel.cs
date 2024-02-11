namespace Metflix.Core.Models;

public class ChatUserModel
{
    private string _nickname;
    private string _lastMessage;
    private string _avatarUrl;
    private string _lastMessageTime;
    private string _lastOnlineTime;
    private bool _isReaden;
    private bool _isOnline;
    

    public ChatUserModel(string nickname, string lastMessage, string avatarUrl, string lastMessageTime, bool isReaden)
    {
        this._nickname = nickname;
        _lastMessage = lastMessage;
        _avatarUrl = avatarUrl;
        _lastMessageTime = lastMessageTime;
        _isReaden = isReaden;
        _isOnline = false;
    }

    public string LastOnlineTime
    {
        get => _lastOnlineTime;
        set => _lastOnlineTime = value;
    }

    public bool IsOnline
    {
        get => _isOnline;
        set => _isOnline = value;
    }

    public string Nickname
    {
        get => _nickname;
        set => _nickname = value;
    }

    public string LastMessage
    {
        get => _lastMessage;
        set => _lastMessage = value;
    }

    public string AvatarUrl
    {
        get => _avatarUrl;
        set => _avatarUrl = value;
    }

    public string LastMessageTime
    {
        get => _lastMessageTime;
        set => _lastMessageTime = value;
    }

    public bool IsReaden
    {
        get => _isReaden;
        set => _isReaden = value;
    }
}