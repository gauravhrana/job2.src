IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NeedsInsert')
BEGIN
	PRINT 'Dropping Procedure NeedsInsert'
	DROP  Procedure  NeedsInsert
END
GO

PRINT 'Creating Procedure NeedsInsert'
GO

/******************************************************************************
**		File: 
**		Name: NeedsInsert
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
CREATE Procedure dbo.NeedsInsert
(
		@NeedsId		    INT
	,	@ApplicationId		INT			
	,	@StudentId			INT
	,	@RequestDate		DATETIME
	,   @ReceivedDate		DATETIME
	,	@NeedItemId		    INT
	,	@NeedItemStatus		VARCHAR(50)
	,	@NeedItemBy			DATETIME	
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME = NULL
)
AS
BEGIN	

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @NeedsId OUTPUT

	INSERT INTO dbo.Needs
	(
			 NeedsId
        ,    ApplicationId
        ,    StudentId
        ,	 RequestDate
		,	 ReceivedDate
		,	 NeedItemId
		,	 NeedItemStatus
		,    NeedItemBy		
	)
	VALUES
	(
			 @NeedsId
        ,    @ApplicationId
        ,    @StudentId
        ,	 @RequestDate
		,	 @ReceivedDate
		,	 @NeedItemId
		,	 @NeedItemStatus
		,    @NeedItemBy		
	)
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= 'Needs' 
		,	@EntityKey				= @NeedsId
		,	@AuditAction			= 'Insert' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
