using Conference.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conference
{
    public class CommentHub : Hub
    {
        public void AddComment(int sessionId, string content)
        {
            var context = new ConferenceContext();
            context.Comments.Add(new Comment { SessionId = sessionId, Content = content });
            context.SaveChanges();

            Clients.Group(sessionId.ToString()).AddNewComment(content);
        }

        public void Register(int sessionId)
        {
            Groups.Add(Context.ConnectionId, sessionId.ToString());
        }
    }
}