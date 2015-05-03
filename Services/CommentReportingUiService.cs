using MrCMS.Services;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Web.Apps.Commenting.Events;
using MrCMS.Web.Apps.Commenting.Models;
using NHibernate;

namespace MrCMS.Web.Apps.Commenting.Services
{
    public class CommentReportingUiService : ICommentReportingUiService
    {
        private readonly ISession _session;

        public CommentReportingUiService(ISession session)
        {
            _session = session;
        }

        public ReportResponse Report(ReportModel reportModel)
        {
            var comment = _session.Get<Comment>(reportModel.CommentId);

            if (comment == null)
            {
                return new ReportResponse
                       {
                           Type = CommentResponseType.Error,
                           Message = "Could not find comment to report, it may have already been deleted"
                       };
            }

            EventContext.Instance.Publish<IOnCommentReported, CommentReportedEventArgs>(
                new CommentReportedEventArgs(comment));

            return new ReportResponse
                   {
                       Type = CommentResponseType.Info,
                       Message = "The comment has been reported, and will be dealt with by site admin",
                       RedirectUrl = "~/"+ comment.Webpage.LiveUrlSegment
                   };
        }
    }
}