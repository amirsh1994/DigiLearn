using BlogModule.Context;
using BlogModule.Domain;
using Common.Domain.Repository;
using Common.Infrastructure.Repository;

namespace BlogModule.Repositories.Posts;

internal interface IPostRepository:IBaseRepository<Post>
{
    void Delete(Post post);
}

internal class PostRepository(BlogContext context):BaseRepository<Post, BlogContext>(context), IPostRepository
{
    public void Delete(Post post)
    {
        context.Posts.Remove(post);
    }
}