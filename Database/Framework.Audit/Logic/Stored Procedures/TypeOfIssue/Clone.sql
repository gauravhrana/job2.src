IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TypeOfIssueClone')
BEGIN
	PRINT 'Dropping Procedure TypeOfIssueClone'
	DROP  Procedure TypeOfIssueClone
END
GO

PRINT 'Creating Procedure TypeOfIssueClone'
GO

/*********************************************************************************************
**		File: 
**		Name: TypeOfIssueClone
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

CREATE Procedure dbo.TypeOfIssueClone
(
		@TypeOfIssueId			INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@Name					VARCHAR(50)		
	,	@Category				VARCHAR(100)					
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'TypeOfIssue'
)
AS
BEGIN

	IF @TypeOfIssueId IS NULL OR @TypeOfIssueId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TypeOfIssueId OUTPUT
	END						
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Category			= Category
		,	@Description		= Description
		,	@SortOrder			= SortOrder				
	FROM	dbo.TypeOfIssue
	WHERE   TypeOfIssueId				= @TypeOfIssueId
	ORDER BY TypeOfIssueId

	EXEC dbo.TypeOfIssueInsert 
			@TypeOfIssueId	=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@Name					=	@Name
		,	@Category				=	@Category
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'TypeOfIssue'
		,	@EntityKey				= @TypeOfIssueId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
