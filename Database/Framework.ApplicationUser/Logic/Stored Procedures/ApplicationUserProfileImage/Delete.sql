--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserProfileImageDelete')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserProfileImageDelete'
--	DROP  Procedure ApplicationUserProfileImageDelete
--END
--GO

--PRINT 'Creating Procedure ApplicationUserProfileImageDelete'
--GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserProfileImageDelete
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
CREATE Procedure dbo.ApplicationUserProfileImageDelete
(
		@ApplicationUserProfileImageId		INT			= NULL
	,	@AuditId							INT			= NULL			
	,	@AuditDate							DATETIME	= NULL
	,	@SystemEntityType					VARCHAR(50)	= 'ApplicationUserProfileImage'
)
AS
BEGIN

	DELETE	dbo.ApplicationUserProfileImage
	WHERE	ApplicationUserProfileImageId	=	ISNULL(@ApplicationUserProfileImageId,	ApplicationUserProfileImageId)	

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationUserProfileImageId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
