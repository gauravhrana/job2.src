IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogInsert')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogInsert'
	DROP  Procedure ReleaseLogInsert
END
GO

PRINT 'Creating Procedure ReleaseLogInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ReleaseLogInsert
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

CREATE Procedure dbo.ReleaseLogInsert
(
		@ReleaseLogId			INT			= NULL 	OUTPUT	
	,	@ReleaseLogStatusId			INT		 				 			
	,	@ApplicationId			INT	
	,	@Name					VARCHAR(50)						
	,	@VersionNo				VARCHAR(50)						
	,	@ReleaseDate			DATETIME						
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ReleaseLog'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReleaseLogId OUTPUT, @AuditId

	INSERT INTO dbo.ReleaseLog 
	( 
			ReleaseLogId
		,	ReleaseLogStatusId		
		,	ApplicationId						
		,	Name				
		,   VersionNo			
		,	ReleaseDate			
		,	Description			
		,	SortOrder						
	)
	VALUES 
	(  
			@ReleaseLogId
		,	@ReleaseLogStatusId
		,	@ApplicationId			
		,	@Name				
		,	@VersionNo          
		,	@ReleaseDate        
		,	@Description		
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ReleaseLogId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 