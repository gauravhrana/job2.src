﻿IF OBJECT_ID ('dbo.ReleasePublishCategory') IS NOT NULL
	DROP TABLE dbo.ReleasePublishCategory 
GO


CREATE TABLE dbo.ReleasePublishCategory 
(
		ReleasePublishCategoryId		  INT				NOT NULL
	,	ApplicationId					  INT				NOT NULL
	,	Name							  VARCHAR (50)		NOT NULL
	,	Description						  VARCHAR (500)		NOT NULL
	,	SortOrder						  INT				NOT NULL
);
