namespace FeladatAPI
{
    public record GetBookResponse(Guid BookId, string Title, int PublicationYear, int PageCount, Guid AuthorId);

    public record CreateBookRequest(string Title, int PublicationYear, int PageCount, Guid AuthorId);

    public record ModifyBookRequest(string Title, int PublicationYear, int PageCount);

    public record GetAuthorResponse(Guid AuthorId, string FirstName, string LastName, Guid NationalityId, bool Gender);

    public record CreateAuthorRequest(string FirstName, string LastName, Guid NationalityId, bool Gender);

    public record ModifyAuthorRequest(string FirstName, string LastName, Guid NationalityId, bool Gender);
    public record GetBookWithAuthorResponse(Guid BookId, string Title, int PublicationYear, string AuthorFirstName, string AuthorLastName, string AuthorNationalityName, bool AuthorGender);
}