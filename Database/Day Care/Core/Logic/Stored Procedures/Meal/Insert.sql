IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MealInsert')
BEGIN
	PRINT 'Dropping ProcedureMealInsert'
    DROP  Procedure MealInsert
END
GO

PRINT 'Creating ProcedureMealInsert'
GO

/******************************************************************************
**		File: 
**		Name: pMealInsert
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
CREATE Procedure dbo.MealInsert
(
		@MealId				INT
	,	@ApplicationId		INT
	,	@StudentId			INT
	,	@Date				Datetime
	,	@MealTypeId			INT
	,   @AuditId			INT			
    ,   @AuditDate			DATETIME	 = NULL
	,	@SystemEntityType	VARCHAR(50)	 = 'Meal'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @MealId OUTPUT
	

	INSERT INTO dbo.Meal
	(
			MealId
		,	ApplicationId
		,	StudentId
		,	Date
		,	MealTypeId
	)
	
	VALUES
	(
			@MealId
		,	@ApplicationId
		,	@StudentId
		,	@Date
		,	@MealTypeId
	)
--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MealId
		,	@AuditAction			= 'Insert' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId		
END
GO