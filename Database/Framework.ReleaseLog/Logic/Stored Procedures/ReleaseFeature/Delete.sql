IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseFeatureDelete')
BEGIN
	PRINT 'Dropping Procedure ReleaseFeatureDelete'
	DROP  Procedure ReleaseFeatureDelete
END
GO

PRINT 'Creating Procedure ReleaseFeatureDelete'
GO
/******************************************************************************
**		File: 
**		Name: ReleaseFeatureDelete
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
CREATE Procedure dbo.ReleaseFeatureDelete
(
		@ReleaseFeatureId			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'ReleaseFeature'
)
AS
BEGIN

	DELETE	 dbo.ReleaseFeature
	WHERE	 ReleaseFeatureId = @ReleaseFeatureId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseFeature'
		,	@EntityKey				= @ReleaseFeatureId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
