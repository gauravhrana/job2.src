--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserClone')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserClone'
--	DROP  Procedure ApplicationUserClone
--END
--GO

--PRINT 'Creating Procedure ApplicationUserClone'
--GO

/*********************************************************************************************
**		File: 
**		Name: ApplicationUserClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.ApplicationUserClone
(
		@ApplicationUserId			INT					= NULL 	OUTPUT	
	,	@ApplicationId				INT	
	,	@ApplicationUserName		VARCHAR(50)	
	,	@FirstName					VARCHAR(50)						
	,	@LastName					VARCHAR(50)						
	,	@MiddleName					VARCHAR(50)	
	,	@ApplicationUserTitleId		INT		
	,	@EmailAddress				VARCHAR(320)				
	,	@AuditId					INT									
	,	@AuditDate					DATETIME			= NULL				
	,	@SystemEntityType			VARCHAR(50)			= 'ApplicationUser'			
)
AS
BEGIN
	
	-- This is not applicable for ApplicationUser table but it wil be applicable for other tables
	SELECT	@ApplicationId				= ApplicationId	
		,	@FirstName					= FirstName
		,	@LastName					= LastName	
		,	@MiddleName					= MiddleName
		,	@ApplicationUserTitleId		= ApplicationUserTitleId	
		,	@ApplicationUserName		= ApplicationUserName	
		,	@EmailAddress				= EmailAddress
	FROM	dbo.ApplicationUser
	WHERE	ApplicationUserId			= @ApplicationUserId

	EXEC dbo.ApplicationUserInsert
			@ApplicationUserId			=	NULL
		,	@ApplicationId				=	@ApplicationId
		,	@FirstName					=	@FirstName
		,	@LastName					=	@LastName
		,	@MiddleName					=   @MiddleName
		,	@ApplicationUserTitleId		=	@ApplicationUserTitleId
		,	@ApplicationUserName		=	@ApplicationUserName
		,	@EmailAddress				=	@EmailAddress	
		,	@AuditId					=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType				= @SystemEntityType
		,	@EntityKey						= @ApplicationUserId
		,	@AuditAction					= 'Clone' 
		,	@CreatedDate					= @AuditDate
		,	@CreatedByPersonId				= @AuditId	

END	
GO
