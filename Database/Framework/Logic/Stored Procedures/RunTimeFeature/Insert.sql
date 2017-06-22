IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'RunTimeFeatureInsert')
BEGIN
	PRINT 'Dropping Procedure RunTimeFeatureInsert'
	DROP  Procedure RunTimeFeatureInsert
END
GO

PRINT 'Creating Procedure RunTimeFeatureInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:RunTimeFeatureInsert
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

CREATE Procedure dbo.RunTimeFeatureInsert
(
		@RunTimeFeatureId			INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT				= NULL	
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(500)						
	,	@SortOrder					INT								
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'RunTimeFeature'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @RunTimeFeatureId OUTPUT, @AuditId
	
	INSERT INTO dbo.RunTimeFeature 
	( 
			RunTimeFeatureId	
		,   ApplicationId					
		,	Name						
		,	Description					
		,	SortOrder						
	)
	VALUES 
	(  
			@RunTimeFeatureId	
		,   @ApplicationId	
		,	@Name						
		,	@Description				
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @RunTimeFeatureId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 