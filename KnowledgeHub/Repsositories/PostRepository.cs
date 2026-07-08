using Dapper;
using KnowledgeHub.Data;
using KnowledgeHub.Models;

namespace KnowledgeHub.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DbConnectionFactory _db;

        public PostRepository(DbConnectionFactory db)
        {
            _db = db;
        }

        //========================
        // Get All Posts
        //========================
        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            using var connection = _db.CreateConnection();

            await connection.OpenAsync();

            return await connection.QueryAsync<Post>(
                @"SELECT *
                  FROM posts
                  ORDER BY created_at DESC");
        }

        //========================
        // Get Post By Id
        //========================
        public async Task<Post?> GetByIdAsync(Guid id)
        {
            using var connection = _db.CreateConnection();

            await connection.OpenAsync();

            return await connection.QueryFirstOrDefaultAsync<Post>(
                @"SELECT *
                  FROM posts
                  WHERE id=@Id",
                new { Id = id });
        }

        //========================
        // Create
        //========================
        public async Task<int> AddAsync(Post post)
        {
            using var connection = _db.CreateConnection();

            await connection.OpenAsync();

            var sql = @"

INSERT INTO posts
(
    id,
    title,
    slug,
    short_description,
    description,
    image_url,
    category_id,
    tags,
    is_published,
    created_at
)

VALUES
(
    @Id,
    @Title,
    @Slug,
    @ShortDescription,
    @Description,
    @ImageUrl,
    @CategoryId,
    @Tags,
    @IsPublished,
    NOW()
)

";

            if (post.Id == Guid.Empty)
                post.Id = Guid.NewGuid();

            return await connection.ExecuteAsync(sql, post);
        }

        //========================
        // Update
        //========================
        public async Task<int> UpdateAsync(Post post)
        {
            using var connection = _db.CreateConnection();

            await connection.OpenAsync();

            var sql = @"

UPDATE posts

SET

title=@Title,

slug=@Slug,

short_description=@ShortDescription,

description=@Description,

image_url=@ImageUrl,

category_id=@CategoryId,

tags=@Tags,

is_published=@IsPublished,

updated_at=NOW()

WHERE id=@Id

";

            return await connection.ExecuteAsync(sql, post);
        }

        //========================
        // Delete
        //========================
        public async Task<int> DeleteAsync(Guid id)
        {
            using var connection = _db.CreateConnection();

            await connection.OpenAsync();

            return await connection.ExecuteAsync(
                @"DELETE FROM posts
                  WHERE id=@Id",
                new { Id = id });
        }
    }
}