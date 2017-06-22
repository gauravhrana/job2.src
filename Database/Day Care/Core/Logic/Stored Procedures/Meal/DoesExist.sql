IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='MealDoesExist')
BEGIN
	PRINT 'Dropping Procedure MealDoesExist'
	DROP  Procedure  MealDoesExist
END
GO

PRINT 'Creating Procedure MealDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: MealDoesExist
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

Create procedure dbo.MealDoesExist
(
		@StudentId				INT				= NULL	
	,	@ApplicationId			INT		
	,	@Date					DATETIME		= NULL		
	,	@MealTypeId				INT				= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'Meal'	
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.Meal a
	WHERE		a.StudentId		=	@StudentId	
	AND			a.Date			=	@Date
	AND			a.MealTypeId	=	@MealTypeId
	AND			a.ApplicationId	=	@ApplicationId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

