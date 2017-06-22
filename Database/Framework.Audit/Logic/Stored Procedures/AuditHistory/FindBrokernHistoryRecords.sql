IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FindBrokenHistoryRecords')
BEGIN
	PRINT 'Dropping Procedure FindBrokenHistoryRecords'
	DROP  Procedure FindBrokenHistoryRecords
END
GO

PRINT 'Creating Procedure FindBrokenHistoryRecords'
GO


/******************************************************************************
**		File: 
**		Name: FindBrokenHistoryRecords
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

CREATE Procedure dbo.FindBrokenHistoryRecords
(
		@TypeOfIssue			VARCHAR(50)
	,	@SystemEntityId			INT				= NULL
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'AuditHistory'
)
AS
BEGIN		

	CREATE TABLE #TempBroken 
	(
    		AuditHistoryId			INT			
		,	SystemEntityId			INT							
		,	EntityKey				INT							
		,	AuditActionId			INT							
		,	CreatedDate				DATETIME					
		,	CreatedByPersonId		INT	
		,	Reason					VARCHAR(500)							
	)

	If	@TypeOfIssue = 'InvalidEntityType'
		BEGIN			

			INSERT	INTO	#TempBroken
			SELECT	a.*
				,	'Entity Type Invalid'		AS	REASON
			FROM	dbo.AuditHistory a
			WHERE	a.SystemEntityId NOT IN
					(
						SELECT  x.SystemEntityTypeId 
						FROM	Configuration.dbo.SystemEntityType x
					)

		END
	ELSE IF	@TypeOfIssue = 'InvalidApplicationUser'
		BEGIN

			INSERT	INTO #TempBroken
			SELECT	a.*
				,	'ApplicationUser Invalid'	AS	REASON
			FROM	dbo.AuditHistory a
			WHERE	a.CreatedByPersonId	NOT IN
					(	
						SELECT	y.ApplicationUserId
						FROM	AuthenticationAndAuthorization.dbo.ApplicationUser y
					)

		END		
	ELSE IF	@TypeOfIssue = 'InvalidEntityKey'
		BEGIN

			DECLARE @EntityName			AS	VARCHAR(50)
			DECLARE @DBName				AS	VARCHAR(50)
			DECLARE @sql				AS	VARCHAR(1000)

			SELECT	@EntityName				= a.EntityName 
			FROM	Configuration.dbo.SystemEntityType a
			WHERE	a.SystemEntityTypeId	= @SystemEntityId

			IF	@EntityName	IS	NOT	NULL
				BEGIN

					SELECT	@DBName				= a.ConnectionKeyName
					FROM	Configuration.dbo.SetUpConfiguration a
					WHERE	a.EntityName		= @EntityName

					IF	@DBName = 'ApplicationServices'
					BEGIN
						SET @DBName = 'CommonServices'
					END

					SET		@sql	=	'	INSERT INTO #TempBroken														'+
								'	SELECT	a.*																	'+						
								'		,	''EntityKey Invalid''	AS	REASON									'+
								'	FROM	dbo.AuditHistory a													'+
								'	WHERE	a.SystemEntityId	=	' + CAST(@SystemEntityId AS VARCHAR(10))	 +
								'	AND		a.EntityKey			NOT IN											'+
								'		(																		'+
								'			SELECT	x.' + @EntityName + 'Id										'+
								'			FROM	' + @DBName + '.dbo.' + @EntityName + ' x					'+
								'		)																		'
			
					--SELECT @sql					
					Execute (@sql)

				END

		END


	

	SELECT		a.AuditHistoryId		
			,	a.SystemEntityId		
			,	a.EntityKey			
			,	a.AuditActionId		
			,	a.CreatedDate
			,	a.CreatedDate						AS 'Date'
			,	a.CreatedByPersonId
			,	b.EntityName						AS 'SystemEntity'
			,	c.Name								AS 'AuditAction'
			,	d.FirstName + ' ' + d.LastName		AS 'Action By'	
			,	d.FirstName + ' ' + d.LastName		AS 'Person'	
	FROM		#TempBroken a
	LEFT JOIN	Configuration.dbo.SystemEntityType					b		ON		a.SystemEntityId	= b.SystemEntityTypeId
	LEFT JOIN	dbo.AuditAction										c		ON		a.AuditActionId		= c.AuditActionId
	LEFT JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser  d		ON		a.CreatedByPersonId = d.ApplicationUserId
	ORDER BY	a.AuditHistoryId ASC


	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO


