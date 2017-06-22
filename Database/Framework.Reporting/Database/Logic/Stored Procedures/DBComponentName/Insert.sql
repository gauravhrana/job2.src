IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DBComponentNameInsert')
BEGIN
	PRINT 'Dropping Procedure DBComponentNameInsert'
	DROP  Procedure DBComponentNameInsert
END
GO

PRINT 'Creating Procedure DBComponentNameInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:DBComponentNameInsert
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

CREATE Procedure dbo.DBComponentNameInsert
(
		@DBComponentNameId			INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT				= NULL	
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(500)						
	,	@SortOrder					INT								
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'DBComponentName'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @DBComponentNameId OUTPUT, @AuditId
	
	INSERT INTO dbo.DBComponentName 
	( 
			DBComponentNameId	
		,   ApplicationId					
		,	Name						
		,	[Description]					
		,	SortOrder						
	)
	VALUES 
	(  
			@DBComponentNameId	
		,   @ApplicationId	
		,	@Name						
		,	@Description				
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @DBComponentNameId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 