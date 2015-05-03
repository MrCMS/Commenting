using System.Collections.Generic;
using MrCMS.Entities.Messaging;
using MrCMS.Messages;
using MrCMS.Services;
using MrCMS.Web.Apps.Commenting.Entities;
using NHibernate;

namespace MrCMS.Web.Apps.Commenting.MessageTemplates
{
    public class NewCommentReplyMessageTemplate : MessageTemplate<Comment>
    {
    }
}