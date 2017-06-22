IF OBJECT_ID ('dbo.HelpPageContext ') IS NOT NULL
	DROP TABLE dbo.HelpPageContext 
GO


CREATE TABLE dbo.HelpPageContext 
(
		HelpPageContextId		INT				NOT NULL
	,	ApplicationId			INT				NOT NULL	
    ,	Name					VARCHAR (50)	NOT NULL	
    ,	Description				VARCHAR (200)	NOT NULL	
    ,	SortOrder				INT				NOT NULL
);
