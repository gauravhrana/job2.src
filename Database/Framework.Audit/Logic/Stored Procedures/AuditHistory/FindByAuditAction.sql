﻿
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AuditHistoryFindByAuditAction')
BEGIN
	PRINT 'Dropping Procedure AuditHistoryFindByAuditAction'
	DROP  Procedure  AuditHistoryFindByAuditAction
END
GO

PRINT 'Creating Procedure AuditHistoryFindByAuditAction'
GO

/******************************************************************************
**		File: 
**		Name: AuditHistoryFindByAuditAction
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

CREATE Procedure dbo.AuditHistoryFindByAuditAction
(	
		@SystemEntityId			INT				= NULL	
	,	@EntityKey				INT				= NULL	
	,	@AuditActionId			INT				= NULL		
	,	@PersonId				INT				= NULL				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'AuditHistory'
)
AS
BEGIN

	DECLARE @CreatedDate		AS	DATETIME
	DECLARE @tmpStartDate		AS	DATETIME	
	DECLARE @tmpAuditActionId	AS	INT
	DECLARE @tmpPersonId		AS	INT
	DECLARE @MyAuditActionId	AS	INT

	DECLARE @SystemEntity		AS	VARCHAR(50)

	SET @tmpStartDate 		= NULL
	SET @tmpAuditActionId 	= NULL
	SET @tmpPersonId		= NULL

	DECLARE db_cursor CURSOR FOR
		SELECT 	a.AuditActionId			
			,	a.CreatedDate	
		FROM	dbo.AuditHistory a			
		WHERE	a.SystemEntityId	=	@SystemEntityId			
		AND		a.EntityKey			=	@EntityKey	
		ORDER	BY	a.AuditActionId ASC
				,	a.CreatedDate	ASC	
	
	CREATE TABLE #TempMyTable 
	(	
			SystemEntityId	INT
		,	EntityKey		INT
		,	AuditActionId	INT
		,	StartDate		DATETIME
		,	EndDate			DATETIME
		,	RecordCount		INT
	)	
	

	OPEN db_cursor
	FETCH NEXT
	FROM db_cursor INTO @MyAuditActionId, @CreatedDate 

	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		IF @tmpAuditActionId IS NULL OR @MyAuditActionId <> @tmpAuditActionId
			BEGIN
		
				SET @tmpStartDate 		=	@CreatedDate
				SET @tmpAuditActionId	=  	@MyAuditActionId									
			
				INSERT #TempMyTable 
				(
						SystemEntityId	
					,	EntityKey									
					,	AuditActionId	
					,	StartDate		
					,	EndDate			
					,	RecordCount						
				)
				VALUES 
				(
						@SystemEntityId	
					,	@EntityKey							
					,	@MyAuditActionId	
					,	@tmpStartDate		
					,	@tmpStartDate			
					,	1						
				)
			
				--PRINT @tmpStartDate
			
			END
		ELSE
			BEGIN	
					
				UPDATE #TempMyTable			
				SET		RecordCount		=	RecordCount + 1						
					,	EndDate			=	@CreatedDate
				WHERE	StartDate		= 	@tmpStartDate
				AND		AuditActionId	=	@MyAuditActionId
		
			END
	
		FETCH NEXT
	
		FROM db_cursor INTO @MyAuditActionId, @CreatedDate  

	END 

	CLOSE db_cursor 

	DEALLOCATE db_cursor
	
	
	SELECT	@SystemEntity		=	EntityName
	FROM	Configuration.dbo.SystemEntityType
	WHERE	SystemEntityTypeId	=	@SystemEntityId

	SELECT 	a.SystemEntityId	
		,	a.EntityKey		
		--,	a.PersonId		
		,	a.AuditActionId		
		,	a.StartDate							AS	'Start'	
		,	a.EndDate							AS	'End'
		,	a.RecordCount						AS	'Count'
		,	@SystemEntity						AS	'SystemEntity'
		,	c.Name								AS	'AuditAction'
		,	''									AS	'Action By'
		,	''									AS	'Person'
		--,	d.FirstName + ' ' + d.LastName		AS	'Action By'	
		--,	d.FirstName + ' ' + d.LastName		AS	'Person'
	FROM #TempMyTable													a
	INNER JOIN dbo.AuditAction											c		ON		a.AuditActionId 	= c.AuditActionId
	--INNER JOIN AuthenticationAndAuthorization.dbo.ApplicationUser  	d		ON		a.PersonId = d.ApplicationUserId
	ORDER BY a.EndDate DESC	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END

GO