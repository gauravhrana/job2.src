IF OBJECT_ID ('dbo.StoredProcedureLog') IS NOT NULL
	DROP TABLE dbo.StoredProcedureLog
GO


CREATE TABLE dbo.StoredProcedureLog 
(
    StoredProcedureLogId			INT				NOT NULL,
    Name							VARCHAR (50)	NOT NULL,
    TimeOfExecution					DATETIME		NOT NULL,
	ExecutedBy						VARCHAR(50)		NOT NULL
);


