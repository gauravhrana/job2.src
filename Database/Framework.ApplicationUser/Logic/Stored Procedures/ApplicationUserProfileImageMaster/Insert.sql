--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserProfileImageMasterInsert')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserProfileImageMasterInsert'
--	DROP  Procedure ApplicationUserProfileImageMasterInsert
--END
--GO

--PRINT 'Creating Procedure ApplicationUserProfileImageMasterInsert'
--GO

/*********************************************************************************************
**		File: 
**		Name:ApplicationUserProfileImageMasterInsert
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

CREATE Procedure dbo.ApplicationUserProfileImageMasterInsert
(
		@ApplicationUserProfileImageMasterId	INT			= NULL 	OUTPUT	
	,	@ApplicationId							INT			
	,	@Title									VARCHAR(50)								
	,	@Image									VARBINARY(MAX)								
	,	@AuditId								INT			= NULL								
	,	@AuditDate								DATETIME	= NULL				
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationUserProfileImageMaster'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationUserProfileImageMasterId OUTPUT, @AuditId
	
	INSERT INTO dbo.ApplicationUserProfileImageMaster 
	( 
			ApplicationUserProfileImageMasterId
		,	ApplicationId						
		,	Title					
		,	Image						
	)
	VALUES 
	(  
			@ApplicationUserProfileImageMasterId	
		,	@ApplicationId					
		,	@Title			
		,	@Image			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationUserProfileImageMasterId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 