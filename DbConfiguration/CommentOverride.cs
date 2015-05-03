using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using MrCMS.DbConfiguration;
using MrCMS.Web.Apps.Commenting.Entities;

namespace MrCMS.Web.Apps.Commenting.DbConfiguration
{
    public class CommentOverride : IAutoMappingOverride<Comment>
    {
        public void Override(AutoMapping<Comment> mapping)
        {
            mapping.Map(comment => comment.Message).MakeVarCharMax();
            mapping.HasMany(comment => comment.Children).KeyColumn("InReplyToId");
        }
    }
}