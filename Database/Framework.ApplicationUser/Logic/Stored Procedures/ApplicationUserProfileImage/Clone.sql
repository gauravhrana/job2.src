--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserProfileImageClone')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserProfileImageClone'
--	DROP  Procedure ApplicationUserProfileImageClone
--END
--GO

--PRINT 'Creating Procedure ApplicationUserProfileImageClone'
--GO

/*********************************************************************************************
**		File: 
**		Name: ApplicationUserProfileImageClone
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

CREATE Procedure dbo.ApplicationUserProfileImageClone
(
		@ApplicationUserProfileImageId			INT				= NULL 	OUTPUT	
	,	@ApplicationId							INT				= NULL
	,	@ApplicationUserId						INT				= NULL				
	,	@Image									VARBINARY(MAX)	= NULL
	,	@AuditId								INT				= NULL					
	,	@AuditDate								DATETIME		= NULL
	,	@SystemEntityType						VARCHAR(50)		= 'ApplicationUserProfileImage'
)
AS
BEGIN		
	
	SELECT	@ApplicationId			= ApplicationId
		,	@ApplicationUserId		= ApplicationUserId
		,	@Image					= Image				
	FROM	dbo.ApplicationUserProfileImage
	WHERE	ApplicationUserProfileImageId	= @ApplicationUserProfileImageId
	ORDER BY ApplicationUserProfileImageId

	EXEC dbo.ApplicationUserProfileImageInsert 
			@ApplicationUserProfileImageId			=	NULL
		,	@ApplicationId 					     	=	@ApplicationId
		,	@ApplicationUserId						=	@ApplicationUserId
		,	@Image									=	@Image
		,	@AuditId								=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationUserProfileImageId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
