IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AuditTestDataDetails')
BEGIN
	PRINT 'Dropping Procedure AuditTestDataDetails'
	DROP  Procedure  dbo.AuditTestDataDetails
END
GO

PRINT 'Creating Procedure AuditTestDataDetails'
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

CREATE Procedure dbo.AuditTestDataDetails
(
		@SystemEntityTypeId		INT
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'AuditHistory'
)
AS
BEGIN
	
	DECLARE @sql NVARCHAR(512)
	DECLARE @EntityName VARCHAR(50) = 'Person'

	SELECT		@EntityName = a.name 
	FROM		sys.Tables a
	INNER JOIN	Configuration.dbo.SystemEntityType b ON a.name = b.EntityName
	WHERE		b.SystemEntityTypeId	= @SystemEntityTypeId

	-- Set Dynamic SQL
	SET @sql =  N'SELECT * FROM TaskTimeTracker.dbo.'+ @EntityName 
			+	' WHERE '+ @EntityName +'Id < 0'	

	-- Execute Dynamic SQL
	EXEC (@sql) 
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO