IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MealDetailInsert')
BEGIN
	PRINT 'Dropping Procedure MealDetailInsert'
	DROP  Procedure MealDetailInsert
END
GO

PRINT 'Creating Procedure MealDetailInsert'
GO

/******************************************************************************
**		File: 
**		Name: MealDetailInsert
**		Desc: 
**
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**		----------						-----------
**
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:			Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.MealDetailInsert
(
		@MealDetailId		INT
	,	@ApplicationId		INT
	,	@MealId	            INT		
	,	@FoodTypeId		    INT
	,	@AmtFinished        FLOAT
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME = NULL	
	,	@SystemEntityType   VARCHAR(50) = 'MealDetail'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @MealDetailId OUTPUT

	INSERT INTO dbo.MealDetail
	(
			MealDetailId
		,	ApplicationId
		,	MealId
		,	FoodTypeId	
		,	AmtFinished 
	)
	
	VALUES
	(
			@MealDetailId	
		,	@ApplicationId
		,	@MealId
		,	@FoodTypeId
		,	@AmtFinished
	)

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @MealDetailId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
