IF OBJECT_ID ('dbo.BatchFileHistory') IS NOT NULL
	DROP TABLE dbo.BatchFileHistory
GO

CREATE TABLE dbo.BatchFileHistory 
(
		BatchFileHistoryId		INT				NOT NULL
	,	ApplicationId			INT				NOT NULL
	,	BatchFileId				INT				NOT NULL	
	,	BatchFileSetId			INT				NOT NULL
	,	BatchFileStatusId		INT				NOT NULL	
    ,	UpdatedDate				DATETIME		NOT NULL
	,	UpdatedByPersonId		INT				NOT NULL	
);

