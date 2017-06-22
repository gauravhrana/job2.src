IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TypeOfIssueUpdate')
BEGIN
	PRINT 'Dropping Procedure TypeOfIssueUpdate'
	DROP  Procedure  TypeOfIssueUpdate
END
GO

PRINT 'Creating Procedure TypeOfIssueUpdate'
GO

/******************************************************************************
**		File: 
**		Name: TypeOfIssueUpdate
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.TypeOfIssueUpdate
(
		@TypeOfIssueId			INT				= NULL	 			
	,	@Name					VARCHAR(50)		
	,	@Category				VARCHAR(100)			
	,	@Description			VARCHAR(50)			
	,	@SortOrder				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'TypeOfIssue'
)
AS
BEGIN

	UPDATE	dbo.TypeOfIssue 
	SET		Name					=	@Name	
		,	Category				=	@Category			
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder							
	WHERE	TypeOfIssueId	=	@TypeOfIssueId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'TypeOfIssue'
		,	@EntityKey				= @TypeOfIssueId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO