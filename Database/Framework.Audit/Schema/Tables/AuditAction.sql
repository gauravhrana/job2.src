--IF OBJECT_ID ('dbo.AuditAction') IS NOT NULL
--BEGIN
--	DROP TABLE dbo.AuditAction
--END
--GO

CREATE TABLE dbo.AuditAction 
(
    AuditActionId		INT          NOT NULL	,
    Name				VARCHAR (50) NOT NULL	,
    Description			VARCHAR (50) NOT NULL	,
    SortOrder			INT          NOT NULL
);
