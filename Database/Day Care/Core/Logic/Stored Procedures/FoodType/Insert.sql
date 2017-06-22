IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FoodTypeInsert')
BEGIN
	PRINT 'Dropping Procedure FoodTypeInsert'
	DROP  Procedure FoodTypeInsert
END
GO

PRINT 'Creating Procedure FoodTypeInsert'
GO

/******************************************************************************
**		File: 
**		Name: pFoodTypeInsert
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
CREATE Procedure dbo.FoodTypeInsert
(
		@FoodTypeId		    INT				= NULL OUTPUT
	,	@ApplicationId		INT				
	,	@Name				VARCHAR(50)
	,	@Description		VARCHAR(500)	= NULL
	,	@SortOrder			INT				= 1
	,   @AuditId		    INT			
    ,   @AuditDate		    DATETIME		= NULL
	,	@SystemEntityType	VARCHAR(50)	    = 'FoodType'
)
AS
BEGIN	

		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @FoodTypeId OUTPUT
	
	INSERT INTO dbo.FoodType
	(
			FoodTypeId
		,	ApplicationId
		,	Name
		,	Description
		,	SortOrder
	)
	VALUES
	(
			@FoodTypeId
		,	@ApplicationId
		,	@Name
		,	@Description
		,	@SortOrder
	)
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FoodTypeId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
