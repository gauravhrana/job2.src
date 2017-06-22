IF OBJECT_ID ('dbo.ReleaseLogDetail') IS NOT NULL
	DROP TABLE dbo.ReleaseLogDetail
GO

CREATE TABLE dbo.ReleaseLogDetail
(
		ReleaseLogDetailId			INT					NOT NULL	
	,	ApplicationId				INT					NOT NULL	
	,	ReleaseLogId				INT					NOT NULL
	,	ReleaseIssueTypeId			INT					NOT NULL
	,	ReleasePublishCategoryId	INT					NOT NULL	
	,	ItemNo						INT					NOT NULL	
	,	Description					VARCHAR(50)			NOT NULL	
	,	SortOrder					INT					NOT NULL	
	,	RequestedBy					VARCHAR(50)			NOT NULL	
	,	PrimaryDeveloper			VARCHAR(50)         NOT NULL    
	,	RequestedDate				DATETIME			NOT NULL	
	,	JIRA						VARCHAR(50)			NOT NULL
	,	Feature						VARCHAR(255)		NULL
	,	PrimaryEntity				VARCHAR(225)		NOT NULL
	,	TimeSpent					VARCHAR(50)			NULL
	,	ModuleId					INT					NOT NULL
	,	ReleaseFeatureId			INT					NOT NULL
	,	SystemEntityTypeId			INT					NULL
)
GO