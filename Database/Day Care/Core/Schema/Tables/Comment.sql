IF OBJECT_Id ('dbo.Comment') IS NOT NULL
   DROP TABLE dbo.Comment
GO

CREATE TABLE dbo.Comment
(
		CommentId		 INT           NOT NULL
	,	ApplicationId    INT           NOT NULL
	,	StudentId		 INT           NOT NULL
	,	Date			 DATETIME      NOT NULL
	,	EventTypeId		 INT           NOT NULL
	,	Comment			 VARCHAR (500) NOT NULL
)
GO

