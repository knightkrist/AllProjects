using AllProjects.Models;

namespace AllProjects.Interface
{
    public interface IGetStringsInterface
    {
        Task<IEnumerable<Users>> GetUserStrings();
    }
}
