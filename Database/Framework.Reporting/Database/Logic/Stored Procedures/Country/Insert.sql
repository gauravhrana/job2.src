IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'CountryInsert')
BEGIN
	PRINT 'Dropping Procedure CountryInsert'
	DROP  Procedure CountryInsert
END
GO

PRINT 'Creating Procedure CountryInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:CountryInsert
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

CREATE Procedure dbo.CountryInsert
(
		@CountryId					INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT				= NULL	
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(50)						
	,	@SortOrder					INT
	,	@TimeZoneId					INT								
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'Country'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @CountryId OUTPUT, @AuditId
	
	INSERT INTO dbo.Country 
	( 
			CountryId	
		,   ApplicationId					
		,	Name						
		,	Description					
		,	SortOrder
		,	TimeZoneId						
	)
	VALUES 
	(  
			@CountryId	
		,   @ApplicationId	
		,	@Name						
		,	@Description				
		,	@SortOrder	
		,	@TimeZoneId		
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @CountryId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 