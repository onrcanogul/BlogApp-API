using BlogApp.Core.Models;
using BlogApp.Core.Repositories;

namespace BlogApp.Repository.Repositories
{
    public class UserRefreshTokenRepository : GenericRepository<UserRefreshToken> , IUserRefreshTokenRepository
    {
        public UserRefreshTokenRepository(AppDbContext context) : base(context)
        {
        }

        public void RemoveByToken(UserRefreshToken entity )
        {
            _context.UserRefreshTokens.Remove(entity);
        }
    }
}
