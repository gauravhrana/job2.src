IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DiscountInsert')
BEGIN
	PRINT 'Dropping Procedure DiscountInsert'
	DROP  Procedure  DiscountInsert
END
GO

PRINT 'Creating Procedure DiscountInsert'
GO

/******************************************************************************
**		File: 
**		Name: DiscountInsert
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
CREATE Procedure dbo.DiscountInsert
(
		@DiscountId			INT				= NULL    OUTPUT
	,	@ApplicationId		INT			
	,	@Name				VARCHAR(50)
	,	@Description		VARCHAR(500)	= NULL
	,	@SortOrder			INT				= 1
	,	@Amount				INT				= 10
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME		= NULL
	,	@SystemEntityType	VARCHAR(50)		= 'Discount'
)
AS
BEGIN	

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @DiscountId OUTPUT
		
	INSERT INTO dbo.Discount
	(
			DiscountId
		,   ApplicationId
		,	Name
		,	Description
		,	SortOrder
		,	Amount
	)
	VALUES
	(
			@DiscountId
		,	@ApplicationId
		,	@Name
		,	@Description
		,	@SortOrder
		,	@Amount
	)
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @DiscountId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO