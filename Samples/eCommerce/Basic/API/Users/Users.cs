using Cratis.Applications.ModelBinding;

namespace API.Users;

/// <summary>
/// Represents the users API.
/// </summary>
[Route("/api/users")]
public class Users : ControllerBase
{
    /// <summary>
    /// Change the profile picture for a user.
    /// </summary>
    /// <param name="changeProfilePicture"><see cref="ChangeProfilePicture"/> command.</param>
    /// <returns>Awaitable task.</returns>
    [HttpPost("{id}/change-profile-picture")]
    public Task ChangeProfilePicture([FromRequest] ChangeProfilePicture changeProfilePicture)
    {
        Console.WriteLine(changeProfilePicture);
        return Task.CompletedTask;
    }
}
