--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserProfileImageMasterDelete')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserProfileImageMasterDelete'
--	DROP  Procedure ApplicationUserProfileImageMasterDelete
--END
--GO

--PRINT 'Creating Procedure ApplicationUserProfileImageMasterDelete'
--GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserProfileImageMasterDelete
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
CREATE Procedure dbo.ApplicationUserProfileImageMasterDelete
(
		@ApplicationUserProfileImageMasterId	INT			= NULL
	,	@AuditId								INT			= NULL			
	,	@AuditDate								DATETIME	= NULL
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationUserProfileImageMaster'
)
AS
BEGIN

	DELETE	dbo.ApplicationUserProfileImageMaster
	WHERE	ApplicationUserProfileImageMasterId	=	ISNULL(@ApplicationUserProfileImageMasterId,	ApplicationUserProfileImageMasterId)	

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationUserProfileImageMasterId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
