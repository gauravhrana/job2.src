IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationModeXRunTimeFeatureInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationModeXRunTimeFeatureInsert'
	DROP  Procedure ApplicationModeXRunTimeFeatureInsert
END
GO

PRINT 'Creating Procedure ApplicationModeXRunTimeFeatureInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ApplicationModeXRunTimeFeatureInsert
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/

CREATE Procedure dbo.ApplicationModeXRunTimeFeatureInsert
(
		@ApplicationModeXRunTimeFeatureId	INT			= NULL 	OUTPUT		
	,	@ApplicationModeId					INT					
	,	@ApplicationId						INT	
	,	@RunTimeFeatureId					INT	
	,	@AuditId							INT									
	,	@AuditDate							DATETIME	= NULL				
	,	@SystemEntityType					VARCHAR(50)	= 'ApplicationModeXRunTimeFeature'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationModeXRunTimeFeatureId OUTPUT, @AuditId
	
	INSERT INTO dbo.ApplicationModeXRunTimeFeature 
	( 
			ApplicationModeXRunTimeFeatureId						
		,	ApplicationModeId	
		,	ApplicationId			
		,	RunTimeFeatureId
	)
	VALUES 
	(  
			@ApplicationModeXRunTimeFeatureId					
		,	@ApplicationModeId	
		,	@ApplicationId	
		,	@RunTimeFeatureId
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationModeXRunTimeFeatureId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 