--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserProfileImageInsert')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserProfileImageInsert'
--	DROP  Procedure ApplicationUserProfileImageInsert
--END
--GO

--PRINT 'Creating Procedure ApplicationUserProfileImageInsert'
--GO

/*********************************************************************************************
**		File: 
**		Name:ApplicationUserProfileImageInsert
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
**********************************************************************************************/

CREATE Procedure dbo.ApplicationUserProfileImageInsert
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

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationUserProfileImageId OUTPUT, @AuditId
	
	INSERT INTO dbo.ApplicationUserProfileImage 
	( 
			ApplicationUserProfileImageId
		,	ApplicationId						
		,	ApplicationUserId					
		,	Image						
	)
	VALUES 
	(  
			@ApplicationUserProfileImageId	
		,	@ApplicationId					
		,	@ApplicationUserId			
		,	@Image			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationUserProfileImageId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 