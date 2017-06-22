IF OBJECT_ID ('dbo.StoredProcedureLogDetail') IS NOT NULL
	DROP TABLE dbo.StoredProcedureLogDetail
GO


CREATE TABLE dbo.StoredProcedureLogDetail 
(
    
    StoredProcedureLogDetailId			INT				NOT NULL,
	StoredProcedureLogId				INT				NOT NULL,
	ParameterName						VARCHAR(50)		NOT NULL,
	ParameterValue						VARCHAR(50)		NOT NULL
);

==============
