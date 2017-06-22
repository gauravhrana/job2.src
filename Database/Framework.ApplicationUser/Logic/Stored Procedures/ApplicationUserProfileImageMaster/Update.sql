--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserProfileImageMasterUpdate')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserProfileImageMasterUpdate'
--	DROP  Procedure  ApplicationUserProfileImageMasterUpdate
--END
--GO

--PRINT 'Creating Procedure ApplicationUserProfileImageMasterUpdate'
--GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserProfileImageMasterUpdate
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

CREATE Procedure dbo.ApplicationUserProfileImageMasterUpdate
(
		@ApplicationUserProfileImageMasterId	INT			= NULL 	OUTPUT	
	,	@Title									VARCHAR(50)	
	,	@ApplicationId							INT							
	,	@Image									VARBINARY(MAX)	
	,	@AuditId								INT			= NULL					
	,	@AuditDate								DATETIME	= NULL	
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationUserProfileImageMaster'
)
AS
BEGIN 

	UPDATE	dbo.ApplicationUserProfileImageMaster 
	SET		Title			=	@Title	
		,	ApplicationId	=	@ApplicationId	
		,	Image			=	@Image							
	WHERE	ApplicationUserProfileImageMasterId		=	@ApplicationUserProfileImageMasterId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ApplicationUserProfileImageMasterId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO