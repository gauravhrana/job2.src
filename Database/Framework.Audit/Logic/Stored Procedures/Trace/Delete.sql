IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TraceDelete')
BEGIN
	PRINT 'Dropping Procedure TraceDelete'
	DROP  Procedure TraceDelete
END
GO

PRINT 'Creating Procedure TraceDelete'
GO
/******************************************************************************
**		File: 
**		Name: TraceDelete
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
CREATE Procedure dbo.TraceDelete
(
		@TraceId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'Trace'
)
AS
BEGIN

	DELETE	 dbo.Trace
	WHERE	 TraceId = @TraceId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'Trace'
		,	@EntityKey				= @TraceId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
