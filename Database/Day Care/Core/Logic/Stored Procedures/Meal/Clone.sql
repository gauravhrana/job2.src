IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MealClone')
BEGIN
	PRINT 'Dropping Procedure MealClone'
	DROP  Procedure MealClone
END
GO

PRINT 'Creating Procedure MealClone'
GO

/*********************************************************************************************
**		File: 
**		Name: MealClone
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

CREATE Procedure dbo.MealClone
(
		@MealId					INT			= NULL 	OUTPUT			
	,	@ApplicationId			INT			
	,	@StudentId				INT									
	,	@Date					Datetime								
	,	@MealTypeId				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Meal'				
)
AS
BEGIN

	IF @MealId IS NULL OR @MealId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @MealId OUTPUT
	END	
		
	
	SELECT	@ApplicationId	=	ApplicationId
		,	@StudentId		=	StudentId
		,	@Date			=	Date
		,	@MealTypeId		=	MealTypeId				
	FROM	dbo.Meal
	WHERE	MealId			=	@MealId 
	AND		ApplicationId	=	@ApplicationId

	EXEC dbo.MealInsert 
			@MealId			=	NULL
		,	@ApplicationId	=	@ApplicationId
		,	@StudentId		=	@StudentId
		,	@Date			=	@Date
		,	@MealTypeId		=	@MealTypeId
		,	@AuditId		=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MealId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
