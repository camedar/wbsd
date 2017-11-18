CREATE TABLE [dbo].[Table]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
    [firstname] VARCHAR(20) NOT NULL, 
    [surname] VARCHAR(20) NULL, 
    [username] VARCHAR(15) NOT NULL, 
    [email] VARCHAR(25) NOT NULL, 
    [password] VARCHAR(15) NOT NULL
)
