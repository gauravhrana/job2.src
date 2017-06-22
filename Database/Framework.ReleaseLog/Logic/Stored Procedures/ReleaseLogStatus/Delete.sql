IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogStatusDelete')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogStatusDelete'
	DROP  Procedure ReleaseLogStatusDelete
END
GO

PRINT 'Creating Procedure ReleaseLogStatusDelete'
GO
/******************************************************************************
**		File: 
**		Name: ReleaseLogStatusDelete
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
CREATE Procedure dbo.ReleaseLogStatusDelete
(
		@ReleaseLogStatusId 			    INT						
	,	@AuditId							INT						
	,	@AuditDate							DATETIME	= NULL		
	,	@SystemEntityType					VARCHAR(50)	= 'ReleaseLogStatus'
)
AS
BEGIN
		-- Update   ReleaseLogStatusId with 'DefaultStatus' before Deleting to refer the FK records.
		UPDATE	ReleaseLog  
		SET		ReleaseLogStatusId	=	10011
		WHERE	ReleaseLogStatusId	=	@ReleaseLogStatusId

		DELETE	 dbo.ReleaseLogStatus
		WHERE	 ReleaseLogStatusId = @ReleaseLogStatusId

		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseLogStatus'
		,	@EntityKey				= @ReleaseLogStatusId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END
GO
