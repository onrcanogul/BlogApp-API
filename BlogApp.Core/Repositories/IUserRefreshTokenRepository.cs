using BlogApp.Core.Models;

namespace BlogApp.Core.Repositories
{
    public interface IUserRefreshTokenRepository : IGenericRepository<UserRefreshToken>
    {
        public void RemoveByToken(UserRefreshToken entity);
    }
}
