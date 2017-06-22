IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DateRangeTitleInsert')
BEGIN
	PRINT 'Dropping Procedure DateRangeTitleInsert'
	DROP  Procedure DateRangeTitleInsert
END
GO

PRINT 'Creating Procedure DateRangeTitleInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:DateRangeTitleInsert
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
**********************************************************************************************/

CREATE Procedure dbo.DateRangeTitleInsert
(
		@DateRangeTitleId			INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT				= NULL	
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(500)						
	,	@SortOrder					INT								
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'DateRangeTitle'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @DateRangeTitleId OUTPUT, @AuditId
	
	INSERT INTO dbo.DateRangeTitle 
	( 
			DateRangeTitleId	
		,   ApplicationId					
		,	Name						
		,	[Description]					
		,	SortOrder						
	)
	VALUES 
	(  
			@DateRangeTitleId	
		,   @ApplicationId	
		,	@Name						
		,	@Description				
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @DateRangeTitleId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 