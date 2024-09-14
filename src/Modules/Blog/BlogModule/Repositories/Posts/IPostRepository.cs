using BlogModule.Context;
using BlogModule.Domain;
using Common.Domain.Repository;
using Common.Infrastructure.Repository;

namespace BlogModule.Repositories.Posts;

internal interface IPostRepository:IBaseRepository<Post>
{

}

internal class PostRepository(BlogContext context):BaseRepository<Post, BlogContext>(context), IPostRepository;