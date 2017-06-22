--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserProfileImageMasterClone')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserProfileImageMasterClone'
--	DROP  Procedure ApplicationUserProfileImageMasterClone
--END
--GO

--PRINT 'Creating Procedure ApplicationUserProfileImageMasterClone'
--GO

/*********************************************************************************************
**		File: 
**		Name: ApplicationUserProfileImageMasterClone
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

CREATE Procedure dbo.ApplicationUserProfileImageMasterClone
(
		@ApplicationUserProfileImageMasterId	INT				= NULL 	OUTPUT	
	,	@ApplicationId							INT				= NULL
	,	@Title									VARCHAR(50)		= NULL				
	,	@Image									VARBINARY(MAX)	= NULL
	,	@AuditId								INT				= NULL					
	,	@AuditDate								DATETIME		= NULL
	,	@SystemEntityType						VARCHAR(50)		= 'ApplicationUserProfileImageMaster'
)
AS
BEGIN		
	
	SELECT	@ApplicationId			= ApplicationId
		,	@Title					= Title
		,	@Image					= Image				
	FROM	dbo.ApplicationUserProfileImageMaster
	WHERE	ApplicationUserProfileImageMasterId	= @ApplicationUserProfileImageMasterId
	ORDER BY ApplicationUserProfileImageMasterId

	EXEC dbo.ApplicationUserProfileImageMasterInsert 
			@ApplicationUserProfileImageMasterId	=	NULL
		,	@ApplicationId 					     	=	@ApplicationId
		,	@Title									=	@Title
		,	@Image									=	@Image
		,	@AuditId								=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationUserProfileImageMasterId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
