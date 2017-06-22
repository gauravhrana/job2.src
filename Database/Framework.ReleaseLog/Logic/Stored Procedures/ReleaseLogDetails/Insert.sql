IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetailInsert')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailInsert'
	DROP  Procedure ReleaseLogDetailInsert
END
GO

PRINT 'Creating Procedure ReleaseLogDetailInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ReleaseLogDetailInsert
**		Desc:                               m/bb mnmmbbbbbbbbbbbbbbbbbbbbbbbvvjvvvvvjvvvvvvvjvbjjjjjjjjjhggb jg gjbkkkkkkkhkigb kgho89o
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

CREATE Procedure dbo.ReleaseLogDetailInsert
(
		@ReleaseLogDetailId				INT			= NULL 	OUTPUT	
	,	@ApplicationId					INT	
	,	@ReleaseLogId					INT								
	,	@ItemNo					        INT								
	,	@Description					VARCHAR(50)						
	,	@SortOrder						INT								
	,	@RequestedBy					VARCHAR(50)						
	,	@PrimaryDeveloper				VARCHAR(50)						
	,	@RequestedDate					INT								
	,	@AuditId						INT									
	,	@AuditDate						DATETIME	= NULL				
	,	@SystemEntityType				VARCHAR(50)	= 'ReleaseLogDetails'
)
AS
BEGIN

	IF @ReleaseLogDetailId IS NULL OR @ReleaseLogDetailId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReleaseLogDetailId OUTPUT, @AuditId
	END
	
	INSERT INTO dbo.ReleaseLogDetail 
	( 
			ReleaseLogDetailId	
		,	ApplicationId							
		,	ReleaseLogId				
		,   ItemNo						
		,	Description					
		,	SortOrder					
		,	RequestedBy                 
		,	PrimaryDeveloper            
		,	RequestedDate	
	)
	VALUES 
	(  
			@ReleaseLogDetailId	
		,	@ApplicationId			
		,	@ReleaseLogId				
		,	@ItemNo						
		,	@Description				
		,	@SortOrder					
		,	@RequestedBy                
		,	@PrimaryDeveloper           
		,	@RequestedDate
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ReleaseLogDetailId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 