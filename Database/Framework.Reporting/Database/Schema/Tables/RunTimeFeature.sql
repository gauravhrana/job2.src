IF OBJECT_ID ('dbo.RunTimeFeature ') IS NOT NULL
	DROP TABLE dbo.RunTimeFeature 
GO


CREATE TABLE dbo.RunTimeFeature
(
		RunTimeFeatureId		INT				NOT NULL
	,	ApplicationId			INT				NOT NULL		
    ,	Name					VARCHAR (50)	NOT NULL	
    ,	Description				VARCHAR (500)	NOT NULL	 
    ,	SortOrder				INT				NOT NULL
);

