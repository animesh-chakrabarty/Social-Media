namespace Social_Media.Models;
public class UserFeed
{
    public IEnumerable<Post> Posts { get; set; }
    public IEnumerable<Like> Likes { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
}

