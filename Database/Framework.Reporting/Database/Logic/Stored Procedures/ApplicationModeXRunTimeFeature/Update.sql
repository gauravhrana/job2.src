IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationModeXRunTimeFeatureUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationModeXRunTimeFeatureUpdate'
	DROP  Procedure  ApplicationModeXRunTimeFeatureUpdate
END
GO

PRINT 'Creating Procedure ApplicationModeXRunTimeFeatureUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationModeXRunTimeFeatureUpdate
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.ApplicationModeXRunTimeFeatureUpdate
(
		@ApplicationModeXRunTimeFeatureId	INT		 			
	,	@ApplicationModeId					INT			
	,	@RunTimeFeatureId					INT	
	,	@ApplicationId						INT			
	,	@AuditId							INT						
	,	@AuditDate							DATETIME	= NULL	
	,	@SystemEntityType					VARCHAR(50)	= 'ApplicationModeXRunTimeFeature'
)
AS
BEGIN 

	UPDATE	dbo.ApplicationModeXRunTimeFeature 
	SET		ApplicationModeId		=	@ApplicationModeId	
		,	RunTimeFeatureId		=   @RunTimeFeatureId
		,	ApplicationId			=	@ApplicationId						
	WHERE	ApplicationModeXRunTimeFeatureId		=	@ApplicationModeXRunTimeFeatureId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ApplicationModeXRunTimeFeature'
		,	@EntityKey				= @ApplicationModeXRunTimeFeatureId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO