IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TimeZoneInsert')
BEGIN
	PRINT 'Dropping Procedure TimeZoneInsert'
	DROP  Procedure TimeZoneInsert
END
GO

PRINT 'Creating Procedure TimeZoneInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:TimeZoneInsert
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

CREATE Procedure dbo.TimeZoneInsert
(
		@TimeZoneId					INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT				= NULL	
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(50)						
	,	@SortOrder					INT
	,	@TimeDifference				DECIMAL(4,2)								
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'TimeZone'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TimeZoneId OUTPUT, @AuditId
	
	INSERT INTO dbo.TimeZone 
	( 
			TimeZoneId	
		,   ApplicationId					
		,	Name						
		,	Description					
		,	SortOrder
		,	TimeDifference						
	)
	VALUES 
	(  
			@TimeZoneId	
		,   @ApplicationId	
		,	@Name						
		,	@Description				
		,	@SortOrder	
		,	@TimeDifference		
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TimeZoneId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 