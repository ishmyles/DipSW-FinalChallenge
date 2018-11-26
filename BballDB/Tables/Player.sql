CREATE TABLE [dbo].[Player]
(
	[ID] NVARCHAR(10) NOT NULL,
	[FirstName] NVARCHAR(20),
	[LastName] NVARCHAR(20),
	[PendingApproval] INT NULL,
	CONSTRAINT PK_Player PRIMARY KEY (ID)
)
