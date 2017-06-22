IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogStatusInsert')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogStatusInsert'
	DROP  Procedure ReleaseLogStatusInsert
END
GO

PRINT 'Creating Procedure ReleaseLogStatusInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ReleaseLogStatusInsert
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
CREATE Procedure dbo.ReleaseLogStatusInsert
(
		@ReleaseLogStatusId				INT		= NULL 	OUTPUT	
	,	@ApplicationId					INT				
	,	@Name							VARCHAR(50)						
	,	@Description					VARCHAR (500)						
	,	@SortOrder						INT								
	,	@AuditId						INT									
	,	@AuditDate						DATETIME		= NULL				
	,	@SystemEntityType				VARCHAR(50)		= 'ReleaseLogStatus'
)
AS
BEGIN
	
	IF @ReleaseLogStatusId IS NULL OR @ReleaseLogStatusId = -999999
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReleaseLogStatusId OUTPUT, @AuditId
		
	INSERT INTO dbo.ReleaseLogStatus 
	( 
			ReleaseLogStatusId
		,	ApplicationId							
		,	Name				
		,	Description			
		,	SortOrder						
	)
	VALUES 
	(  
			@ReleaseLogStatusId
		,	@ApplicationId			
		,	@Name				
		,	@Description		
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ReleaseLogStatusId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 