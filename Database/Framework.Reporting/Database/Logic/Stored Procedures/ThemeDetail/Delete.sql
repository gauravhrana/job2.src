IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeDetailDelete')
BEGIN
	PRINT 'Dropping Procedure ThemeDetailDelete'
	DROP  Procedure ThemeDetailDelete
END
GO

PRINT 'Creating Procedure ThemeDetailDelete'
GO
/******************************************************************************
**		File: 
**		Name: ThemeDetailDelete
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
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.ThemeDetailDelete
(
		@ThemeDetailId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'ThemeDetail'
)
AS
BEGIN

	DELETE	 dbo.ThemeDetail
	WHERE	 ThemeDetailId = @ThemeDetailId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ThemeDetail'
		,	@EntityKey				= @ThemeDetailId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
