--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserTitleInsert')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserTitleInsert'
--	DROP  Procedure ApplicationUserTitleInsert
--END
--GO

--PRINT 'Creating Procedure ApplicationUserTitleInsert'
--GO

/*********************************************************************************************
**		File: 
**		Name:ApplicationUserTitleInsert
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

CREATE Procedure dbo.ApplicationUserTitleInsert
(
		@ApplicationUserTitleId		INT			= NULL 	OUTPUT	
	,	@ApplicationId				INT	
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(50)						
	,	@SortOrder					INT								
	,	@AuditId					INT									
	,	@AuditDate					DATETIME	= NULL				
	,	@SystemEntityType			VARCHAR(50)	= 'ApplicationUserTitle'	
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationUserTitleId OUTPUT, @AuditId
	
	INSERT INTO dbo.ApplicationUserTitle 
	( 
			ApplicationUserTitleId	
		,	ApplicationId					
		,	Name				
		,	Description			
		,	SortOrder						
	)
	VALUES 
	(  
			@ApplicationUserTitleId		
		,	@ApplicationId
		,	@Name				
		,	@Description		
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationUserTitleId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 