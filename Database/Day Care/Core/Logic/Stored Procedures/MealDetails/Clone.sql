IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MealDetailClone')
BEGIN
	PRINT 'Dropping Procedure MealDetailClone'
	DROP  Procedure MealDetailClone
END
GO

PRINT 'Creating Procedure MealDetailClone'
GO

/*********************************************************************************************
**		File: 
**		Name: MealDetailClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.MealDetailClone
(
		@MealDetailId			INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT						
	,	@MealId					INT								
	,	@FoodTypeId				INT									
	,	@AmtFinished			FLOAT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'MealDetail'				
)
AS
BEGIN

	IF @MealDetailId IS NULL OR @MealDetailId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @MealDetailId OUTPUT
	END	
		
	
	SELECT	@MealId			=	MealId       
		,	@ApplicationId	=	ApplicationId 
		,	@FoodTypeId		=	FoodTypeId   
		,	@AmtFinished	=	AmtFinished				
	FROM	dbo.MealDetail
	WHERE	MealDetailId	= @MealDetailId 
	AND		ApplicationId	= @ApplicationId

	EXEC dbo.MealDetailInsert 
			@MealDetailId	=	NULL
		,	@ApplicationId	=   ApplicationId
		,	@MealId			=	@MealId      
		,	@FoodTypeId		=	@FoodTypeId  
		,	@AmtFinished	=	@AmtFinished
		,	@AuditId		=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @MealDetailId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
