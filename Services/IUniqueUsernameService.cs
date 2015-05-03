namespace MrCMS.Web.Apps.Commenting.Services
{
    public interface IUniqueUsernameService
    {
        bool IsUniqueUsername(string username, int? id = null);
    }
}