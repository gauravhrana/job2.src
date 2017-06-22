IF OBJECT_Id ('dbo.Needs') IS NOT NULL
PRINT 'Dropping Table Needs'
   DROP TABLE dbo.Needs
GO
PRINT 'Creating Table Needs'
CREATE TABLE dbo.Needs 
(
		NeedsId        INT          NOT NULL
	,	ApplicationId  INT			NOT NULL
    ,	StudentId      INT          NOT NULL
    ,	RequestDate    DATETIME     NULL
    ,	ReceivedDate   DATETIME     NULL
    ,	NeedItemId     INT          NOT NULL
    ,	NeedItemStatus VARCHAR (50) NULL
    ,	NeedItemBy     DATETIME     NULL
)
GO

