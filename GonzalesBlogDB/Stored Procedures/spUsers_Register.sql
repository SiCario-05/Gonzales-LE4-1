CREATE PROCEDURE dbo.spUsers_Register
  @username NVARCHAR(50),
  @password NVARCHAR(100),
  @firstName NVARCHAR(50),
  @lastName NVARCHAR(50)
AS
BEGIN
  SET NOCOUNT ON;

  INSERT INTO dbo.Users (Username, [Password], FirstName, LastName)
  VALUES (@username, @password, @firstName, @lastName);

  SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewUserId;
END
