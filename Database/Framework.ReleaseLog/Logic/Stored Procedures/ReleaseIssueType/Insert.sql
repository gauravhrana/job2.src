IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseIssueTypeInsert')
BEGIN
	PRINT 'Dropping Procedure ReleaseIssueTypeInsert'
	DROP  Procedure ReleaseIssueTypeInsert
END
GO

PRINT 'Creating Procedure ReleaseIssueTypeInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ReleaseIssueTypeInsert
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
CREATE Procedure dbo.ReleaseIssueTypeInsert
(
		@ReleaseIssueTypeId				INT		= NULL 	OUTPUT	
	,	@ApplicationId					INT				
	,	@Name							VARCHAR(50)						
	,	@Description					VARCHAR (500)						
	,	@SortOrder						INT								
	,	@AuditId						INT									
	,	@AuditDate						DATETIME		= NULL				
	,	@SystemEntityType				VARCHAR(50)		= 'ReleaseIssueType'
)
AS
BEGIN
	
	IF @ReleaseIssueTypeId IS NULL OR @ReleaseIssueTypeId = -999999
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReleaseIssueTypeId OUTPUT, @AuditId
		
	INSERT INTO dbo.ReleaseIssueType 
	( 
			ReleaseIssueTypeId
		,	ApplicationId							
		,	Name				
		,	Description			
		,	SortOrder						
	)
	VALUES 
	(  
			@ReleaseIssueTypeId
		,	@ApplicationId			
		,	@Name				
		,	@Description		
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ReleaseIssueTypeId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 