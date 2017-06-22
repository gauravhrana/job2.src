IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AuditActionInsert')
BEGIN
	PRINT 'Dropping Procedure AuditActionInsert'
	DROP  Procedure AuditActionInsert
END
GO

PRINT 'Creating Procedure AuditActionInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:AuditActionInsert
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
CREATE Procedure dbo.AuditActionInsert
(
		@AuditActionId		INT			= NULL 	OUTPUT		
	,	@Name				VARCHAR(50)						
	,	@Description		VARCHAR(50)						
	,	@SortOrder			INT				
	,	@ApplicationId		INT				
	,	@AuditId			INT								
	,	@AuditDate			DATETIME	= NULL				
	,	@SystemEntityType	VARCHAR(50)	= 'AuditAction'		
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @AuditActionId OUTPUT, @AuditId
	
	INSERT INTO dbo.AuditAction 
	( 
			AuditActionId					
		,	Name			
		,	Description		
		,	SortOrder	
		,	ApplicationId						
	)
	VALUES 
	(  
			@AuditActionId	
		,	@Name			
		,	@Description	
		,	@SortOrder	
		,	@ApplicationId	
	)

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @AuditActionId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END	
GO

 