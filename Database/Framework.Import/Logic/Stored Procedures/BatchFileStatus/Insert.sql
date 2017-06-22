IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileStatusInsert')
BEGIN
	PRINT 'Dropping Procedure BatchFileStatusInsert'
	DROP  Procedure BatchFileStatusInsert
END
GO

PRINT 'Creating Procedure BatchFileStatusInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:BatchFileStatusInsert
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

CREATE Procedure dbo.BatchFileStatusInsert
(
		@BatchFileStatusId		INT			= NULL 	OUTPUT	
	,	@ApplicationId			INT	
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'BatchFileStatus'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @BatchFileStatusId OUTPUT, @AuditId
		
	INSERT INTO dbo.BatchFileStatus 
	( 
			BatchFileStatusId
		,	ApplicationId					
		,	Name				
		,	Description			
		,	SortOrder						
	)
	VALUES 
	(  
			@BatchFileStatusId	
		,	@ApplicationId
		,	@Name				
		,	@Description		
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileStatusId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 