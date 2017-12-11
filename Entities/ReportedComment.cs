﻿using MrCMS.Entities;
using MrCMS.Entities.People;

namespace MrCMS.Web.Apps.Commenting.Entities
{
    public class ReportedComment : SiteEntity
    {
        public virtual Comment Comment { get; set; }
        public virtual User User { get; set; }
        public virtual string IPAddress { get; set; }
    }
}