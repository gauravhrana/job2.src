--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserUpdate')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserUpdate'
--	DROP  Procedure  ApplicationUserUpdate
--END
--GO

--PRINT 'Creating Procedure ApplicationUserUpdate'
--GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserUpdate
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

CREATE Procedure dbo.ApplicationUserUpdate
(
		@ApplicationUserId			INT		
	,	@ApplicationUserName		VARCHAR(50)		
	,	@FirstName					VARCHAR(50)				
	,	@LastName					VARCHAR(50)			
	,	@MiddleName					VARCHAR(50)	
	,	@ApplicationUserTitleId		INT		
	,	@EmailAddress				VARCHAR(320)
	,	@ApplicationId				INT
	,	@AuditId					INT					
	,	@AuditDate					DATETIME	= NULL	
	,	@SystemEntityType			VARCHAR(50)	= 'ApplicationUser'			
)	
AS
BEGIN

	UPDATE	dbo.ApplicationUser 
	SET		FirstName					=	@FirstName					
		,	LastName					=	@LastName		
		,	MiddleName					=   @MiddleName
		,	ApplicationUserTitleId		=	@ApplicationUserTitleId				
		,	ApplicationId				=	@ApplicationId		
		,	ApplicationUserName			=	@ApplicationUserName	
		,	EmailAddress				=	@EmailAddress
	WHERE	ApplicationUserId			=	@ApplicationUserId	

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType				= @SystemEntityType	
		,	@EntityKey						= @ApplicationUserId
		,	@AuditAction					= 'Update'
		,	@CreatedDate					= @AuditDate
		,	@CreatedByPersonId				= @AuditId
 END
 GO