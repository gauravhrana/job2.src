IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FeatureOwnerStatusDelete')
BEGIN
	PRINT 'Dropping Procedure FeatureOwnerStatusDelete'
	DROP  Procedure FeatureOwnerStatusDelete
END
GO

PRINT 'Creating Procedure FeatureOwnerStatusDelete'
GO
/******************************************************************************
**		File: 
**		Name: FeatureOwnerStatusDelete
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
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.FeatureOwnerStatusDelete
(
		@FeatureOwnerStatusId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'FeatureOwnerStatus'
)
AS
BEGIN

	DELETE	 dbo.FeatureOwnerStatus
	WHERE	 FeatureOwnerStatusId = @FeatureOwnerStatusId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'FeatureOwnerStatus'
		,	@EntityKey				= @FeatureOwnerStatusId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
