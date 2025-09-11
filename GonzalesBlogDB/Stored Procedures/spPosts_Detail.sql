CREATE PROCEDURE dbo.spPosts_Detail
  @postId INT
AS
BEGIN
  SET NOCOUNT ON;

  SELECT
      p.Id,
      p.Title,
      p.Body,
      p.UserId,
      p.DateCreated,
      u.Username,
      u.FirstName,
      u.LastName
  FROM dbo.Posts p
  INNER JOIN dbo.Users u ON u.Id = p.UserId
  WHERE p.Id = @postId;
END
