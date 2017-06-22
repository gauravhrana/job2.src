IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TraceInsert')
BEGIN
	PRINT 'Dropping Procedure TraceInsert'
	DROP  Procedure TraceInsert
END
GO

PRINT 'Creating Procedure TraceInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:TraceInsert
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

CREATE Procedure dbo.TraceInsert
(
		@TraceId			INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT				= NULL	
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(50)						
	,	@SortOrder					INT								
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'Trace'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TraceId OUTPUT, @AuditId
	
	INSERT INTO dbo.Trace 
	( 
			ApplicationId					
		,	Name						
		,	Description					
		,	SortOrder						
	)
	VALUES 
	(  
			@ApplicationId	
		,	@Name						
		,	@Description				
		,	@SortOrder			
	)

	SELECT @TraceId = SCOPE_IDENTITY()

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TraceId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 