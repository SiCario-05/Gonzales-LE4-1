CREATE PROCEDURE dbo.spPosts_List
AS
BEGIN
  SET NOCOUNT ON;

  SELECT
      p.Id,
      p.Title,
      u.Username,
      p.DateCreated,
      LEFT(p.Body, 20) AS BodyPreview
  FROM dbo.Posts p
  INNER JOIN dbo.Users u ON u.Id = p.UserId
  ORDER BY p.DateCreated DESC, p.Id DESC;
END
