using Post.Common.DTOs;

namespace Posts.Cmd.Api.DTOs;

public class NewPostResponse : BaseResponse
{
    public Guid Id { get; set; }
    
}