using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core;
using Metflix.Core.Models;

namespace VideoDemos.Views.Notifications;

public partial class ChatPage : ContentPage
{
    private Grid _selectedChat;

    public ChatPage()
    {
        ChatUserModel testModel1 = new ChatUserModel("Kawazaki", "Серіал мені дуже сподобався, не...", "ava_test.png",
            "11:33", false);
        ChatUserModel testModel2 =
            new ChatUserModel("Cago", "Серіал мені дуже сподобався, не...", "ava_test.png", "10:45", true);
        ChatUserModel testModel3 =
            new ChatUserModel("Krico", "Серіал мені дуже сподобався, не...", "ava_test.png", "Пн", false);

        ChatMessageModel messageTestModel1 = new ChatMessageModel(false, DateTime.Now,
            "Звичайний серіал. Він не шедевр, але і не зовсім вже сміття. Чи вартий він того хайпу, який трапився? Ні. Це явно не \"Гра престолів\" від Південної Кореї. І тим не менше саме для Південної Кореї серіал непоганий. Якщо краще опрацюють сюжетну лінію, головних і другорядних героїв, то у серіалу є шанс стати дуже хорошим у другому сезоні. Це справді диво, що він вистрілив. Особисто для мене 6 з 10.\nІ так, люди тут мають рацію. Тупості в серіалі дійсно багато.");
        ChatMessageModel messageTestModel2 = new ChatMessageModel(true, DateTime.Now,
            "Звичайний серіал. Він не шедевр, але і не зовсім вже сміття. Чи вартий він того хайпу, який трапився? Ні. ");
        InitializeComponent();
        ChatFactory.ChatSelectedEvent = ChatSelected;

        UsersContainer.Add(ChatFactory.CreateChat(testModel1));
        UsersContainer.Add(ChatFactory.CreateChat(testModel2));
        UsersContainer.Add(ChatFactory.CreateChat(testModel3));
        UsersContainer.Add(ChatFactory.CreateChat(testModel3));
        UsersContainer.Add(ChatFactory.CreateChat(testModel3));
        UsersContainer.Add(ChatFactory.CreateChat(testModel3));
        UsersContainer.Add(ChatFactory.CreateChat(testModel3));
        UsersContainer.Add(ChatFactory.CreateChat(testModel3));
        UsersContainer.Add(ChatFactory.CreateChat(testModel3));
        UsersContainer.Add(ChatFactory.CreateChat(testModel3));

        CommentsLayout.Add(ChatFactory.CreateMessage(messageTestModel1));
        CommentsLayout.Add(ChatFactory.CreateMessage(messageTestModel2));
        CommentsLayout.Add(ChatFactory.CreateMessage(messageTestModel2));
    }

    public void ChatSelected(object sender, EventArgs e)
    {
        if (_selectedChat != null) _selectedChat.BackgroundColor = Colors.Transparent;
        Grid container = (Grid)((Button)sender).Parent;
        _selectedChat = container;
        container.BackgroundColor = Color.FromArgb("#111111");
        CurrentDialogNickname.Text = "Krico";
        CurrentDialogLastSeen.Text = "Зараз в мережі";
        CurrentDialogAvatar.Source = "ava_test.png";
    }


    private async void NotificationsButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"/{nameof(NotificationsPage)}");
    }
}