IF OBJECT_ID ('dbo.FileType') IS NOT NULL
	DROP TABLE dbo.FileType
GO

CREATE TABLE dbo.FileType 
(
		FileTypeId		INT          NOT NULL	
	,	ApplicationId	INT			 NOT NULL
    ,	Name			VARCHAR (50) NOT NULL	
    ,	Description		VARCHAR (50) NOT NULL	
    ,	SortOrder		INT          NOT NULL
);

