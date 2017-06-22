--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserProfileImageUpdate')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserProfileImageUpdate'
--	DROP  Procedure  ApplicationUserProfileImageUpdate
--END
--GO

--PRINT 'Creating Procedure ApplicationUserProfileImageUpdate'
--GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserProfileImageUpdate
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

CREATE Procedure dbo.ApplicationUserProfileImageUpdate
(
		@ApplicationUserProfileImageId		INT			= NULL 	OUTPUT	
	,	@ApplicationId						INT			
	,	@ApplicationUserId					INT								
	,	@Image								VARBINARY(MAX)	
	,	@AuditId							INT			= NULL					
	,	@AuditDate							DATETIME	= NULL	
	,	@SystemEntityType					VARCHAR(50)	= 'ApplicationUserProfileImage'
)
AS
BEGIN 

	UPDATE	dbo.ApplicationUserProfileImage 
	SET		ApplicationUserId				=	@ApplicationUserId	
		,	ApplicationId					=	@ApplicationId		
		,	Image							=	@Image							
	WHERE	ApplicationUserProfileImageId	=	@ApplicationUserProfileImageId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ApplicationUserProfileImageId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO