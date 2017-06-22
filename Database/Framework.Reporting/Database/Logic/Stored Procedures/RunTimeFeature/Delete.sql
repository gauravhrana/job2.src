IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'RunTimeFeatureDelete')
BEGIN
	PRINT 'Dropping Procedure RunTimeFeatureDelete'
	DROP  Procedure RunTimeFeatureDelete
END
GO

PRINT 'Creating Procedure RunTimeFeatureDelete'
GO
/******************************************************************************
**		File: 
**		Name: RunTimeFeatureDelete
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
CREATE Procedure dbo.RunTimeFeatureDelete
(
		@RunTimeFeatureId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'RunTimeFeature'
)
AS
BEGIN

	DELETE	 dbo.RunTimeFeature
	WHERE	 RunTimeFeatureId = @RunTimeFeatureId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'RunTimeFeature'
		,	@EntityKey				= @RunTimeFeatureId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
