using System.Collections.Generic;

namespace Metflix.Core.Models;

public class ChatModel
{
    private List<ChatMessageModel> _messages;

    public ChatModel(List<ChatMessageModel> messages)
    {
        _messages = messages;
    }

    public List<ChatMessageModel> Messages
    {
        get => _messages;
        set => _messages = value;
    }
}