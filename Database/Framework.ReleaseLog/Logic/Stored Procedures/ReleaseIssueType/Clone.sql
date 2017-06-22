IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseIssueTypeClone')
BEGIN
	PRINT 'Dropping Procedure ReleaseIssueTypeClone'
	DROP  Procedure ReleaseIssueTypeClone
END
GO

PRINT 'Creating Procedure ReleaseIssueTypeClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ReleaseIssueTypeClone
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
**		
**********************************************************************************************/
CREATE Procedure dbo.ReleaseIssueTypeClone
(
		@ReleaseIssueTypeId		        INT			= NULL 	OUTPUT	
	,	@ApplicationId			INT			
	,	@Name					VARCHAR(50)						
	,	@Description            VARCHAR (500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ReleaseIssueType'
)
AS
BEGIN
		IF @ReleaseIssueTypeId IS NULL OR @ReleaseIssueTypeId = -999999
		BEGIN
			EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReleaseIssueTypeId OUTPUT
		END						
	
		SELECT	@ApplicationId	= ApplicationId
			,	@Description	= Description
			,	@SortOrder		= SortOrder				
		FROM	dbo.ReleaseIssueType
		WHERE   ReleaseIssueTypeId		= @ReleaseIssueTypeId

		EXEC dbo.ReleaseIssueTypeInsert 
			@ReleaseIssueTypeId		=	NULL
		,	@ApplicationId			=	@ApplicationId
		,	@Name					=	@Name
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder
		,	@AuditId				=	@AuditId

		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseIssueType'
		,	@EntityKey				= @ReleaseIssueTypeId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

	END	
GO
