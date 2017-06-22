IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FindRecordsWithNoAuditHistory')
BEGIN
	PRINT 'Dropping Procedure FindRecordsWithNoAuditHistory'
	DROP  Procedure FindRecordsWithNoAuditHistory
END
GO

PRINT 'Creating Procedure FindRecordsWithNoAuditHistory'
GO


/******************************************************************************
**		File: 
**		Name: FindRecordsWithNoAuditHistory
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
**     ----------							-----------
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

CREATE Procedure dbo.FindRecordsWithNoAuditHistory
(
		@SystemEntityTypeId		INT 
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'AuditHistory'
)
AS
BEGIN
	DECLARE @EntityName			AS VARCHAR(50)
	DECLARE @DBName				AS VARCHAR(50)
	DECLARE @sql				AS VARCHAR(1000)

	SELECT	@EntityName				= a.EntityName 
	FROM	Configuration.dbo.SystemEntityType a
	WHERE	a.SystemEntityTypeId	= @SystemEntityTypeId

	SELECT	@DBName				= a.ConnectionKeyName
	FROM	Configuration.dbo.SetUpConfiguration a
	WHERE	a.EntityName	= @EntityName

	IF	@DBName = 'ApplicationServices'
		SET @DBName = 'CommonServices'

	CREATE TABLE #TempNoHistory
	(	
			Id					INT				IDENTITY(1,1)
		,	SystemEntityType	VARCHAR(50)
		,	EntityKey			INT
		,	NoHistoryRecords	INT			
		,	Reason				VARCHAR(500)
	)

	SET @sql =	'	INSERT INTO #TempNoHistory																'+
				'	SELECT  ''' + @EntityName + '''							AS SystemEntityType				'+
				'		,	c.' + @EntityName + 'Id							AS EntityKey					'+
				'		,	(																				'+
				'				SELECT COUNT(*)																'+
				'				FROM CommonServices.dbo.AuditHistory										'+
				'				WHERE SystemEntityId = ' + CAST(@SystemEntityTypeId AS VARCHAR(10))			 +
				'				AND EntityKey = c.' + @EntityName + 'Id										'+
				'			)												AS TotalAuditRecords			'+
				'		,	''''											AS Reason						'+
				'	FROM	' + @DBName + '.dbo.' + @EntityName + ' c										'+
				'	WHERE	c.' + @EntityName + 'Id NOT IN													'+
				'		(																					'+
				'			SELECT		a.EntityKey															'+
				'			FROM		CommonServices.dbo.AuditHistory						a				'+
				'			WHERE a.AuditHistoryId IN														'+
				'			(																				'+
				'				SELECT MIN(AuditHistoryId) FROM CommonServices.dbo.AuditHistory x			'+
				'				WHERE x.SystemEntityId = ' + CAST(@SystemEntityTypeId AS VARCHAR(10))		 +
				'				GROUP BY EntityKey, x.SystemEntityId										'+
				'			)																				'+
				'			AND a.AuditActionId = 1															'+
				'		)																					';
			
	Execute (@sql)
	
	UPDATE #TempNoHistory
	SET Reason = 'No History Records'
	WHERE NoHistoryRecords = 0
	
	UPDATE #TempNoHistory
	SET Reason = 'Insert Record Missing'
	WHERE Reason = ''
	
	UPDATE #TempNoHistory
	SET Reason = 'Insert Record Not First One'
	WHERE Reason = 'Insert Record Missing'
	AND	(	
			SELECT	COUNT(1) 
			FROM AuditHistory a
			WHERE	a.EntityKey			= EntityKey
			AND		a.SystemEntityId	= @SystemEntityTypeId
			AND		a.AuditActionId		= 1
		) > 0

	SELECT	a.* 
	FROM	#TempNoHistory a
	ORDER BY a.Id ASC

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END	
GO
   