using System;

namespace Metflix.Core.Models;

public class Comment
{
    int _id;
    int _parentCommentId;
    int _senderId;

    string _nickname;
    string _avatarUrl;
    private string _commentText;
    private int _likesAmmount;
    private bool _isLikePressed;
    private DateTime _commentDate;

    public Comment(int id, int parentCommentId, int senderId, string nickname, string avatarUrl, string commentText,
        int likesAmmount, bool isLikePressed, DateTime commentDate)
    {
        this._id = id;
        _parentCommentId = parentCommentId;
        _senderId = senderId;
        this._nickname = nickname;
        _avatarUrl = avatarUrl;
        this._commentText = commentText;
        this._likesAmmount = likesAmmount;
        this._isLikePressed = isLikePressed;
        this._commentDate = commentDate;
    }

    public Comment(string nickname, string avatarUrl, string commentText, int likesAmmount, DateTime commentDate)
    {
        _nickname = nickname;
        _avatarUrl = avatarUrl;
        _commentDate = commentDate;
        _commentText = commentText;
        _likesAmmount = likesAmmount;
    }

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public int ParentCommentId
    {
        get => _parentCommentId;
        set => _parentCommentId = value;
    }

    public int SenderId
    {
        get => _senderId;
        set => _senderId = value;
    }

    public string Nickname
    {
        get => _nickname;
        set => _nickname = value;
    }

    public string AvatarUrl
    {
        get => _avatarUrl;
        set => _avatarUrl = value;
    }

    public string CommentText
    {
        get => _commentText;
        set => _commentText = value;
    }

    public int LikesAmmount
    {
        get => _likesAmmount;
        set => _likesAmmount = value;
    }

    public bool IsLikePressed
    {
        get => _isLikePressed;
        set => _isLikePressed = value;
    }

    public DateTime CommentDate
    {
        get => _commentDate;
        set => _commentDate = value;
    }
}