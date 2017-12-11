using MrCMS.Helpers;
using MrCMS.Paging;
using MrCMS.Web.Apps.Commenting.Areas.Admin.Controllers;
using MrCMS.Web.Apps.Commenting.Areas.Admin.Models;
using MrCMS.Web.Apps.Commenting.Entities;
using NHibernate;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.Services
{
    public class ReportedCommentAdminService : IReportedCommentAdminService
    {
        private readonly ISession _session;

        public ReportedCommentAdminService(ISession session)
        {
            _session = session;
        }

        public IPagedList<ReportedComment> Search(ReportedCommentSearchQuery query)
        {
            var reportedComments = _session.QueryOver<ReportedComment>();
            return reportedComments.OrderBy(x => x.Id).Desc.Paged(query.Page);
        }
    }
}