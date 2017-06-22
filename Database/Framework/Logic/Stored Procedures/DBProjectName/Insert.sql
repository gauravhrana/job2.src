IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DBProjectNameInsert')
BEGIN
	PRINT 'Dropping Procedure DBProjectNameInsert'
	DROP  Procedure DBProjectNameInsert
END
GO

PRINT 'Creating Procedure DBProjectNameInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:DBProjectNameInsert
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

CREATE Procedure dbo.DBProjectNameInsert
(
		@DBProjectNameId			INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT				= NULL	
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(500)						
	,	@SortOrder					INT								
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'DBProjectName'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @DBProjectNameId OUTPUT, @AuditId
	
	INSERT INTO dbo.DBProjectName 
	( 
			DBProjectNameId	
		,   ApplicationId					
		,	Name						
		,	[Description]					
		,	SortOrder						
	)
	VALUES 
	(  
			@DBProjectNameId	
		,   @ApplicationId	
		,	@Name						
		,	@Description				
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @DBProjectNameId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 