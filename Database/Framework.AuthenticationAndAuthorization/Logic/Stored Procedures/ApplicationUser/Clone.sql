IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserClone'
	DROP  Procedure ApplicationUserClone
END
GO

PRINT 'Creating Procedure ApplicationUserClone'
GO

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
		@ApplicationUserId			INT			= NULL 	OUTPUT		
	,	@FirstName			VARCHAR(50)						
	,	@LastName			VARCHAR(50)						
	,	@MiddleName			VARCHAR(50)							
	,	@AuditId			INT									
	,	@AuditDate			DATETIME			= NULL				
	,	@SystemEntityType	VARCHAR(50)			= 'ApplicationUser'			
)
AS
BEGIN
	
	-- This is not applicable for ApplicationUser table but it wil be applicable for other tables
	--SELECT	@FirstName		= FirstName
	--	,	@LastName		= LastName					
	--FROM	ApplicationUser
	--WHERE	ApplicationUserId = @ApplicationUserId

	--EXEC dbo.ApplicationUserInsert 
	--		@ApplicationUserId	=	NULL
	--	,	@FirstName	=	@FirstName
	--	,	@LastName	=	@LastName
	--	,	@MiddleName =   @MiddleName
	--	,	@AuditId	=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType				= @SystemEntityType
		,	@EntityKey						= @ApplicationUserId
		,	@AuditAction					= 'Clone' 
		,	@CreatedDate					= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
