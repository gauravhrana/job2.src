IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MealTypeInsert')
BEGIN
	PRINT 'Dropping Procedure MealTypeInsert'
	DROP  Procedure MealTypeInsert
END
GO

PRINT 'Creating Procedure MealTypeInsert'
GO

/******************************************************************************
**		File: 
**		Name: MealTypeInsert
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
CREATE Procedure dbo.MealTypeInsert
(
		@MealTypeId			INT				= NULL OUTPUT
	,	@ApplicationId		INT				
	,	@Name				VARCHAR(50)
	,	@Description		VARCHAR(500)	= NULL
	,	@SortOrder			INT				= 1
	,   @AuditId			INT			
    ,   @AuditDate			DATETIME		= NULL
	,	@SystemEntityType	VARCHAR(50)		= 'MealType'
)
AS
BEGIN	

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @MealTypeId OUTPUT
			
	INSERT INTO dbo.MealType
	(
			MealTypeId
		,	ApplicationId
		,	Name
		,	Description
		,	SortOrder
	)
	VALUES
	(
			@MealTypeId
		,	@ApplicationId
		,	@Name
		,	@Description
		,	@SortOrder
	)

--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @MealTypeId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
