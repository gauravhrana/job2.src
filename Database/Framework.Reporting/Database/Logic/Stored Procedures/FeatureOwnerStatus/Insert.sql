IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FeatureOwnerStatusInsert')
BEGIN
	PRINT 'Dropping Procedure FeatureOwnerStatusInsert'
	DROP  Procedure FeatureOwnerStatusInsert
END
GO

PRINT 'Creating Procedure FeatureOwnerStatusInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:FeatureOwnerStatusInsert
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

CREATE Procedure dbo.FeatureOwnerStatusInsert
(
		@FeatureOwnerStatusId			INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT				= NULL	
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(50)						
	,	@SortOrder					INT								
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'FeatureOwnerStatus'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @FeatureOwnerStatusId OUTPUT, @AuditId
	
	INSERT INTO dbo.FeatureOwnerStatus 
	( 
			FeatureOwnerStatusId	
		,   ApplicationId					
		,	Name						
		,	Description					
		,	SortOrder						
	)
	VALUES 
	(  
			@FeatureOwnerStatusId	
		,   @ApplicationId	
		,	@Name						
		,	@Description				
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FeatureOwnerStatusId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 