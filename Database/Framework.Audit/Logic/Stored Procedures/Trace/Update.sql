IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TraceUpdate')
BEGIN
	PRINT 'Dropping Procedure TraceUpdate'
	DROP  Procedure  TraceUpdate
END
GO

PRINT 'Creating Procedure TraceUpdate'
GO

/******************************************************************************
**		File: 
**		Name: TraceUpdate
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

CREATE Procedure dbo.TraceUpdate
(
		@TraceId			INT				= NULL	 			
	,	@Name						VARCHAR(50)				
	,	@Description				VARCHAR(50)			
	,	@SortOrder					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'Trace'
)
AS
BEGIN
	UPDATE	dbo.Trace 
	SET		Name					=	@Name				
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder							
	WHERE	TraceId	=	@TraceId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'Trace'
		,	@EntityKey				= @TraceId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO