using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MrCMS.Apps;
using MrCMS.Web.Apps.Commenting.Areas.Admin.Controllers;
using MrCMS.Web.Apps.Commenting.Controllers;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Web.Apps.Commenting.Services;
using NHibernate;
using Ninject;

namespace MrCMS.Web.Apps.Commenting
{
    public class CommentingApp : MrCMSApp
    { 
        protected override void RegisterApp(MrCMSAppRegistrationContext context)
        {
            context.MapAreaRoute("Commenting admin controllers", "Admin", "Admin/Apps/Commenting/{controller}/{action}/{id}",
                       new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                       new[] { typeof(CommentingSettingsController).Namespace });

            context.MapRoute("Add Comments Form", "comments/add/{id}",
                         new { controller = "Comments", action = "Add" },
                         new[] { typeof(CommentsController).Namespace });

            context.MapRoute("Show Comments", "comments/Show/{id}",
                             new { controller = "Comments", action = "Show" },
                             new[] { typeof(CommentsController).Namespace });

            context.MapRoute("Guest Comment", "comments/Post/Guest",
                             new { controller = "Comments", action = "Guest" },
                             new[] { typeof(CommentsController).Namespace });

            context.MapRoute("Logged in Comment", "comments/Post/User",
                             new { controller = "Comments", action = "LoggedIn" },
                             new[] { typeof(CommentsController).Namespace });

            context.MapRoute("Comment - Upvote", "comments/upvote",
                             new { controller = "CommentVoting", action = "Upvote" },
                             new[] { typeof(CommentVotingController).Namespace });

            context.MapRoute("Comment - Downvote", "comments/downvote",
                             new { controller = "CommentVoting", action = "Downvote" },
                             new[] { typeof(CommentVotingController).Namespace });

            context.MapRoute("Comment - Report", "comments/report",
                             new { controller = "CommentReporting", action = "Report" },
                             new[] { typeof(CommentReportingController).Namespace });

            context.MapRoute("Unique Username Check", "comments/username-check",
                new { controller = "CommentingUserAccount", action = "IsUniqueUsername" },
                new[] { typeof(CommentingUserAccountController).Namespace });

            context.MapRoute("Update Username", "comments/update-username",
                new {controller = "CommentingUserAccount", action = "UpdateUsername"},
                new[] {typeof (CommentingUserAccountController).Namespace});
        }

        public override string AppName
        {
            get { return "Commenting"; }
        }

        public override string Version
        {
            get { return "1.0"; }
        }

        protected override void RegisterServices(IKernel kernel)
        {
        }
    }
}