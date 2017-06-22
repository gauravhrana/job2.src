IF OBJECT_ID ('dbo.SuperKeyDetail') IS NOT NULL
	DROP TABLE dbo.SuperKeyDetail
GO

CREATE TABLE dbo.SuperKeyDetail 
(
		SuperKeyDetailId	INT		IDENTITY(1, 1)	NOT NULL
	,   ApplicationId		INT						NOT NULL 
	,	SuperKeyId			INT						NOT NULL 
	,	EntityKey			INT						NOT NULL
);

