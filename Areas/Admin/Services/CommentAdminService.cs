using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using MrCMS.Helpers;
using MrCMS.Paging;
using MrCMS.Web.Apps.Commenting.Areas.Admin.Models;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Web.Apps.Commenting.Entities.UserProfile;
using NHibernate;
using NHibernate.Criterion;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.Services
{
    public class CommentAdminService : ICommentAdminService
    {
        private readonly ISession _session;

        public CommentAdminService(ISession session)
        {
            _session = session;
        }

        public IPagedList<Comment> Search(CommentSearchQuery query)
        {
            var queryOver = _session.QueryOver<Comment>();

            switch (query.ApprovalStatus)
            {
                case ApprovalStatus.Pending:
                    queryOver = queryOver.Where(comment => comment.Approved == null);
                    break;
                case ApprovalStatus.Rejected:
                    queryOver = queryOver.Where(comment => comment.Approved == false);
                    break;
                case ApprovalStatus.Approved:
                    queryOver = queryOver.Where(comment => comment.Approved == true);
                    break;
            }
            if (!string.IsNullOrWhiteSpace(query.Email))
                queryOver = queryOver.Where(comment => comment.Email.IsLike(query.Email, MatchMode.Anywhere));
            if (!string.IsNullOrWhiteSpace(query.Message))
                queryOver = queryOver.Where(comment => comment.Message.IsLike(query.Message, MatchMode.Anywhere));
            if (query.DateFrom.HasValue)
                queryOver = queryOver.Where(comment => comment.CreatedOn >= query.DateFrom);
            if (query.DateTo.HasValue)
                queryOver = queryOver.Where(comment => comment.CreatedOn < query.DateTo);
            if (query.Id.HasValue)
                queryOver = queryOver.Where(comment => comment.Id == query.Id);

            return queryOver.OrderBy(comment => comment.CreatedOn).Desc.Paged(query.Page);
        }

        public List<SelectListItem> GetApprovalOptions()
        {
            return Enum.GetValues(typeof(ApprovalStatus))
                .Cast<ApprovalStatus>()
                .BuildSelectItemList(status => status.ToString(), emptyItem: null);
        }

        public void Update(Comment comment)
        {
            _session.Transact(session => session.Update(comment));
        }

        public void Delete(Comment comment)
        {
            _session.Transact(session => session.Delete(comment));
        }
    }
}