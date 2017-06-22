IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AuditHistoryDeleteDataBySystemEntity')
BEGIN
	PRINT 'Dropping Procedure AuditHistoryDeleteDataBySystemEntity'
	DROP  Procedure  dbo.AuditHistoryDeleteDataBySystemEntity
END
GO

PRINT 'Creating Procedure AuditHistoryDeleteDataBySystemEntity'
GO

/******************************************************************************
**		File: 
**		Name: AuditTestData
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------					   ---------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.AuditHistoryDeleteDataBySystemEntity
(
		@EntityKey				VARCHAR(100)
	,	@SystemEntityTypeId		INT
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'AuditHistory'
)
AS
BEGIN
	
	DECLARE @sql		NVARCHAR(512)
	DECLARE @EntityName VARCHAR(50) = 'Person'
	DECLARE @DBName		AS	VARCHAR(50)

	SELECT		@EntityName = EntityName
	FROM		Configuration.dbo.SystemEntityType 
	WHERE		SystemEntityTypeId	= @SystemEntityTypeId

	SELECT	@DBName				= a.ConnectionKeyName
	FROM	Configuration.dbo.SetUpConfiguration a
	WHERE	a.EntityName		= @EntityName
	
	IF	@DBName = 'ApplicationServices'
	BEGIN
		SET @DBName = 'CommonServices'
	END

	-- Set Dynamic SQL
	--SET @sql =  N'	SELECT * FROM AuditHistory '
	SET @sql =  N'	DELETE FROM dbo.AuditHistory '
		+	'	WHERE SystemEntityId = '	+	CAST(@SystemEntityTypeId AS varchar(50))
		+	'	AND	EntityKey IN (SELECT '+	@EntityName +'Id FROM '+ @DBName+'.dbo.' + @EntityName + ' WHERE '+ @EntityName +'Id in (' + @EntityKey +' ))'	

	-- Execute Dynamic SQL
	EXEC (@sql) 

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO