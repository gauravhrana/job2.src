IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationModeXRunTimeFeatureClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationModeXRunTimeFeatureClone'
	DROP  Procedure ApplicationModeXRunTimeFeatureClone
END
GO

PRINT 'Creating Procedure ApplicationModeXRunTimeFeatureClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ApplicationModeXRunTimeFeatureClone
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
**		
**********************************************************************************************/

CREATE Procedure dbo.ApplicationModeXRunTimeFeatureClone
(
		@ApplicationModeXRunTimeFeatureId	 INT		 = NULL 	OUTPUT	
	,	@ApplicationId						 INT         = NULL
	,	@ApplicationModeId					 INT							
	,	@RunTimeFeatureId					 INT		
	,	@AuditId							 INT									
	,	@AuditDate							 DATETIME	 = NULL				
	,	@SystemEntityType					 VARCHAR(50) = 'ApplicationModeXRunTimeFeature'
)
AS
BEGIN

	IF @ApplicationModeXRunTimeFeatureId IS NULL OR @ApplicationModeXRunTimeFeatureId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationModeXRunTimeFeatureId OUTPUT
	END						
	
	SELECT	@ApplicationId			= ApplicationId	
		,	@ApplicationModeId		= ApplicationModeId	
		,	@RunTimeFeatureId		= RunTimeFeatureId				
	FROM	dbo.ApplicationModeXRunTimeFeature
	WHERE   ApplicationModeXRunTimeFeatureId				= @ApplicationModeXRunTimeFeatureId
	ORDER BY ApplicationModeXRunTimeFeatureId

	EXEC dbo.ApplicationModeXRunTimeFeatureInsert 
			@ApplicationModeXRunTimeFeatureId	=	NULL
		,   @ApplicationId						=   ApplicationId
		,	@ApplicationModeId					=   @ApplicationModeId	
		,	@RunTimeFeatureId					=	@RunTimeFeatureId		
		,	@AuditId							=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ApplicationModeXRunTimeFeature'
		,	@EntityKey				= @ApplicationModeXRunTimeFeatureId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
