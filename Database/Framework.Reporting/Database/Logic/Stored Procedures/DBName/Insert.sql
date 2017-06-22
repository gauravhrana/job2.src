IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DBNameInsert')
BEGIN
	PRINT 'Dropping Procedure DBNameInsert'
	DROP  Procedure DBNameInsert
END
GO

PRINT 'Creating Procedure DBNameInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:DBNameInsert
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

CREATE Procedure dbo.DBNameInsert
(
		@DBNameId			INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT				= NULL	
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(500)						
	,	@SortOrder					INT								
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'DBName'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @DBNameId OUTPUT, @AuditId
	
	INSERT INTO dbo.DBName 
	( 
			DBNameId	
		,   ApplicationId					
		,	Name						
		,	[Description]					
		,	SortOrder						
	)
	VALUES 
	(  
			@DBNameId	
		,   @ApplicationId	
		,	@Name						
		,	@Description				
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @DBNameId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 