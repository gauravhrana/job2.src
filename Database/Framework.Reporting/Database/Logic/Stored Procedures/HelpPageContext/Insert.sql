IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'HelpPageContextInsert')
BEGIN
	PRINT 'Dropping Procedure HelpPageContextInsert'
	DROP  Procedure HelpPageContextInsert
END
GO

PRINT 'Creating Procedure HelpPageContextInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:HelpPageContextInsert
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/
CREATE Procedure dbo.HelpPageContextInsert
(
		@HelpPageContextId		INT				= NULL 	OUTPUT
	,	@ApplicationId			INT				= NULL			
	,	@Name					VARCHAR(50)						
	,	@Description            VARCHAR (500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME		= NULL				
	,	@SystemEntityType		VARCHAR(50)		= 'HelpPageContext'
)
AS
BEGIN	
	 
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @HelpPageContextId OUTPUT, @AuditId
		
	INSERT INTO dbo.HelpPageContext 
	( 
			HelpPageContextId
		,	ApplicationId									
		,	Name				
		,	Description			
		,	SortOrder		
	)
	VALUES 
	(  
			@HelpPageContextId	
		,	@ApplicationId				
		,	@Name				
		,	@Description
		,	@SortOrder
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @HelpPageContextId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 