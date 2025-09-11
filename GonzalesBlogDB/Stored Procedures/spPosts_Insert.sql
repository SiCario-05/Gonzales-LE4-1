CREATE PROCEDURE dbo.spPosts_Insert
  @title NVARCHAR(200),
  @body NVARCHAR(MAX),
  @userId INT
AS
BEGIN
  SET NOCOUNT ON;

  INSERT INTO dbo.Posts (Title, Body, UserId, DateCreated)
  VALUES (@title, @body, @userId, GETDATE());

  SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewPostId;
END
