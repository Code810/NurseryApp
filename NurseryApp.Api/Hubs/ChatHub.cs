using Microsoft.AspNetCore.SignalR;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Api.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatMessageService _chatMessageService;
        private readonly IGroupMessageService _groupMessageService;
        private static readonly Dictionary<string, string> OnlineUsers = new Dictionary<string, string>();

        public ChatHub(IChatMessageService chatMessageService, IGroupMessageService groupMessageService)
        {
            _chatMessageService = chatMessageService;
            _groupMessageService = groupMessageService;
        }

        public async Task SendMessageToUser(string ReceiverAppUserId, string message)
        {
            await _chatMessageService.AddChatMessageAsync(Context.UserIdentifier!, ReceiverAppUserId, message);
            await Clients.User(ReceiverAppUserId).SendAsync("ReceiveMessage", Context.UserIdentifier!, message);
        }

        public async Task SendMessageToGroup(int groupId, string message)
        {
            await _groupMessageService.AddGroupMessageAsync(groupId, message);
            await Clients.Group(groupId.ToString()).SendAsync("ReceiveMessage", message);
        }

        public async Task AddUserToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveUserFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            if (!OnlineUsers.ContainsKey(userId))
            {
                OnlineUsers.Add(userId, Context.ConnectionId);
                await Clients.All.SendAsync("UserConnected", userId);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier;
            if (OnlineUsers.ContainsKey(userId))
            {
                OnlineUsers.Remove(userId);
                await Clients.All.SendAsync("UserDisconnected", userId);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public bool IsUserOnline(string userId)
        {
            return OnlineUsers.ContainsKey(userId);
        }
    }
}
