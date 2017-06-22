IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationModeXRunTimeFeatureDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationModeXRunTimeFeatureDelete'
	DROP  Procedure ApplicationModeXRunTimeFeatureDelete
END
GO

PRINT 'Creating Procedure ApplicationModeXRunTimeFeatureDelete'
GO
/******************************************************************************
**		File: 
**		Name: ApplicationModeXRunTimeFeatureDelete
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.ApplicationModeXRunTimeFeatureDelete
(
		@ApplicationModeXRunTimeFeatureId		INT			= NULL	
	,	@ApplicationModeId						INT			= NULL	
	,	@RunTimeFeatureId						INT			= NULL	
	,	@AuditId								INT					
	,	@AuditDate								DATETIME	= NULL
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationModeXRunTimeFeature'
)
AS
BEGIN

	DELETE	dbo.ApplicationModeXRunTimeFeature
	WHERE	ApplicationModeXRunTimeFeatureId	=	ISNULL(@ApplicationModeXRunTimeFeatureId,	ApplicationModeXRunTimeFeatureId)	
	AND		ApplicationModeId					=	ISNULL(@ApplicationModeId,			ApplicationModeId)
	AND		RunTimeFeatureId					=	ISNULL(@RunTimeFeatureId,			RunTimeFeatureId)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationModeXRunTimeFeatureId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
