namespace Social_Media.Models;

public class Comment
{
    public int CommentID { get; set; }
    public int UserID { get; set; }
    public int PostID { get; set; }
    public string Content { get; set; }
}