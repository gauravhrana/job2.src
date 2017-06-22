IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteDeveloperValueDelete')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteDeveloperValueDelete'
	DROP  Procedure ReleaseNoteDeveloperValueDelete
END
GO

PRINT 'Creating Procedure ReleaseNoteDeveloperValueDelete'
GO
/******************************************************************************
**		File: 
**		Name: ReleaseNoteDeveloperValueDelete
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
CREATE Procedure dbo.ReleaseNoteDeveloperValueDelete
(
		@ReleaseNoteDeveloperValueId	INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'ReleaseNoteDeveloperValue'
)
AS
BEGIN

	DELETE	 dbo.ReleaseNoteDeveloperValue
	WHERE	 ReleaseNoteDeveloperValueId = @ReleaseNoteDeveloperValueId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseNoteDeveloperValue'
		,	@EntityKey				= @ReleaseNoteDeveloperValueId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
