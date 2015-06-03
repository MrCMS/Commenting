using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MrCMS.Helpers;
using MrCMS.Settings;
using MrCMS.Web.Apps.Commenting.Entities;

namespace MrCMS.Web.Apps.Commenting.Settings
{
    public class CommentingSettings : SiteSettingsBase
    {
        public CommentingSettings()
        {
            CommentApprovalType = CommentApprovalType.Guest;
            //InitialNumberOfCommentsToShow = 10;
            AllowVoting = true;
            NotifyCommentAddedEmail = string.Empty;

            MaxUINestingDepth = 3;
        }

        [DisplayName("Allow guest comments?")]
        public bool AllowGuestComments { get; set; }

        [DisplayName("Type of comments that require approval")]
        public CommentApprovalType CommentApprovalType { get; set; }

        [DisplayName("Allow voting?")]
        public bool AllowVoting { get; set; }

        [DisplayName("Notify comments posted email(s)")]
        public string NotifyCommentAddedEmail { get; set; }

        //[DisplayName("Initial number of comments to show")]
        //public int InitialNumberOfCommentsToShow { get; set; }

        public string AllowedPageTypes { get; set; }

        public IEnumerable<string> EmailsToNotify
        {
            get
            {
                if (string.IsNullOrWhiteSpace(NotifyCommentAddedEmail))
                    yield break;
                foreach (
                    string email in NotifyCommentAddedEmail.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                    yield return email;
            }
        }

        public virtual IEnumerable<Type> AllowedTypes
        {
            get
            {
                if (string.IsNullOrWhiteSpace(AllowedPageTypes))
                    yield break;
                string[] types = AllowedPageTypes.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string type in types)
                {
                    yield return TypeHelper.GetTypeByName(type);
                }
            }
        }

        [DisplayName("Maximum UI Nesting Depth")]
        public int MaxUINestingDepth { get; set; }

        public CommentingSettings SetAllowedPageTypes(params Type[] types)
        {
            AllowedPageTypes = string.Join(",", types.Select(type => type.FullName));
            return this;
        }
    }
}