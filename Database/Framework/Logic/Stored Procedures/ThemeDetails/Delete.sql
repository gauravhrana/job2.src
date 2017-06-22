IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeDetailsDelete')
BEGIN
	PRINT 'Dropping Procedure ThemeDetailsDelete'
	DROP  Procedure ThemeDetailsDelete
END
GO

PRINT 'Creating Procedure ThemeDetailsDelete'
GO
/******************************************************************************
**		File: 
**		Name: ThemeDetailsDelete
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
CREATE Procedure dbo.ThemeDetailsDelete
(
		@ThemeDetailId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'ThemeDetails'
)
AS
BEGIN

	DELETE	 dbo.ThemeDetails
	WHERE	 ThemeDetailId = @ThemeDetailId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ThemeDetails'
		,	@EntityKey				= @ThemeDetailId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
