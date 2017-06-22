﻿IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='MealTypeDoesExist')
BEGIN
	PRINT 'Dropping Procedure MealTypeDoesExist'
	DROP  Procedure  MealTypeDoesExist
END
GO

PRINT 'Creating Procedure MealTypeDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: MealTypeDoesExist
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
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.MealTypeDoesExist
(
		@Name					VARCHAR(50)		= NULL	
	,	@ApplicationId			INT	
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'MealType'		
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.MealType a
	WHERE		a.Name = @Name	
	AND			a.ApplicationId	= @ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

