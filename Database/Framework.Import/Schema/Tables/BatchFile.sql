IF OBJECT_ID ('dbo.BatchFile') IS NOT NULL
	DROP TABLE dbo.BatchFile
GO

CREATE TABLE dbo.BatchFile
(
		BatchFileId				INT				NOT NULL	
	,	ApplicationId			INT				NOT NULL
	,	Name					VARCHAR(50)		NOT NULL	 
	,	Folder					VARCHAR(1000)	NOT NULL
	,	BatchFile				VARCHAR(150)	NOT NULL
	,	BatchFileSetId			INT				NOT NULL	 
	,	Description				VARCHAR(150)	NOT NULL	
	,	FileTypeId				INT				NOT NULL	 
	,	SystemEntityTypeId		INT				NOT NULL	
	,	BatchFileStatusId		INT				NOT NULL	
	,	CreatedDate				DATETIME		NOT NULL	
	,	CreatedByPersonId		INT				NOT NULL	
	,	UpdatedDate				DATETIME		NOT NULL	
	,	UpdatedByPersonId		INT				NOT NULL
	,	Errors					VARCHAR(1000)	NOT NULL
)
GO
