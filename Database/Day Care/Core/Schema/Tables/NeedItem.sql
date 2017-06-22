IF OBJECT_Id ('dbo.NeedItem') IS NOT NULL
PRINT 'Dropping Table NeedItem'
   DROP TABLE dbo.NeedItem
GO
PRINT 'Creating Table NeedItem'
CREATE TABLE dbo.NeedItem 
(
		NeedItemId  INT           NOT NULL
    ,	Name        VARCHAR (50)  NOT NULL
    ,	Description VARCHAR (500) NOT NULL
    ,	SortOrder   INT           NOT NULL
)
GO

