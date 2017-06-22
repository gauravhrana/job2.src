IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='MealDetailDoesExist')
BEGIN
	PRINT 'Dropping Procedure MealDetailDoesExist'
	DROP  Procedure  MealDetailDoesExist
END
GO

PRINT 'Creating Procedure MealDetailDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: MealDetailDoesExist
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

Create procedure dbo.MealDetailDoesExist
(
		@MealId					INT				= NULL
	,	@ApplicationId			INT				
	,	@FoodTypeId				INT				= NULL				
	,	@AuditId				INT								
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'MealDetail'				
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.MealDetail a
	WHERE		a.MealId			=	@MealId	
	AND			a.FoodTypeId		=	@FoodTypeId	
	AND			a.ApplicationId		=	@ApplicationId  

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'MealDetails'
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

